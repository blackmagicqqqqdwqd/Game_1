using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }
    void Update()
    {
        ChangeColor();
    }
    void ChangeColor()
    {
        if (Input.GetKey(KeyCode.A)) { sr.enabled = true; sr.color = Color.red; }
        else if (Input.GetKey(KeyCode.D)) { sr.enabled = true; sr.color = Color.blue; }
        else sr.enabled = false;
    }
}
