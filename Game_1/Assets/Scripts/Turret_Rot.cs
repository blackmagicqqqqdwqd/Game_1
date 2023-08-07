using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret_Rot : MonoBehaviour
{
    private Turret_Rot_clone turret_data;
    private float angle = 90;
    private bool lazerActivate;
    private float roration_speed = 0.02f;
    void Update()
    {
        if (lazerActivate == false && (turret_data.Lr.GetPosition(1) - Move(transform.position, 90, 18)).magnitude > 1)
        {
            turret_data.Lr.SetPosition(1, Vector3.MoveTowards(turret_data.Lr.GetPosition(1), Move(transform.position, 90, 18), 10 * Time.deltaTime));
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
       if (Physics2D.Linecast(transform.position, turret_data.Lr.GetPosition(1)))
        {
            if (turret_data.My_Color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color) 
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
            }
        }

    }
    public void atack(Vector3 target)
    {
        turret_data.Lr.SetPosition(1, target);
    }
    public Vector3 Move(Vector2 start, float angle, float rad)
    {
        return new Vector3(rad * Mathf.Cos(Mathf.Deg2Rad * angle) + start.x, rad * Mathf.Sin(Mathf.Deg2Rad * angle) + start.y, 0);
    }

    public class Turret_Rot_clone
    {
        GameObject this_turret;
        public LineRenderer Lr { get; set; }
        public Color_state My_Color { get;set; }
        public Turret_Rot_clone(float x, float y , Color_state color , float start_angle = 90 , float lazer_lenth = 18 )
        {
            My_Color = color;
            this_turret = new GameObject();
            this_turret.transform.position = new Vector3(x, y, 0);

            SpriteRenderer sr = this_turret.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
            sr.sortingOrder = 2;

            Lr = this_turret.AddComponent<LineRenderer>();
            Lr.sortingOrder = 1;
            Lr.material = Resources.Load<Material>("Lazer");
            Lr.SetWidth(0.25f, 0.25f);
            Lr.SetColors(Color.magenta, Color.magenta);
            Lr.SetPosition(0, this_turret.transform.position);
            Lr.SetPosition(1, this_turret.transform.position);

            this_turret.AddComponent<Turret_Rot>().turret_data = this;

            switch (color)
            {
                case Color_state.red:
                    Lr.SetColors(Color.red, Color.red);
                    break;
                case Color_state.blue:
                    Lr.SetColors(Color.blue, Color.blue);
                    break;
                case Color_state.purple:
                    Lr.SetColors(Color.magenta, Color.magenta);
                    break;
                case Color_state.none:
                    Lr.SetColors(Color.white, Color.white);
                    break;
                default:
                    Lr.SetColors(Color.red, Color.red);
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
