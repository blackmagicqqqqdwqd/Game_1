using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Lazerweb : MonoBehaviour
{
    LineRenderer lr;
    Color_state color = Color_state.red;
    GameObject This_laser = new GameObject();
    public Lazerweb()
    {
        float radius = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.circleCollider.radius;
        This_laser.transform.SetParent(GameObject.Find("background").transform);
        lr = This_laser.AddComponent<LineRenderer>();
        lr.sortingOrder = 2;
        lr.SetPosition(0, new Vector2(-7, Random.Range(-24-radius, -22+radius)));
        lr.SetPosition(1, new Vector2(7, Random.Range(-24-radius, -22+radius)));
        lr.startWidth = 0.3f;
        lr.endWidth = 0.3f;
        lr.material = Resources.Load<Material>("Material/Lazer");
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
        MeshFilter meshFilter = This_laser.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        This_laser.GetComponent<LineRenderer>().BakeMesh(mesh);
        meshFilter.sharedMesh = mesh;
        MeshRenderer meshRenderer = This_laser.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = Resources.Load<Material>("Material/Lazer");
        meshRenderer.sortingOrder = 2;
        GameObject.Destroy(This_laser.GetComponent<LineRenderer>());
    }
    void Update()
    {        /*
        if (transform.TransformPoint(This_laser.transform.position).y > 30) Destroy(This_laser);
        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield.GetComponent<CircleCollider2D>().OverlapPoint(This_laser.GetComponent<MeshRenderer>().bounds.center))
        {
            if (color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color) Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
        }*/
    }
}

public class LazerwebRepocitory : Repository
{
    public LazerwebController lazerwebController;

}
public class LazerwebInteractor : Interactor
{
    LazerwebRepocitory lazerwebrepocitory;
    public Lazerweb CreateLazerweb()
    {
        if(lazerwebrepocitory == null) CreateController();
        Lazerweb lazerweb = new Lazerweb();
        //Lazerweb lazerweb = GameObject.Instantiate(Resources.Load<GameObject>("Lazerweb")).GetComponent<Lazerweb>();
        return lazerweb;
    }
    public override void Initialize() => lazerwebrepocitory = Scene_1.s.repositorysBase.GetRepository<LazerwebRepocitory>();
    public void CreateController()
    {
        if (lazerwebrepocitory.lazerwebController == null)
        {
            GameObject gameObject = new GameObject();
            lazerwebrepocitory.lazerwebController = gameObject.AddComponent<LazerwebController>();
        }
    }
}
public class LazerwebController : MonoBehaviour
{
    public List<Lazerweb> lazerwebs;  
    private void Start()
    {
        StartCoroutine(LazerwebSpawn());
    }

    public IEnumerator LazerwebSpawn()
    {
        for (int i = 0; i < 5; i++)
        {
            Color_state color = (Color_state)Random.Range(1, 4);
            Scene_1.s.interactorsBase.GetInteractor<LazerwebInteractor>().CreateLazerweb();
            yield return new WaitForSeconds(2f);
        }
    }
}
