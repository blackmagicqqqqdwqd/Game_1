using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System.Net.Sockets;

public class Rocket : MonoBehaviour
{

    Vector3 playerPos;
    public void Start()
    {
         playerPos = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position;
    }
    Vector3 point;
    public Color_state cl = Color_state.none;
    public void rocketClone(float x, float y, Color_state color)
    {

        GameObject this_rocket = new GameObject("SomeRocket");
        var SR = this_rocket.AddComponent<SpriteRenderer>();
        this_rocket.AddComponent<CircleCollider2D>();
        this_rocket.AddComponent<Rigidbody2D>();
        this_rocket.AddComponent<CollisionDetector>().mycolor = color;

        SR.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
        switch (color)
        {
            case Color_state.red:
                SR.color = Color.red;
                
                break;
            case Color_state.blue:
                SR.color = Color.blue;
                break;
            case Color_state.purple:
                SR.color = Color.magenta;
                break;
            case Color_state.none:
                SR.color = Color.white ;
                break;
            default:
                SR.color = Color.red;
                break;
        }
        SR.sortingOrder = 2;
        this_rocket.transform.position = new Vector3(x, y, 0);
        LaunchRocket(Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player, this_rocket, 2); // мин нормальная скорость = 8
    }
    void LaunchRocket(GameObject target, GameObject rocket, int speed)
    {
        rocket.GetComponent<Rigidbody2D>().AddForce((target.transform.position - rocket.transform.position) * speed,ForceMode2D.Impulse);
        rocket.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) == true) {
            Debug.Log(1);
            switch (Random.Range(0, 8))
            {
                case 0:
                    rocketClone(playerPos.x - 10, playerPos.y - 10, Color_state.red);
                    break;
                case 1:
                    rocketClone(playerPos.x - 10, playerPos.y + 10, Color_state.red);
                    break;
                case 2:
                    rocketClone(playerPos.x + 10, playerPos.y + 10, Color_state.red);
                    break;
                case 3:
                    rocketClone(playerPos.x + 10, playerPos.y - 10, Color_state.red);
                    break;
                case 4:
                    rocketClone(playerPos.x - 7, playerPos.y - 8, Color_state.red);
                    break;
                case 5:
                    rocketClone(playerPos.x - 9, playerPos.y + 6, Color_state.red);
                    break;
                case 6:
                    rocketClone(playerPos.x + 5, playerPos.y + 7, Color_state.red);
                    break;
                case 7:
                    rocketClone(playerPos.x + 8, playerPos.y - 7, Color_state.red);
                    break;
            }
        }
    }
}
public class CollisionDetector : MonoBehaviour {
    public Color_state mycolor;
    public bool statk;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield)
        {

            if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color != mycolor && statk == false)
            {
                
                StartCoroutine(Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag());
                statk = true;
            }
            //Destroy(gameObject);
            //gameObject(false);
        }  
    }
}
