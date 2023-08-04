using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Lazerwall 
{
    public string name;
    GameObject this_lazer;
    Vector3 point_1;
    Vector3 point_2;
    Color color;
    int wight;
    public Rigidbody2D rb; 
    LineRenderer lr;
    public Lazerwall(Vector2 center, int length, Color color)
    {
        wight = length;
        GameObject this_lazer = new GameObject();
        this.color = color;
        lr = this_lazer.AddComponent<LineRenderer>();
        lr.SetPosition(0, center - new Vector2(wight / 2, 0));
        lr.SetPosition(1, center + new Vector2(wight / 2, 0));
        lr.SetWidth(0.3f, 0.3f);
        lr.material = Resources.Load<Material>("Lazer");
        lr.SetColors(color, color);
        var rb = this_lazer.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        var lm = rb.AddComponent<LazerMove>();
        lm.lazerwall = this;
        name = "12";
        rb.AddForce(new Vector2(0, 10));
    }
    public void Move(Vector3 direction)
    {
        lr.SetPosition(0, lr.GetPosition(0) + direction);
        lr.SetPosition(1, lr.GetPosition(1) + direction);
    }

}
public class LazerWallsRepository : Repository
{
    public List<Lazerwall> lazers;
    public LazerWallsRepository()
    {
        lazers = new List<Lazerwall>();
    }

}
public class LazerWallsInteractor : Interactor
{
    LazerWallsRepository repository;
    public void Creat(Vector2 center, int length,Color color)
    {
        repository.lazers.Add( new Lazerwall(center, length, color));
    }
    public override void Initialize()
    {
        repository = Scene_1.s.repositorysBase.GetRepository <LazerWallsRepository>();
    }
}


