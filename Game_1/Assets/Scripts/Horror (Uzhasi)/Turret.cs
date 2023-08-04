using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    LineRenderer lr;
    SpriteRenderer sr;
    Vector3 targetVector;
    public GameObject target;
    float initLength = 0.125f;
    float length = 0.125f;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        sr = GetComponent<SpriteRenderer>();
        lr.SetWidth(0.25f, 0.25f);
        lr.SetColors(Color.magenta, Color.magenta);
        lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space)) StartCoroutine(ShootPrepare());            

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShootTarget(target));
        }

    }
    IEnumerator ShootTarget(GameObject target)
    {        
        yield return new WaitForSeconds(0.025f);
            lr.SetPosition(1, Vector3.Lerp(lr.GetPosition(0), target.transform.position, length + initLength/4));
        length += initLength/4;
        if (lr.GetPosition(1) != target.transform.position) { Debug.Log("2"); StartCoroutine(ShootTarget(target)); }
        else
        {
            yield return new WaitForSeconds(0.25f);
            lr.SetPosition(1, lr.GetPosition(0));
        }
        
    }
    IEnumerator ShootPrepare()
    {
        for (int i = 0; i < 3; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            Debug.Log("1");
        }
        StartCoroutine(ShootTarget(target));
    }
}
