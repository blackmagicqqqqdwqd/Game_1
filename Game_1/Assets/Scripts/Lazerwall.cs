using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class Lazerwall : MonoBehaviour
{
    public Lazerwall_clone lazerwall;
    void Update()
    {
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
        if (v.x > 1 || v.x < 0 || v.y > 1 || v.y < 0) Destroy(gameObject);
        lazerwall.Move2(transform.position);
        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer.OverlapPoint(transform.position))
        {
            if (lazerwall.Color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color)
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
            }


        }
    }
}

public class Lazerwall_clone 
{
    public GameObject This_lazer { get; set; }
    public Color_state Color{ get; set; }
    int wight;
    public Rigidbody2D rb; 
    LineRenderer lr;
    public Lazerwall_clone(Vector2 center, int length , Vector2 speed, Color_state color = Color_state.random)
    {

        if (color == Color_state.random)
        {
            color = (Color_state)Random.Range(1,4);
        }
        this.Color = color;
        wight = length;
        This_lazer = new GameObject();

        This_lazer.transform.position = center;
        lr = This_lazer.AddComponent<LineRenderer>();
        lr.sortingOrder = 1;
        lr.SetPosition(0, center - new Vector2(wight / 2, 0));
        lr.SetPosition(1, center + new Vector2(wight / 2, 0));
        lr.SetWidth(0.3f, 0.3f);
        lr.material = Resources.Load<Material>("Lazer");
        switch (color)
        {
            case Color_state.red:
                lr.SetColors(UnityEngine.Color.red, UnityEngine.Color.red);
                break;
            case Color_state.blue:
                lr.SetColors(UnityEngine.Color.blue, UnityEngine.Color.blue);
                break;
            case Color_state.purple:
                lr.SetColors(UnityEngine.Color.magenta, UnityEngine.Color.magenta);
                break;
            case Color_state.none:
                lr.SetColors(UnityEngine.Color.white, UnityEngine.Color.white);
                break;
            default:
                lr.SetColors(UnityEngine.Color.black, UnityEngine.Color.black);
                break;
        }
        rb = This_lazer.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        var lm = rb.AddComponent<Lazerwall>();
        lm.lazerwall = this;
        rb.AddForce(new Vector2(0, Random.Range(speed.x,speed.y)),ForceMode2D.Impulse);
    }
    public void Move(Vector3 direction)
    {
        lr.SetPosition(0, lr.GetPosition(0) + direction);
        lr.SetPosition(1, lr.GetPosition(1) + direction);
    }
    public void Move2(Vector3 center)
    {
        lr.SetPosition(0, center - new Vector3(wight / 2, 0,0));
        lr.SetPosition(1, center + new Vector3(wight / 2, 0, 0));
    }

}
public class LazerWallsRepository : Repository
{
    public List<Lazerwall_clone> lazers;
    public LazerWallsRepository()
    {
        lazers = new List<Lazerwall_clone>();
    }

}
public class LazerWallsInteractor : Interactor
{
    LazerWallsRepository repository;
    public void Creat(Vector2 center, int length , Vector2 speed ,Color_state color)
    {
        repository.lazers.Add( new Lazerwall_clone(center, length, speed,  color));
    }
    public override void Initialize()
    {
        repository = Scene_1.s.repositorysBase.GetRepository <LazerWallsRepository>();
    }
    public void Destroy(Lazerwall_clone lazerwall)
    {
        repository.lazers.Remove(lazerwall);
    }
    public void Clear() => repository.lazers.Clear();
}


