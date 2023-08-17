using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class Rocket : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform target;
    private Color color = Color.white;
    public Color Mycolor
    {
        get
        {

            return color;
        }
        set
        {
            if (spriteRenderer != null)
            {
                if (value == Color.blue || value == Color.red || value == Color.magenta)
                {
                  
                    color = value;
                    spriteRenderer.color = value;
                    spriteRenderer.color = value;
                }
                else if (value == Color.black)
                {
                    var v = new[] { Color.blue, Color.red, Color.magenta };
                    color = v[UnityEngine.Random.Range(0, v.Length)];
                    spriteRenderer.color = color;
                }
                else
                {
                    color = Color.white;
                    spriteRenderer.color = Color.white;
                }
            }
        }
    }

    public void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Mycolor = Color.red;
        //Debug.Log(Mycolor.ToString());
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
        if (collision.gameObject.tag == "Shield" && collision.gameObject.GetComponent<Shield>() != null)
        {

           // if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>(). != Mycolor) Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();            
            if (!collision.gameObject.GetComponent<Shield>().Compare_Color(Mycolor)) 
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
           
        }
        if (collision.gameObject.tag == "Player)")
            {
            Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
        }
        Destroy(gameObject);
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



