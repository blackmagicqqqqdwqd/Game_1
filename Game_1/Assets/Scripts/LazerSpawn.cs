using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LazerSpawn : MonoBehaviour
{
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
        var meshFilter = This_laser.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        lr.BakeMesh(mesh);
        meshFilter.sharedMesh = mesh;

        var meshRenderer = This_laser.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = Resources.Load<Material>("Lazer");
        meshRenderer.sortingOrder = 1;
        //rb = This_laser.AddComponent<Rigidbody2D>();
        //rb.gravityScale = 0f;

        GameObject.Destroy(lr);
        StartCoroutine(IsOutOfBounds(This_laser));
       // StartCoroutine(PlayerCheck(This_laser));

    }  
    IEnumerator IsOutOfBounds(GameObject laser)
    {
        yield return new WaitForSeconds(5);
        if (transform.TransformPoint(laser.transform.position).y > 50) Destroy(laser);
        else StartCoroutine(IsOutOfBounds(laser));
    }
   /* IEnumerator PlayerCheck(GameObject laser)
    {
        yield return new WaitForSeconds(0.5f);
        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer.OverlapPoint(laser.transform.position))
        {
            if (laser.Color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color)
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
            }
        }
    }*/

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) {Lazer_Spawn(new Vector2(-7,Random.Range(-24,-17)),new Vector2(7,Random.Range(-24,-17)));}        
    }
}
