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
    public Rocket(float x, float y, Color_state color)
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
            switch (Random.Range(0, 8))
            {
                case 0:
                    Rocket rocket = new (playerPos.x - 10, playerPos.y - 10, Color_state.red);
                    break;
                case 1:
                    Rocket rocket1 = new(playerPos.x - 10, playerPos.y + 10, Color_state.red);
                    break;
                case 2:
                    Rocket rocket2 = new(playerPos.x + 10, playerPos.y + 10, Color_state.red);
                    break;
                case 3:
                    Rocket rocket3 = new(playerPos.x + 10, playerPos.y - 10, Color_state.red);
                    break;
                case 4:
                    Rocket rocket4 = new(playerPos.x - 7, playerPos.y - 8, Color_state.red);
                    break;
                case 5:
                    Rocket rocket5 = new(playerPos.x - 9, playerPos.y + 6, Color_state.red);
                    break;
                case 6:
                    Rocket rocket6 = new(playerPos.x + 5, playerPos.y + 7, Color_state.red);
                    break;
                case 7:
                    Rocket rocket7 = new(playerPos.x + 8, playerPos.y - 7, Color_state.red);
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
                
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                statk = true;
            }
            Destroy(gameObject);
            //gameObject(false);
        }  
    }
}

public class RocketRepocitory : Repository
{
    public List<Rocket> rockets;
    public override void Initialize()
    {
        rockets = new List<Rocket>();
    }
}
public class RocketInteractor: Interactor
{
    RocketRepocitory rocketRepocitory;
    public Rocket CreateRocket(float x, float y, Color_state color)
    {
        Rocket rocket = new Rocket(x, y, color);
        rocketRepocitory.rockets.Add(rocket);
        return rocket;
    }
    public override void Initialize() => rocketRepocitory = Scene_1.s.repositorysBase.GetRepository<RocketRepocitory>();
}