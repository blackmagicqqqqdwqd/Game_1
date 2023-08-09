using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEngine.RuleTile.TilingRuleOutput;
/*
public class Lazerweb : MonoBehaviour
{
    public Lazerweb_Clone repocitory;
    MeshFilter meshFilter = repocitory.This_laser.addComponent<MeshFilter>();
    Mesh mesh = new Mesh();
    repocitory.getComponent<LineRenderer>().BakeMesh(mesh);
    meshFilter.sharedMesh = mesh;
    MeshRenderer meshRenderer = repocitory.This_laser.AddComponent<MeshRenderer>();
    meshRenderer.sharedMaterial = Resources.Load<Material>("Lazer");
    meshRenderer.sortingOrder = 1;
    GameObject.Destroy(repocitory.getComponent<LineRenderer>());
    public void checkOOB() => StartCoroutine(IsOutOfBounds(repocitory.This_laser));
    public void lazering() => StartCoroutine(PlayerCheck(repocitory.This_laser, repocitory.Color));
 IEnumerator IsOutOfBounds(GameObject laser)
{
    yield return new WaitForSeconds(0.05f);
    if (Input.GetKeyDown(KeyCode.C)) Debug.Log(laser.transform.position);
    if (transform.TransformPoint(laser.transform.position).y > 30) Destroy(laser);
    else StartCoroutine(IsOutOfBounds(laser));
}
IEnumerator PlayerCheck(GameObject laser, Color_state color)
{
    yield return new WaitForSeconds(0.025f);
    if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield.GetComponent<CircleCollider2D>().OverlapPoint(laser.GetComponent<MeshRenderer>().bounds.center))
    {
        if (color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color) Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
    }
    StartCoroutine(PlayerCheck(laser, color));
}
}
public class Lazerweb_Clone
{
    public GameObject This_laser { get; set; }
    public Rigidbody2D rb;
    public Color_state Color { get; set; }
    LineRenderer lr;
    public Lazerweb_Clone(Vector2 lCoord, Vector2 rCoord, Color_state color = Color_state.random)
    {
        if (color == Color_state.random)
        {
            color = (Color_state)Random.Range(1, 4);
        }
        this.Color = color;
        This_laser = new GameObject();
        This_laser.transform.SetParent(GameObject.Find("background").transform);
        lr = This_laser.AddComponent<LineRenderer>();
        lr.sortingOrder = 1;
        lr.SetPosition(0, lCoord);
        lr.SetPosition(1, rCoord);
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
        
    void Update()
    {
        
    }
}

public class Lazerweb_interactor :Interactor
{
    Lazerweb_repocitory lazerweb_repocitory;
    public Lazerweb_Clone CreateLazerweb(Vector2 lCoord, Vector2 rCoord, Color_state color)
    {
        Lazerweb_Clone lazerweb = new Lazerweb_Clone(lCoord, rCoord, color);
        lazerweb_repocitory.lazerwebs.Add(lazerweb);
        return lazerweb;
    }
    public override void Initialize() => lazerweb_repocitory = Scene_1.s.repositorysBase.GetRepository<Lazerweb_repocitory>();
}
public class Lazerweb_repocitory :Repository
{
    public List<Lazerweb_Clone> lazerwebs;
    public override void Initialize()
    {
        lazerwebs = new List<Lazerweb_Clone>();
    }
}
*/