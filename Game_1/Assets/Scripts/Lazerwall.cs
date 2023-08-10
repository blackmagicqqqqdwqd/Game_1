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
        if ( player_collider == null) Debug.LogError("долбоёб, повесь коллизию на игрока");
    }
    void Update()
    {
        // проверка на выход за границы
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
        if (v.x > (1 + inacculacy) || v.x < (0 - inacculacy) || v.y > (1 + inacculacy) || v.y < (0 - inacculacy))
        {
            Destroy(gameObject);
            Scene_1.s.interactorsBase.GetInteractor<LazerWallsInteractor>().Destroy(lazerwall);
            
        }

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
    public bool Atack_is_now { get; set; }
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
        if (repository.lazers.Count == 0 )
        {
            Scene_1.Now_atack = false;
        }
    }
    public void Clear() => repository.lazers.Clear();
    public void atack_sleepers(int count, Vector3 start_position)
    {
        var g = new GameObject();
        var s = g.AddComponent<Lazer_controller>();
        s.start_position = start_position;
        s.lazers = repository.lazers;
    }
}

public class Lazer_controller:MonoBehaviour
{
    float spawn_distans = 3;
    public bool Continue { get; set; }
    public int now_count = 0;
    public int max_count = 4;
    public Vector3 start_position;
    public List<Lazerwall_clone> lazers;
    public void Start() => Scene_1.Now_atack = true;
    private void Update()
    {
        if (now_count != 0 && (now_count <= max_count) && ((start_position - lazers[lazers.Count - 1].This_lazer.transform.position).magnitude > spawn_distans)  )
        {
            now_count++;
            Scene_1.s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(start_position, 10, new Vector2(4, 4), Color_state.random);

        }
        else if (now_count == 0 && max_count != 0 )
        {
            now_count++;
            Scene_1.s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(start_position, 10, new Vector2(4, 4), Color_state.random);
        }
    }
    public void Is_Finish()
    {
        if (lazers.Count == 0 && now_count == max_count)
        Scene_1.Now_atack = false;
    }
}


