using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animation))]
public class Turret : MonoBehaviour
{
    public enum State
    {
        rotation,
        rotation_and_charging,
        move_on_spawn_zone,
        charging,
        shoot,
        orbit_rotation,
        wait,
        rotation_and_shoot,
        destroy
    }

    private Color_state color = Color_state.red;
    public Color_state Mycolor
    {
        get { return color; }
        set
        {
            switch (value)
            {
                case Color_state.red:
                    color = Color_state.red;
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = Color.red;
                    break;
                case Color_state.blue:
                    color = Color_state.blue;
                    lineRenderer.startColor = Color.blue;
                    lineRenderer.endColor = Color.blue;
                    break;
                case Color_state.purple:
                    color = Color_state.purple;
                    lineRenderer.startColor = Color.magenta;
                    lineRenderer.endColor = Color.magenta;
                    break;
                default:
                    color = Color_state.red;
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = Color.red;
                    break;
            }
        }
    }

    public State MyState { get; set; } = State.wait;
   
    public Vector3 Spawn_position { get; set; }
    public Vector3 atack_position { get; set; }

    private LineRenderer lineRenderer;
    private Animator animator;
    private Transform target;
    private float move_speed = 6;
    private float lazer_speed = 10;
    private float lenth =0;

    public void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        animator = gameObject.GetComponent<Animator>();
        target = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform;
        lineRenderer.startColor= Color.red;
        lineRenderer.endColor= Color.red;
        lineRenderer.positionCount = 0;
    }   
    public void Update()
    {
       
        
        switch (MyState)
        {
            case State.wait:
              
                break;
            case State.shoot:
                Atack();

                break;
            case State.move_on_spawn_zone:
                //
                transform.parent.GetComponent<Turret_controller>().SpawnPos();
                if (((transform.position - Spawn_position).magnitude != 0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, Spawn_position, move_speed * Time.deltaTime);
                }
                 if ((transform.position - target.position).magnitude >= (Spawn_position - target.position).magnitude - 0.2f)
                {
                    MyState = State.destroy;
                }
                break;
            case State.orbit_rotation:
                   
            case State.rotation: ////////////
                Move_on_atack_position();
                if ((transform.position - target.position).magnitude <= (atack_position - target.position).magnitude + 0.1f)
                {
                    MyState = State.orbit_rotation;
                }
                break;
            case State.charging: ////////////
             
                Charging();
                break;
            case State.destroy:
            
                Destroy(gameObject);
                break;
            case State.rotation_and_charging:
             
                Charging();
            
                Move_on_atack_position();
                break;
            case State.rotation_and_shoot:
                Atack();
                Move_on_atack_position();
                break;
        }




        if (lineRenderer.positionCount != 0 && Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer.OverlapPoint(lineRenderer.GetPosition(1)) && (MyState ==State.shoot || MyState == State.rotation_and_shoot))
        {
            if (Mycolor != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color)
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                MyState = State.wait;
                transform.parent.GetComponent<Turret_controller>().atack = false;
            }
            else MyState = State.wait;

            transform.parent.GetComponent<Turret_controller>().atack = false;
            lineRenderer.positionCount = 0;
            MyState = State.move_on_spawn_zone;
        }

    }
    public void Atack()
    {
        if (lineRenderer.positionCount != 2  )
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, transform.position);
        }
       
        lineRenderer.SetPosition(0,transform.position);
        
        if ((lineRenderer.GetPosition(1) - target.position).magnitude != 0)
        {
           lineRenderer.SetPosition(1, Vector3.MoveTowards(transform.position, target.position, lenth)  );
           lenth += lazer_speed * Time.deltaTime;
        }
          
    }

    
    public void Charging()
    {
        
        if (animator.GetBool(0) == false || animator.GetBool(1) == false || animator.GetBool(0) == false)
        {
            Debug.Log(1);
            if (Mycolor == Color_state.purple) animator.SetBool("magenta_atack", true);
            else if (Mycolor == Color_state.blue) animator.SetBool("blue_atack", true);
            else if (Mycolor == Color_state.red)
            {

                animator.SetBool("red_atack", true);
            }
            
        }
     
    }
    public void Atack2() => MyState = State.rotation_and_charging;
    public void Move_on_atack_position()
    {
        //Debug.Log((transform.position - target).magnitude);
        
        if (((transform.position - atack_position).magnitude != 0))
        {
            transform.position = Vector3.MoveTowards(transform.position, atack_position, move_speed * Time.deltaTime);
        }
        else if ((transform.position - atack_position).magnitude == 0)
        {
            MyState = State.wait;
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, transform.position);
        }
       
    }
}

public class TurretsRepocitort : Repository
{
    public Turret_controller my_contorller;
}

public class TurretsInteractor : Interactor
{
    TurretsRepocitort turretsRepocitort;
    public Turret CreatTurrent(float x, float y, Color_state color, Turret_controller my_contorller)
    {
        if (my_contorller != null)
        {
           
            GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>("pr"));
            gameObject.transform.position = new Vector3(x, y, 0);
            Turret turret = gameObject.GetComponent<Turret>();
            turret.Spawn_position = new Vector3(x, y, 0);
            my_contorller.turrets.Add(turret);
            return turret;
        }
        Debug.Log("не создан контролер");
        return null;       
    }

    public override void Initialize()=> turretsRepocitort = Scene_1.s.repositorysBase.GetRepository<TurretsRepocitort>();
    public Turret_controller Creat_controller()
    {
        GameObject gameObject = new GameObject("Contoller_turrets");
        Turret_controller contoller = gameObject.AddComponent<Turret_controller>();
        turretsRepocitort.my_contorller = contoller;

        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player == null)
            gameObject.transform.position = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position;
        return contoller;

    }
    public void CircleAtack(float rad, int amount, float rot)
    {
        var contoller = Creat_controller();
        contoller.rad = rad;
        contoller.amount = amount;
        contoller.rot = rot;
    }
    public void DestroyTurren( ) =>  GameObject.Destroy(turretsRepocitort.my_contorller);


}

public class Turret_controller : MonoBehaviour
{
    public List<Turret> turrets = new List<Turret>();
    int index_now_turret = 0;
    public bool atack = false;
    public float rad = 3;
    public int amount = 3;
    public float rot = 0;

    public void Start()
    {
        Scene_1.Now_atack = true;
      
        List<Vector3> atack_position = CircleSpawn(rad, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
        List<Vector3> spawn_position = CircleSpawn(rad + 8, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
        var len = 2 * Math.PI * rad / amount;

        for (int i = 0; i < amount; i++)
        {
            var turret = Scene_1.s.interactorsBase.GetInteractor<TurretsInteractor>().CreatTurrent(spawn_position[i].x, spawn_position[i].y, Color_state.red, this);
            turret.atack_position = atack_position[i];
            turret.transform.SetParent(this.transform);
            turret.MyState = Turret.State.wait;


        }
        Move_On_atack_zone_all();
        
    }
    public void Update()
    {
        
        List<Vector3> atack_position = CircleSpawn(rad, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
        rot += 20 * Time.deltaTime;
      
        Change_atack_position_all(atack_position);
        
        //if (turrets[0].turretScript.state == Turret.State.wait) { transform.Rotate(new Vector3(0, 0, 20 * Time.deltaTime)); }
        if (turrets.Count != 0 && atack == false && index_now_turret <= turrets.Count - 1 && turrets[index_now_turret].MyState == Turret.State.orbit_rotation)
        {
            atack = true;
            turrets[index_now_turret].Atack2();
            index_now_turret++;
        }
        if (index_now_turret == amount && atack == false)
        {
            Scene_1.Now_atack = false;
            Scene_1.s.interactorsBase.GetInteractor<TurretsInteractor>().DestroyTurren();
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
    public void Move_On_atack_zone_all()
    {
        foreach (Turret turret in turrets) turret.MyState = Turret.State.rotation;
    }
    public void Move_On_spawn_zone()
    {

    }
    public void Change_atack_position_all(List<Vector3> atack_positions)
    {
        for (int i = 0; i < amount; ++i)
        {
            turrets[i].atack_position = atack_positions[i];
        }
    }
    public void Change_swapn_position()
    {

    }
    public void SpawnPos()
    {
        List<Vector3> spawn_position = CircleSpawn(rad + 8, amount, rot, Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position);
        for (int i = 0; i < amount; ++i)
        {
            turrets[i].Spawn_position = spawn_position[i];
        }
       
    }
    
}

