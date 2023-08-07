using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    GameObject game;
    GameObject game2;



    void Start()
    {
        
        game = Creat();
    }

    // Update is called once per frame
    void Update()
    {
        game.transform.position =  Vector3.MoveTowards(game.transform.position,GameObject.Find("Point").transform.position, 1 * Time.deltaTime);
        if (game.transform.position.magnitude > 3)
        {
            game2 = Creat();
        }
        
        // Move(Vector2.zero, 1, 2);
        //if()
    }
   
        public GameObject Creat()
        {
        GameObject go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Shield");
        return go;
    }
}
