using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().ChangeColor(); //?
    }
}
public enum Color_state
{
    red,
    blue,
    purple,
    none
}
public class PlayerRepocitory:Repository
{
    public bool invulnerable { get; set; }
    public Color_state color { get; set; }
    public SpriteRenderer srPlayer { get; set; }
    public SpriteRenderer srShield { get; set; }
    public GameObject player { get; set; }
    public GameObject shield { get; set; }
    public CircleCollider2D ccPlayer { get; set; }
    public int HP {    get; set;}
    public override void Initialize() 
    {
        HP = 4;
        color = Color_state.none;

        player = new GameObject();
        srPlayer = player.AddComponent<SpriteRenderer>();
        ccPlayer =  player.AddComponent<CircleCollider2D>();
        srPlayer.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
        srPlayer.sortingOrder = 2;
        player.name = "MainCircle";
        player.AddComponent<Player>();

        shield = new GameObject();
        srShield = shield.AddComponent<SpriteRenderer>();
        shield.AddComponent<CircleCollider2D>();
        srShield.sprite = Resources.Load<Sprite>("Sprites/Shield");
        srShield.sortingOrder = 1;
        shield.name = "Shield";
        shield.transform.localScale = new Vector2(1.2f,1.2f);   //?

        shield.transform.SetParent(player.transform);
    }

}
public class PlayerInteractor:Interactor
{
    PlayerRepocitory myR;
    public override void Initialize()
    {
        myR = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>(); 
    }
    public void ChangeColor()
    {
        if (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2))
        {
            myR.srShield.enabled = true;
            myR.color = Color_state.purple;
            myR.srShield.color = Color.magenta;
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            myR.srShield.enabled = true;
            myR.color = Color_state.red;
            myR.srShield.color = Color.red;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            myR.srShield.enabled = true;
            myR.color = Color_state.blue;
            myR.srShield.color = Color.blue;
        }
        else
        {
            myR.srShield.enabled = false;
            myR.color = Color_state.none;
        }
    }
    public IEnumerator Get_Damag()
    {
        if (myR.invulnerable == false)
        {
            myR.srPlayer.color = Color.green;
            myR.invulnerable = true;
            myR.HP -= 1;
            if (myR.HP == 0) Died();
            Debug.Log('a');
            Scene_1.s.interactorsBase.GetInteractor<HP_UIInteractor>().Set_HP(myR.HP);
            yield return new WaitForSeconds(1);
            myR.srPlayer.color = Color.white;
            myR.invulnerable = false;
        }

    }
    public void Died()
    {
        Debug.Log(1);
        //GameObject.Find("Slave_window").SetActive(true);

    }
    
    
    
}


