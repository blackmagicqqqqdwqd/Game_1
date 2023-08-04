using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMove : MonoBehaviour
{
    public Lazerwall lazerwall;
    public void Start()
    {
        lazerwall.Move(new Vector3(10,10,0));
    }
    void Update()
    {
        

    }
}
