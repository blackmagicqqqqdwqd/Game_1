
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Rocket : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform target;

    private Color_state color = Color_state.none;
    public Color_state Mycolor
    {
        get { return color; }
        set
        {
            if (spriteRenderer != null)
            {
                switch (value)
                {
                    case Color_state.red:
                        color = Color_state.red;
                        spriteRenderer.color = Color.red;
                        break;
                    case Color_state.blue:
                        color = Color_state.blue;
                        spriteRenderer.color = Color.blue;
                        break;
                    case Color_state.purple:
                        color = Color_state.purple;
                        spriteRenderer.color = Color.magenta;
                        break;
                    default:
                        color = Color_state.red;

                        break;
                }
            }
        }
    }

    bool b = true; 
    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Mycolor = (Color_state)Random.Range(1, 4);
        target = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform;
        switch (Random.Range(0, 8))
        {
            case 0:
                transform.position = new Vector3(target.position.x - 10, target.position.y - 10, 0);
                break;
            case 1:
                transform.position = new Vector3(target.position.x - 10, target.position.y + 10, 0);
                break;
            case 2:
                transform.position = new Vector3(target.position.x + 10, target.position.y + 10, 0);
                break;
            case 3:
                transform.position = new Vector3(target.position.x + 10, target.position.y - 10, 0);
                break;
            case 4:
                transform.position = new Vector3(target.position.x - 7, target.position.y - 8, 0);
                break;
            case 5:
                transform.position = new Vector3(target.position.x - 9, target.position.y + 6, 0);
                break;
            case 6:
                transform.position = new Vector3(target.position.x + 5, target.position.y + 7, 0);
                break;
            case 7:
                transform.position = new Vector3(target.position.x + 8, target.position.y - 7, 0);
                break;
        }
        LaunchRocket(target.gameObject, gameObject, 3);
    }
    public void Update()
    {
       
    }

    void LaunchRocket(GameObject target, GameObject rocket, int speed)
    {
        rocket.GetComponent<Rigidbody2D>().AddForce((target.transform.position - rocket.transform.position) * speed, ForceMode2D.Impulse);
        rocket.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield)
        {

            if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color != Mycolor)
            {

                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
            }
            Destroy(gameObject);

        }
    }
}


public class RocketRepocitory : Repository
{
    public Rocket_controller rocket_Controller;

}
public class RocketInteractor : Interactor
{

    RocketRepocitory rocketRepocitory;
    public Rocket CreateRocket(Rocket_controller controller, Color_state color)
    {
        if (rocketRepocitory == null) Creat_Controller();

        GameObject gameObject = new GameObject();
        Rocket rocket = GameObject.Instantiate(Resources.Load<GameObject>("Rocket")).GetComponent<Rocket>();
        rocket.Mycolor = Color_state.blue;
        //rocketRepocitory.rocket_Controller.rockets.Add(rocket);
        return rocket;
    }
    public void Rocket_Atack(int count)
    {

    }
    public void Massage_about_atack()
    {
        Debug.Log("вас атакуют через две секунды");
    }
    public override void Initialize() => rocketRepocitory = Scene_1.s.repositorysBase.GetRepository<RocketRepocitory>();
    public void Creat_Controller()
    {
        if (rocketRepocitory.rocket_Controller == null)
        {
            GameObject gameObject = new GameObject("controller");
            rocketRepocitory.rocket_Controller = gameObject.AddComponent<Rocket_controller>();
        }
    }
}

public class Rocket_controller : MonoBehaviour
{
    private int count = 5;
    //public bool atack { get; set; }
    public List<Rocket> rockets;
    private void Start()
    {
        StartCoroutine(LaunchRocket());
    }

    public IEnumerator LaunchRocket()
    {
        for (int i = 0; i < count; i++)
        {
            Scene_1.s.interactorsBase.GetInteractor<RocketInteractor>().Massage_about_atack();
            yield return new WaitForSeconds(2);
            Scene_1.s.interactorsBase.GetInteractor<RocketInteractor>().CreateRocket(this, (Color_state)Random.Range(1, 4));
            yield return new WaitForSeconds(1);
        }
        Scene_1.Now_atack = false;
    }

}



