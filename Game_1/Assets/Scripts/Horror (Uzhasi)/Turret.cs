using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    LineRenderer lr;
    SpriteRenderer sr;
    Animator an;
    Vector3 targetVector;
    public GameObject target;
    float initLength = 0.125f;
    float length = 0.125f;
    void Start()
    {
        an = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        sr = GetComponent<SpriteRenderer>();
        lr.SetWidth(0.25f, 0.25f);
        lr.SetColors(Color.magenta, Color.magenta);
        lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha8) && an.GetBool("idle")) StartCoroutine(ShootPrepare("blue"));
        else if (Input.GetKeyDown(KeyCode.Alpha9) && an.GetBool("idle")) StartCoroutine(ShootPrepare("red"));

    }
    IEnumerator ShootTarget(GameObject target)
    {
        yield return new WaitForSeconds(0.035f);
        lr.SetPosition(1, Vector3.Lerp(lr.GetPosition(0), target.transform.position, length + initLength / 4));
        length += initLength / 4;
        if (an.GetBool("blue_atack") == true) an.SetBool("blue_atack", false); else if (an.GetBool("red_atack") == true) an.SetBool("red_atack", false);
        if (lr.GetPosition(1) != target.transform.position) { Debug.Log("2"); StartCoroutine(ShootTarget(target)); }
        else
        {
            yield return new WaitForSeconds(0.25f);
            lr.SetPosition(1, lr.GetPosition(0));
            length = initLength;
            an.SetBool("idle", true);
        }
        

    }
    IEnumerator ShootPrepare(string color)
    {
        if (color == "blue")
        {
            lr.startColor = Color.blue;
            lr.endColor = Color.blue;
            an.SetBool("blue_atack", true);
            an.SetBool("idle", false);
        }
        else if (color == "red") {
            lr.startColor = Color.red;
            lr.endColor = Color.red;
            an.SetBool("red_atack", true);
            an.SetBool("idle", false);
        }
        yield return new WaitForSeconds(2.05f);
        StartCoroutine(ShootTarget(target));
    }
}
class TurretRepocitort :Repository
    {
        public override void Initialize()
        {
            base.Initialize();
        }
    }
    class TurretInteractor : Interactor
    {
        public void CreatTurrent()
        {

        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }

