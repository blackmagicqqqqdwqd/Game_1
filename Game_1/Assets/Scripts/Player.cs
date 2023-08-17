using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject shield_1;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Shield shield;

    private Color color = Color.white;
    public Color Color
    {
        get
        {
            return color;
        }
        set
        {
       
            if (spriteRenderer != null)
            {
                if (value == Color.green)
                {
                   
                    color = value;
                    spriteRenderer.color = color;
                }
                else
                {
                   
                    color = Color.white;
                    spriteRenderer.color = color;
                }
            }
        
        }
    }

 

    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            if (invulnerable == false) hp = value; 
        }

    } 
    public bool invulnerable { get; set; }
    
  
    public CircleCollider2D circleCollider;
  
    private void Start()
    {
        if (shield_1 != null) shield = shield_1.GetComponent<Shield>(); 
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
   
        circleCollider = gameObject.GetComponent<CircleCollider2D>();

        hp = 4;
        Scene_1.s.interactorsBase.GetInteractor<HP_UIInteractor>().Show_HP();
     
        ChangeColor();
    }
    void Update() => ChangeColor();
    public void Died()
    {
        Scene_1.s.interactorsBase.GetInteractor<GAMEOVER_UIInteractor>().Show_goscreen();
        //GameObject.Find("Slave_window").SetActive(true);
    }
    public void Inveriable()
    {
        StartCoroutine(Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Invulnerable());
    }
    public void ChangeColor()
    {
        if (shield != null)
        {
          
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {

                shield.gameObject.SetActive(true);
                shield.Mycolor = (Color.magenta);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                shield.gameObject.SetActive(true);
                shield.Mycolor = (Color.red);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                shield.gameObject.SetActive(true);
                shield.Mycolor = (Color.blue);
            }
            else
            {
                shield.gameObject.SetActive(false);
                shield.Mycolor = (Color.white);

            }
        }
        

    }
    public Color GetShieldColor()
    {
        if (shield == null) return shield.Mycolor;
        else return Color.black;
    }
}
public class PlayerRepocitory : Repository
{
    
    public Player player;
   
    //public SpriteRenderer srPlayer { get; set; }
    //public SpriteRenderer spriteRenderer { get; set; }
    //public GameObject player { get; set; }
    //public GameObject shield { get; set; }
    //public CircleCollider2D ccPlayer { get; set; }
    //public Player player_sc { get; set; }
    //int hp;
    //public int HP
    //{
    //    get
    //     { return hp; }
    //    set
    //    {
    //        if (invulnerable == false)
    //        {
    //            hp = value;
    //
    //        }
    //    }
    //
    //}
    public override void Initialize()
    {
        //HP = 4;
        //   GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        //player = new GameObject();
        //srPlayer = player.AddComponent<SpriteRenderer>();
        //ccPlayer = player.AddComponent<CircleCollider2D>();
        //srPlayer.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
        //srPlayer.sortingOrder = 2;
        //player.name = "MainCircle";
        //player_sc = player.AddComponent<Player>();

        //shield = new GameObject();
        //spriteRenderer = shield.AddComponent<SpriteRenderer>();
        // shield.AddComponent<CircleCollider2D>();
        // spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Shield");
        // spriteRenderer.sortingOrder = 1;
        //shield.name = "Shield";
        //shield.transform.localScale = new Vector2(1.2f, 1.2f);   //?

        //shield.transform.SetParent(player.transform);
    }

}
public class PlayerInteractor : Interactor
{
    public PlayerRepocitory myR { get; set; }
    public override void Initialize()
    {
        myR = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>();
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        myR.player = player.GetComponent<Player>(); //public GameObject Player {private set; get; }
    }

    public void Get_Damag()
    {
        if (myR.player.invulnerable == false)
        {
            myR.player.HP -= 1;
            myR.player.Color = Color.green;
          
            Scene_1.s.interactorsBase.GetInteractor<HP_UIInteractor>().Set_HP(myR.player.HP);
          
            if (myR.player.HP == 0) myR.player.Died();
            //myR.player.invulnerable = true;
            myR.player.Inveriable();
            myR.player.invulnerable = true;
        }
        
    }
    public IEnumerator Invulnerable()
    {
       
        yield return new WaitForSeconds(2);
        
        myR.player.Color = Color.white;
        myR.player.invulnerable = false;
    }
    



}



