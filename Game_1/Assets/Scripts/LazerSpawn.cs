using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LazerSpawn : MonoBehaviour
{
    /*
    public GameObject This_laser { get; set; }
    public Rigidbody2D rb;
    public Color_state Color { get; set; }
    LineRenderer lr;
    public void Lazer_Spawn(Vector2 lCoord, Vector2 rCoord, Color_state color = Color_state.random)
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
        lr.startWidth = 0.3f;
        lr.endWidth = 0.3f;
        lr.material = Resources.Load<Material>("Lazer");
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
        var meshFilter = This_laser.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        lr.BakeMesh(mesh);
        meshFilter.sharedMesh = mesh;        
        var meshRenderer = This_laser.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = Resources.Load<Material>("Lazer");
        meshRenderer.sortingOrder = 1;
        GameObject.Destroy(lr);
        StartCoroutine(IsOutOfBounds(This_laser));
        StartCoroutine(PlayerCheck(This_laser,Color));

    }  
    IEnumerator IsOutOfBounds(GameObject laser)
    {
        yield return new WaitForSeconds(0.05f);
        if (Input.GetKeyDown(KeyCode.C)) Debug.Log(laser.transform.position);
        if (transform.TransformPoint(laser.transform.position).y > 30) Destroy(laser);
        else StartCoroutine(IsOutOfBounds(laser));
    }
    IEnumerator PlayerCheck(GameObject laser,Color_state color)
    {
        yield return new WaitForSeconds(0.025f);
        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield.GetComponent<CircleCollider2D>().OverlapPoint(laser.GetComponent<MeshRenderer>().bounds.center))
        {
            if (color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color) Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
        }
        StartCoroutine(PlayerCheck(laser, color));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) 
        {
            Lazer_Spawn(new Vector2(-7,Random.Range(-24,-17)),new Vector2(7,Random.Range(-24,-17)));
            Lazer_Spawn(new Vector2(-7, Random.Range(-24, -17)), new Vector2(7, Random.Range(-24, -17)));
            Lazer_Spawn(new Vector2(-7, Random.Range(-24, -17)), new Vector2(7, Random.Range(-24, -17)));
            Lazer_Spawn(new Vector2(-7, Random.Range(-24, -17)), new Vector2(7, Random.Range(-24, -17)));
            Lazer_Spawn(new Vector2(-7, Random.Range(-24, -17)), new Vector2(7, Random.Range(-24, -17)));
        }        
    }*/
}
