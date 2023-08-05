using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircleSpawns : MonoBehaviour
{
    //float radius = 5f;
    //int spawnsAmount = 10;
    //float circleRotation = 75.8f;
    //Vector3 coordinates = new Vector3(-5, 1, 0);
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { foreach(Vector3 vect in CircleSpawn(5f, 10, 75, new Vector3(-5,1,0))) {Debug.Log(vect); };}
    }
    public List<Vector3> CircleSpawn(float rad, int amount, float rot, Vector3 coord)
    {
        if (rad > 0 && amount > 0)
        {
            List<Vector3> SpawnedObjects = new List<Vector3>();
            for (int i = 1; i <= amount; ++i)
            {
                GameObject go = new GameObject("Spawned"+i);
                go.transform.position = new Vector3(coord.x+rad * Mathf.Cos((360 / amount) * i * Mathf.Deg2Rad + rot * Mathf.Deg2Rad), coord.y+rad * Mathf.Sin((360 / amount) * i * Mathf.Deg2Rad + rot * Mathf.Deg2Rad), 0);
                go.AddComponent<SpriteRenderer>();
                go.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
                SpawnedObjects.Add(go.transform.position);
            }
            return SpawnedObjects;
        }
        else return null;
    }
}
