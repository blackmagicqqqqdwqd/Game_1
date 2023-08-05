using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CircleSpawns : MonoBehaviour
{
    float radius = 5f;
    int spawnsAmount = 10;
    float circleRotation = 75.8f;
    GameObject circumflex;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { CreateSpawns(radius,spawnsAmount,circleRotation); }
    }
    public void CreateSpawns(float rad, int amount, float rot)
    {
        if (rad > 0 && amount > 0)
        {
            for (int i = 1; i <= amount; ++i)
            {
                GameObject go = new GameObject();
                go.transform.position = new Vector3(radius * Mathf.Cos((360 / spawnsAmount) * i * Mathf.Deg2Rad + circleRotation * Mathf.Deg2Rad), radius * Mathf.Sin((360 / spawnsAmount) * i * Mathf.Deg2Rad + circleRotation * Mathf.Deg2Rad), 0);
                go.AddComponent<SpriteRenderer>();
                go.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
            }
        }
    }
}
