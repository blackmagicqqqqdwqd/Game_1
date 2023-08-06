using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System.Net.Sockets;

public class Rocket : MonoBehaviour
{
    Vector3 playerPos = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position;

    Vector3 point;

    public void rocketClone(float x, float y, Color color)
    {
        GameObject this_rocket = new GameObject("SomeRocket");
        var SR = this_rocket.AddComponent<SpriteRenderer>();
        this_rocket.AddComponent<CircleCollider2D>();
        this_rocket.AddComponent<Rigidbody2D>();
        this_rocket.AddComponent<ConstantForce2D>();
        this_rocket.AddComponent<CollisionDetector>();
        SR.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
        SR.color = color;
        SR.sortingOrder = 2;
        this_rocket.transform.position = new Vector3(x, y, 0);
        LaunchRocket(Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player, this_rocket, 8); // мин нормальная скорость = 8
    }
    void LaunchRocket(GameObject target, GameObject rocket, int speed)
    {
        rocket.GetComponent<ConstantForce2D>().force = (target.transform.position - rocket.transform.position) * speed;
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) == true) {
            Debug.Log(1);
            switch (Random.Range(0, 8))
            {
                case 0:
                    rocketClone(playerPos.x - 10, playerPos.y - 10, Color.red);
                    break;
                case 1:
                    rocketClone(playerPos.x - 10, playerPos.y + 10, Color.red);
                    break;
                case 2:
                    rocketClone(playerPos.x + 10, playerPos.y + 10, Color.red);
                    break;
                case 3:
                    rocketClone(playerPos.x + 10, playerPos.y - 10, Color.red);
                    break;
                case 4:
                    rocketClone(playerPos.x - 7, playerPos.y - 8, Color.red);
                    break;
                case 5:
                    rocketClone(playerPos.x - 9, playerPos.y + 6, Color.red);
                    break;
                case 6:
                    rocketClone(playerPos.x + 5, playerPos.y + 7, Color.red);
                    break;
                case 7:
                    rocketClone(playerPos.x + 8, playerPos.y - 7, Color.red);
                    break;
            }
        }
    }
}
public class CollisionDetector : MonoBehaviour { 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(8);
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject == Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield)
        {
            Debug.Log(7);
            Destroy(gameObject);
            if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield.GetComponent<SpriteRenderer>().color != gameObject.GetComponent<SpriteRenderer>().color) Destroy(Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player);

        }  
    }
}
