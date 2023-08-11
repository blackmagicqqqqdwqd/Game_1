using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static TurretsInteractor;

public class Turret : MonoBehaviour
{
    public enum State
    {
        move_on_atack_zone,
        move_on_spawn_zone,
        loading_anim,
        shoot,
        wait,
        destroy
    }
    public State state = State.move_on_atack_zone;
    public Turret_Clone repocitory;
   
    public Vector3 Spawn_position;
    public Vector3 atack_position;
    float move_speed = 6;

    public void Atack()
    { 
        Vector3 target = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position;
       
        if ((repocitory.lr.GetPosition(1) - target).magnitude != 0)
            repocitory.lr.SetPosition(1, Vector3.MoveTowards(repocitory.lr.GetPosition(1), target, 16* Time.deltaTime));
    }
    public void Atack2()
    {
         
            state = State.loading_anim;
    }
    public void Update()
    {
        switch(state)
        {
            case State.wait:
                break;
            case State.shoot:
                Atack();
                break;
            case State.move_on_spawn_zone:
                //
                if (((transform.position - Spawn_position).magnitude != 0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, Spawn_position, move_speed * Time.deltaTime);
                }
                else if ((transform.position - Spawn_position).magnitude == 0)
                {
                    state = State.destroy;
                }
                break;
            case State.move_on_atack_zone: ////////////
                if (((transform.position - atack_position).magnitude != 0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, atack_position, move_speed * Time.deltaTime);
                }
                else if ((transform.position - atack_position).magnitude == 0)
                {
                    state = State.wait;
                    repocitory.lr.SetPosition(0, transform.position);
                    repocitory.lr.SetPosition(1, transform.position);
                }

                break;
            case State.loading_anim: ////////////
                if (repocitory.anim.GetBool(0) == false || repocitory.anim.GetBool(1) == false || repocitory.anim.GetBool(0) == false)
                {
                    if (repocitory.color == Color_state.purple) repocitory.anim.SetBool("magenta_atack", true);
                    else if (repocitory.color == Color_state.blue) repocitory.anim.SetBool("blue_atack", true);
                    else if (repocitory.color == Color_state.red) repocitory.anim.SetBool("red_atack", true);
                }
                state = State.wait;
                break;
            case State.destroy:
                Destroy(gameObject);
                break;
        }
      
      
            if (repocitory.lr.positionCount != 0 && Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer.OverlapPoint(repocitory.lr.GetPosition(1)) && state ==State.shoot)
        {
            if (repocitory.color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color)
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                state = State.wait;
                transform.parent.GetComponent<Turret_controller>().atack = false;
            }
            else state = State.wait;

            transform.parent.GetComponent<Turret_controller>().atack = false;
            repocitory.lr.positionCount = 0;
            state = State.move_on_spawn_zone;
        }

    }

    
}
public class Turret_Clone
{
    public Color_state color { get; set; }
    public LineRenderer lr;
    public Animator anim;
    public GameObject this_turret;
    public Turret turretScript;
    public Turret_Clone(float x, float y, Color_state color = Color_state.random)
    {
        if (color == Color_state.random)
        {
            color = (Color_state) UnityEngine.Random.Range(1, 4);
        }
        this.color = color;

        this_turret = new GameObject();
        this_turret.transform.position = new Vector3(x, y, 0);

        SpriteRenderer sr = this_turret.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
        sr.sortingOrder = 2;

        turretScript = this_turret.AddComponent<Turret>();
        turretScript.repocitory = this;

        lr = this_turret.AddComponent<LineRenderer>();
        lr.sortingOrder = 1;
        lr.material = Resources.Load<Material>("Material/Lazer");
        lr.startWidth = 0.25f;
        lr.endWidth = 0.25f;
        lr.SetPosition(0, this_turret.transform.position);
        lr.SetPosition(1, this_turret.transform.position);

        anim = this_turret.AddComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Turret 1");

        turretScript.Spawn_position = new Vector3(x,y,0);
        switch (color)
        {
            case Color_state.red:
                lr.startColor = UnityEngine.Color.red;
                lr.endColor = UnityEngine.Color.red;
                break;
            case Color_state.blue:
                lr.startColor = UnityEngine.Color.blue;
                lr.endColor = UnityEngine.Color.blue;
                break;
            case Color_state.purple:
                lr.startColor = UnityEngine.Color.magenta;
                lr.endColor = UnityEngine.Color.magenta;
                break;
            case Color_state.none:
                lr.startColor = UnityEngine.Color.white;
                lr.endColor = UnityEngine.Color.white;
                break;
            default:
                lr.startColor = UnityEngine.Color.black;
                lr.endColor = UnityEngine.Color.black;
                break;
        }
    }
}
public class TurretsRepocitort : Repository
{
    public List<Turret_Clone> turrets;
    public override void Initialize()
    {
        turrets = new List<Turret_Clone>();
    }
}
public class TurretsInteractor : Interactor
{
   
    TurretsRepocitort turretsRepocitort;
    public Turret_Clone CreatTurrent(float x, float y, Color_state color)
    {
        Turret_Clone turret = new Turret_Clone(x, y, color);
        turretsRepocitort.turrets.Add(turret);
        return turret;
    }
    public override void Initialize()=> turretsRepocitort = Scene_1.s.repositorysBase.GetRepository<TurretsRepocitort>();
   
    public void CircleAtack(float rad, int amount, float rot)
    {

        GameObject f = new GameObject();
        var b = f.AddComponent<Turret_controller>();
        b.rad = rad;
        b.rot = rot;
        b.amount = amount;
        b.turrets = turretsRepocitort.turrets;

    }
    public void Clear() => turretsRepocitort.turrets.Clear(); // желательно проверять все ли Gameobject уничтожены
    public void DestroyTurren(Turret_Clone turret)
    {
        turretsRepocitort.turrets.Clear();
    }

    public class Turret_controller : MonoBehaviour
    {
        public List<Turret_Clone> turrets;
        int index_now_turret;
        public bool atack;
        public float rad;
        public int amount;
        public float rot;

        public void Start()
        {
            Scene_1.Now_atack = true;
            var vv1 = CircleSpawn(rad, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
            var vv2 = CircleSpawn(rad + 8, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
            var len = 2 * Math.PI * rad / amount;
            for (int i = 0; i < vv1.Count; i++)
            {
                var turret = Scene_1.s.interactorsBase.GetInteractor<TurretsInteractor>().CreatTurrent(vv2[i].x, vv2[i].y, Color_state.random);
                turret.turretScript.atack_position = vv1[i];
                turret.this_turret.transform.SetParent(this.transform);
            }
        }
        public  void Update()
        {
         if (turrets.Count !=  0 && atack == false && index_now_turret <= turrets.Count-1 && turrets[index_now_turret].turretScript.state == Turret.State.wait)
            {
                atack = true;
                turrets[index_now_turret].turretScript.Atack2();
                index_now_turret++;
            }       
         if (index_now_turret == amount && atack == false)
            {
                Scene_1.Now_atack = false;
                Scene_1.s.interactorsBase.GetInteractor<TurretsInteractor>().Clear();
                GameObject.Destroy(this.gameObject);

            }
        }
        List<Vector3> CircleSpawn(float rad, int amount, float rot, Vector3 coord)
        {
            if (rad > 0 && amount > 0)
            {
                List<Vector3> SpawnedObjects = new List<Vector3>();
                for (int i = 1; i <= amount; ++i)
                {
                    var v = new Vector3(coord.x + rad * Mathf.Cos((360 / amount) * i * Mathf.Deg2Rad + rot * Mathf.Deg2Rad), coord.y + rad * Mathf.Sin((360 / amount) * i * Mathf.Deg2Rad + rot * Mathf.Deg2Rad), 0);

                    SpawnedObjects.Add(v);
                }
                return SpawnedObjects;
            }
            else return null;
        }
    }
}

