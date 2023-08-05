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
    void Start()
    {
        StartCoroutine(ShootPrepare(repocitory.color));
    }
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
        else if (color == Color_state.red) {
            repocitory.lr.startColor = Color.red;
            repocitory.lr.endColor = Color.red;
            repocitory.anim.SetBool("red_atack", true);
            repocitory.anim.SetBool("idle", false);
        }
        yield return new WaitForSeconds(2.05f);
        StartCoroutine(ShootTarget(Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player));
    }

}


public class TurretsRepocitort :Repository
    {
        public List<Turret_Clone> turrets; 
        public override void Initialize()
        {
            turrets = new List<Turret_Clone>();
        }
    }
    public class Turret_Clone
    {
        string name;
        public Color_state color { get; set; }
        public LineRenderer lr;
        public Animator anim;
        public GameObject this_turret;
        public Turret_Clone(int x, int y,Color_state color)
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
            this_turret.AddComponent<Turret>().repocitory = this;
    }

    }
    public class TurretsInteractor : Interactor
    {
        TurretsRepocitort turretsRepocitort;
        public void CreatTurrent(int x , int y,Color_state color)
        {
            Turret_Clone turret = new Turret_Clone(x,y, color);
            turretsRepocitort.turrets.Add(turret);
        }
        public override void Initialize()
        {
            turretsRepocitort = Scene_1.s.repositorysBase.GetRepository<TurretsRepocitort>();
        }
    }

