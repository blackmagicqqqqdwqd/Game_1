using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Turret_Rot : MonoBehaviour
{
    public enum State { move_on_atack_zone,  move_on_spawn_zone,  loading_anim,  shoot,   wait,  destroy }
    public State MyState { private set; get; } = State.move_on_atack_zone;
    private Transform target;  
    private Color color = Color.white;
    public Color Mycolor
    {
        get
        {
            return color;
        }
        set
        {
            if (lineRenderer != null)
            {
                if (value == Color.blue || value == Color.red || value == Color.magenta)
                {
                    color = value;
                    lineRenderer.startColor = value;
                    lineRenderer.endColor = value;
                }
                else if (value == Color.black)
                {
                    var v = new[] { Color.blue, Color.red, Color.magenta };
                    color = v[UnityEngine.Random.Range(0, v.Length)];
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                }
                else
                {
                    color = Color.white;
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                }
            }
        }
    }
    private float angle = 90;
    private bool lazerActivate;
    private float roration_speed = 0.02f;
    private LineRenderer lineRenderer; 
    private SpriteRenderer spriteRenderer;
    public void Start()
    {
        lineRenderer.GetComponent<LineRenderer>();
        spriteRenderer. GetComponent<SpriteRenderer>();
        target = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform;
    }
    void Update()
    {
        switch (MyState)
        { 
            case State.loading_anim:
                break;
            case State.shoot:
                break;
            case State.destroy:
                break;
        }

        if (lazerActivate == false && (lineRenderer.GetPosition(1) - Move(transform.position, 90, 18)).magnitude > 1)
        {
            lineRenderer.SetPosition(1, Vector3.MoveTowards(lineRenderer.GetPosition(1), Move(transform.position, 90, 18), 10 * Time.deltaTime));
        }
        else
        {
            lazerActivate = true;
        }

        if (angle > 0 && lazerActivate)
        {
            atack((Move(transform.position, angle, 18)));
            angle -= roration_speed;

        }

        if (Physics2D.Linecast(transform.position, lineRenderer.GetPosition(1)))
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, lineRenderer.GetPosition(1));
            if (hit.transform.tag == "Player")
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                lineRenderer.SetPosition(1, target.position);
                lineRenderer.positionCount = 0;
            }
            else if (hit.transform.tag == "Shield")
            {       
                if (hit.transform.GetComponent<Shield>().Compare_Color(Mycolor))
                {
                    lineRenderer.SetPosition(1, hit.transform.position);
                    lineRenderer.positionCount = 0;
                }
                else
                {
                    lineRenderer.SetPosition(1, hit.transform.position);             
                    lineRenderer.positionCount = 0;
                    Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                }
            }
        }

    }

    public void atack(Vector3 target)
    {
        //turret_data.Lr.SetPosition(1, target);
    }

    public Vector3 Move(Vector2 start, float angle, float rad)
    {
        return new Vector3(rad * Mathf.Cos(Mathf.Deg2Rad * angle) + start.x, rad * Mathf.Sin(Mathf.Deg2Rad * angle) + start.y, 0);
    }

 
}



public class Turret_RotIterator : Interactor
{
    Turret_RotRepocitory my_repocitory;
    public override void Initialize()
    {
        my_repocitory = Scene_1.s.repositorysBase.GetRepository<Turret_RotRepocitory>();

    }
    public void Creat_Turret_Rot(float x, float y, Color_state color)
    {
        //my_repocitory.turrets.Add(new Turret_Rot_clone(x, y, color));
    }
    public Turret_rot_controller Creat_controller()
    {
        GameObject gameObject = new GameObject("Contoller_turrets");
        Turret_rot_controller contoller = gameObject.AddComponent<Turret_rot_controller>();
        my_repocitory.controller = contoller;

        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player == null)
            gameObject.transform.position = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position;
        return contoller;

    }
}

public class Turret_RotRepocitory : Repository
{
    public Turret_rot_controller controller;
}

public class Turret_rot_controller : MonoBehaviour
{
    public List<Turret_Rot> turrets = new List<Turret_Rot>();
    //int index_now_turret;
    public bool atack;
    public float rad;
    public int amount;
    public float rot;

    public void Start()
    {
        
    }
    public void Update()
    {
       
    }
    }

