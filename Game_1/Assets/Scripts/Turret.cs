using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Turret : MonoBehaviour
{
    enum State
    {
        atack,
        move_on_atack_zone,
        move_on_spawn_zone,
        wait
    }
    State state = State.move_on_atack_zone;
    public Turret_Clone repocitory;
    public bool move_on_position = true;
    public Vector3 Spawn_position;
    public Vector3 atack_position;
    float move_speed = 6;

    bool atack = true;
    float initLength = 0.125f;
    float length = 0.125f;
    public void Atack() => StartCoroutine(ShootPrepare(repocitory.color));
    public void Update()
    {
        if (((transform.position - atack_position).magnitude != 0 ) && move_on_position)
        {
            transform.position = Vector3.MoveTowards(transform.position, atack_position, move_speed * Time.deltaTime);
        }
        else move_on_position = false;

        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer.OverlapPoint(repocitory.lr.GetPosition(1)) && atack &&   move_on_position == false)
        {
            if (repocitory.color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color)
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
            }
            else
            {
                atack = false;
            }

        }
    }
    IEnumerator ShootTarget(GameObject target)
    {

        yield return new WaitForSeconds(0.005f);
        repocitory.lr.SetPosition(0, transform.position);
        repocitory.lr.SetPosition(1, Vector3.Lerp(repocitory.lr.GetPosition(0), target.transform.position, length + initLength / 4));
        length += initLength / 4;
        if (repocitory.anim.GetBool("blue_atack") == true) repocitory.anim.SetBool("blue_atack", false);
        else if (repocitory.anim.GetBool("red_atack") == true) repocitory.anim.SetBool("red_atack", false);
        if (repocitory.lr.GetPosition(1) != target.transform.position) { StartCoroutine(ShootTarget(target)); }
        else
        {
            yield return new WaitForSeconds(0.25f);
            repocitory.lr.SetPosition(1, repocitory.lr.GetPosition(0));
            length = initLength;
            repocitory.anim.SetBool("idle", true);
        }


    }
   
    IEnumerator ShootPrepare(Color_state color)

    {
        if (color == Color_state.blue)
        {
            repocitory.lr.startColor = Color.blue;
            repocitory.lr.endColor = Color.blue;
            repocitory.anim.SetBool("blue_atack", true);
            repocitory.anim.SetBool("idle", false);
            Debug.Log(1);
        }
        else if (color == Color_state.red)
        {
            repocitory.lr.startColor = Color.red;
            repocitory.lr.endColor = Color.red;
            repocitory.anim.SetBool("red_atack", true);
            repocitory.anim.SetBool("idle", false);
        }
        yield return new WaitForSeconds(2.05f);
        StartCoroutine(ShootTarget(Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player));
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
        lr.material = Resources.Load<Material>("Lazer");
        lr.SetWidth(0.25f, 0.25f);
        lr.SetPosition(0, this_turret.transform.position);
        lr.SetPosition(1, this_turret.transform.position);

        anim = this_turret.AddComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Turret 1");


        switch (color)
        {
            case Color_state.red:
                lr.SetColors(Color.red, Color.red);
                break;
            case Color_state.blue:
                lr.SetColors(Color.blue, Color.blue);
                break;
            case Color_state.purple:
                lr.SetColors(Color.magenta, Color.magenta);
                break;
            case Color_state.none:
                lr.SetColors(Color.white, Color.white);
                break;
            default:
                lr.SetColors(Color.red, Color.red);
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
    public IEnumerator CircleAtack(float rad, int amount, float rot)
    {
        var len = 2 * Math.PI * rad / amount;
        var vv1 = CircleSpawn(rad, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
        var vv2 = CircleSpawn(rad + 8, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
        for (int i = 0; i < vv1.Count; i++) {
            CreatTurrent(vv2[i].x, vv2[i].y, Color_state.random).turretScript.atack_position = vv1[i];
        }

        for (int i = turretsRepocitort.turrets.Count - 1; i >= 1; i--)
        {
            int j = UnityEngine.Random.Range(1, i + 1);
            var temp = turretsRepocitort.turrets[j];
            turretsRepocitort.turrets[j] = turretsRepocitort.turrets[i];
            turretsRepocitort.turrets[i] = temp;
        }

        foreach (var v in turretsRepocitort.turrets )
        {
            yield return new WaitForSeconds(2);
            v.turretScript.Atack();
            if (v == turretsRepocitort.turrets[turretsRepocitort.turrets.Count- 1])
            {

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
        //GameObject player = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player;
        
    }
    public void Clear() => turretsRepocitort.turrets.Clear(); // желательно проверять все ли Gameobject уничтожены
    public void DestroyTurren(Turret_Clone turret)
    {
        GameObject.Destroy(turret.this_turret);
        // ? уничтожать турель в репозитории
    }

}

