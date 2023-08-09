using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class Lazerwall : MonoBehaviour
{
    public Lazerwall_clone lazerwall;
    private float inacculacy = 0.5f;
    Collider2D player_collider;
    public void Start()
    {
        player_collider = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer;
        if ( player_collider == null) Debug.LogError("������, ������ �������� �� ������");
    }
    void Update()
    {
        // �������� �� ����� �� �������
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
        if (v.x > (1 + inacculacy) || v.x < (0 - inacculacy) || v.y > (1 + inacculacy) || v.y < (0 - inacculacy)) Destroy(gameObject);

        //lazerwall.Move2(transform.position); // ?

        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer.OverlapPoint(lazerwall.mr.bounds.center))
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
    public Color_state Color { get; set; }
    public Rigidbody2D rb;
    public MeshRenderer mr { get; set; }

    private LineRenderer lr;
    private int wight;

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
        LineRenderer lr = This_lazer.AddComponent<LineRenderer>();
        lr.sortingOrder = 1;
        lr.SetPosition(0, center - new Vector2(wight / 2, 0));
        lr.SetPosition(1, center + new Vector2(wight / 2, 0));
        lr.SetWidth(0.3f, 0.3f);
        lr.material = Resources.Load<Material>("Material/Lazer");
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
       

        var meshFilter = This_lazer.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        lr.BakeMesh(mesh);
        meshFilter.sharedMesh = mesh;
        mr = This_lazer.AddComponent<MeshRenderer>();
        mr.sharedMaterial = Resources.Load<Material>("Material/Lazer");
        mr.sortingOrder = 1;
        GameObject.Destroy(lr);
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
    public IEnumerator atack_sleepers(int count, Vector3 start_position)
    {
        float spawn_distans = 3 ;
        Creat(start_position, 10, new Vector2(4,4), Color_state.random);
        int x = 0;
        while (x < count) 
        {
            if (( start_position - repository.lazers[repository.lazers.Count-1].This_lazer .transform.position).magnitude > spawn_distans)
            {
                Creat(start_position, 10, new Vector2(4, 4), Color_state.random);
                x++;
            }
            yield return null;
        }
    }
}

public class Lazer_controller:MonoBehaviour
{
   // List<Lazerwall_clone> lazer_walls;
}


