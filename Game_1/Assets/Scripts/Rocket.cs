
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

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
                        spriteRenderer.color = UnityEngine.Color.red;
                        break;
                    case Color_state.blue:
                        color = Color_state.blue;
                        spriteRenderer.color = UnityEngine.Color.blue;
                        break;
                    case Color_state.purple:
                        color = Color_state.purple;
                        spriteRenderer.color = UnityEngine.Color.magenta;
                        break;
                    default:
                        color = Color_state.red;
                        break;
                }
            }
        }
    }
    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Mycolor = Color_state.red;
        target = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform;
        switch (Random.Range(0, 2))
        {
            case 0:
                transform.position = new Vector3(target.position.x + 11 * Mathf.Cos(360 *  Mathf.Deg2Rad + Random.Range(-75,75) * Mathf.Deg2Rad), target.position.y + 11 * Mathf.Sin(360 * Mathf.Deg2Rad + Random.Range(-90,90) * Mathf.Deg2Rad), 0);
                break;
            case 1:
                transform.position = new Vector3(target.position.x + 11 * Mathf.Cos(360 * Mathf.Deg2Rad + Random.Range(105, 255) * Mathf.Deg2Rad), target.position.y + 11 * Mathf.Sin(360 * Mathf.Deg2Rad + Random.Range(90, 270) * Mathf.Deg2Rad), 0);
                break;
        }
        LaunchRocket(target.gameObject, gameObject, 3);
    }
    //Тут был update
    void LaunchRocket(GameObject target, GameObject rocket, int speed)
    {
        rocket.GetComponent<Rigidbody2D>().AddForce((target.transform.position - rocket.transform.position) * speed, ForceMode2D.Impulse);
        rocket.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield)
        {
            if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color != Mycolor) Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();            
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
            Color_state color = (Color_state)Random.Range(1, 4);
            GameObject.Find("massage").GetComponent<UI_Massage>().Massage_ON(color);
            //Scene_1.s.interactorsBase.GetInteractor<RocketInteractor>().Massage_about_atack();
            yield return new WaitForSeconds(4);
            Scene_1.s.interactorsBase.GetInteractor<RocketInteractor>().CreateRocket(this, color);
            yield return new WaitForSeconds(1);
        }
        Scene_1.Now_atack = false;
    }
}



