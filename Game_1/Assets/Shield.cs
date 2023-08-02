using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    SpriteRenderer sr;
    int x;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }
    void Update()
    {
        ChangeColor();
        Debug.Log("Welcome");
    }
    void ChangeColor()
    {
        if (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2)) { sr.enabled = true; sr.color = Color.magenta; }
        else if (Input.GetKey(KeyCode.Alpha1)) { sr.enabled = true; sr.color = Color.red; }
        else if (Input.GetKey(KeyCode.Alpha2)) { sr.enabled = true; sr.color = Color.blue; }
        else sr.enabled = false;
    }
}
