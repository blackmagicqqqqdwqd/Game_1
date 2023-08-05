using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Turret : MonoBehaviour
{
    public Turret_Clone repocitory;
    float initLength = 0.125f;
    float length = 0.125f;
    public void Atack() => StartCoroutine(ShootPrepare(repocitory.color));
    public void Update()
    {
       
        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().ccPlayer.OverlapPoint(repocitory.lr.GetPosition(1)))
        {
            if (repocitory.color != Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color)
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Died();
            }

        }
    }
    IEnumerator ShootTarget(GameObject target)
    {

        yield return new WaitForSeconds(0.015f);
        repocitory.lr.SetPosition(1, Vector3.Lerp(repocitory.lr.GetPosition(0), target.transform.position, length + initLength / 4));
        length += initLength / 4;
        if (repocitory.anim.GetBool("blue_atack") == true) repocitory.anim.SetBool("blue_atack", false);
        else if (repocitory.anim.GetBool("red_atack") == true) repocitory.anim.SetBool("red_atack", false);
        if (repocitory.lr.GetPosition(1) != target.transform.position) { Debug.Log("2"); StartCoroutine(ShootTarget(target)); }
        else
        {
            yield return new WaitForSeconds(0.25f);
            repocitory.lr.SetPosition(1, repocitory.lr.GetPosition(0));
            length = initLength;
            repocitory.anim.SetBool("idle", true);
        }


    }

    IEnumerator ShootPrepare(Color_state color)

    {
        if (color == Color_state.blue)
        {
            repocitory.lr.startColor = Color.blue;
            repocitory.lr.endColor = Color.blue;
            repocitory.anim.SetBool("blue_atack", true);
            repocitory.anim.SetBool("idle", false);
            Debug.Log(1);
        }
        else if (color == Color_state.red)
        {
            repocitory.lr.startColor = Color.red;
            repocitory.lr.endColor = Color.red;
            repocitory.anim.SetBool("red_atack", true);
            repocitory.anim.SetBool("idle", false);
        }
        yield return new WaitForSeconds(2.05f);
        StartCoroutine(ShootTarget(Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player));
    }

}
public class Turret_Clone
{
    string name;
    public Color_state color { get; set; }
    public LineRenderer lr;
    public Animator anim;
    public GameObject this_turret;
    public Turret turretScript;
    public Turret_Clone(float x, float y, Color_state color)
    {
        this.color = color;
        GameObject this_turret = new GameObject();
        name = this_turret.name;
        var SR = this_turret.AddComponent<SpriteRenderer>();
        SR.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
        lr = this_turret.AddComponent<LineRenderer>();
        anim = this_turret.AddComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Turret 1");
        this_turret.transform.position = new Vector3(x, y, 0);
        lr.SetPosition(0, this_turret.transform.position);
        lr.SetPosition(1, this_turret.transform.position);
        lr.sortingOrder = 1;
        SR.sortingOrder = 2;
        lr.material = Resources.Load<Material>("Lazer");
        lr.SetWidth(0.25f, 0.25f);
        lr.SetColors(Color.magenta, Color.magenta);
        turretScript = this_turret.AddComponent<Turret>();
        turretScript.repocitory = this;
    }

}
public class TurretsRepocitort : Repository
{
    public List<Turret_Clone> turrets;
    public override void Initialize()
    {
        turrets = new List<Turret_Clone>();
    }
}
public class TurretsInteractor : Interactor
{
    TurretsRepocitort turretsRepocitort;
    public Turret_Clone CreatTurrent(float x, float y, Color_state color)
    {
        Turret_Clone turret = new Turret_Clone(x, y, color);
        turretsRepocitort.turrets.Add(turret);
        return turret;
    }
    public override void Initialize()
    {
        turretsRepocitort = Scene_1.s.repositorysBase.GetRepository<TurretsRepocitort>();
    }
    public IEnumerator SquareAtack(float distans)
    {
        GameObject player = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player;
        Vector3 v_1 =  player.transform.TransformPoint(new Vector3(distans, distans, 0));
        Turret_Clone turret_1 = CreatTurrent(v_1.x, v_1.y, Color_state.red);
        Vector3 v_2 = player.transform.TransformPoint(new Vector3(-distans, -distans, 0));
        Turret_Clone turret_2 = CreatTurrent(v_2.x, v_2.y, Color_state.red);
        Vector3 v_3 = player.transform.TransformPoint(new Vector3(distans, -distans, 0));
        Turret_Clone turret_3 = CreatTurrent(v_3.x, v_3.y, Color_state.red);
        Vector3 v_4 = player.transform.TransformPoint(new Vector3(-distans, distans, 0));
        Turret_Clone turret_4 = CreatTurrent(v_4.x, v_4.y, Color_state.red);
        yield return new WaitForSeconds(2);
        turret_1.turretScript.Atack();
        yield return new WaitForSeconds(2);
        turret_2.turretScript.Atack();
        yield return new WaitForSeconds(2);
        turret_3.turretScript.Atack();
        yield return new WaitForSeconds(2);
        turret_4.turretScript.Atack();
    }
}

