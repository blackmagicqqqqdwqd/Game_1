using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FoneMove : MonoBehaviour
{
  
    [SerializeField] int speed = 1;
    void Start()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.AddForce(new Vector2(0, speed), ForceMode2D.Impulse); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
