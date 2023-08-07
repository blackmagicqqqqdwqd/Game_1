using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Rot : MonoBehaviour
{
    Turret_Rot_clone turret_data;
    float angle;
    bool lazerActivate;
    void Start()
    {
        angle = 90;
    }
    void Update()
    {
        if (lazerActivate == false && (turret_data.lr.GetPosition(1) - Move(transform.position, 90, 18)).magnitude > 1)
        {
            turret_data.lr.SetPosition(1, Vector3.MoveTowards(turret_data.lr.GetPosition(1), Move(transform.position, 90, 18), 10 * Time.deltaTime));
           //lazerActivate = true;
        }
        else
        {
            lazerActivate = true;
            Debug.Log(1);
        }

        if (angle > 0 && lazerActivate)
        {
            atack((Move(transform.position, angle, 18)));
            angle -= 0.1f;

        }


    }
    public void atack(Vector3 target)
    {
        turret_data.lr.SetPosition(1, target);
    }
    public Vector3 Move(Vector2 start, float angle, float rad)
    {
        return new Vector3(rad * Mathf.Cos(Mathf.Deg2Rad * angle) + start.x, rad * Mathf.Sin(Mathf.Deg2Rad * angle) + start.y, 0);
    }

    public class Turret_Rot_clone
    {
        GameObject this_turret;
        public LineRenderer lr;
        Color_state color;
        public Turret_Rot_clone(float x, float y, Color_state color)
        {
           

            this.color = color;
            this_turret = new GameObject();
            this_turret.transform.position = new Vector3(x, y, 0);

            SpriteRenderer sr = this_turret.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
            sr.sortingOrder = 2;

            lr = this_turret.AddComponent<LineRenderer>();
            lr.sortingOrder = 1;
            lr.material = Resources.Load<Material>("Lazer");
            lr.SetWidth(0.25f, 0.25f);
            lr.SetColors(Color.magenta, Color.magenta);
            lr.SetPosition(0, this_turret.transform.position);
            lr.SetPosition(1, this_turret.transform.position);

            this_turret.AddComponent<Turret_Rot>().turret_data = this;

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
    public class Turret_RotRepocitory:Repository
    {
        public List<Turret_Rot_clone> turrets;
        public override void Initialize()
        {
            turrets = new List<Turret_Rot_clone>();
        }
    }
    public class Turret_RotIterator:Interactor
    {
        Turret_RotRepocitory my_repocitory;
        public override void Initialize()
        {
            my_repocitory = Scene_1.s.repositorysBase.GetRepository<Turret_RotRepocitory>();

        }
        public void Creat_Turret_Rot(float x, float y, Color_state color)
        {
            my_repocitory.turrets.Add ( new Turret_Rot_clone(x,y,color));
        }
    }
    
}
