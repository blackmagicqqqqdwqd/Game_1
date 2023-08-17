
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer) , typeof(CircleCollider2D))]
public class Shield : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField]  bool change_spriter;
    [SerializeField] SpriteRenderer Sprite_Renderer
    {
        get { 
            if ( sprite_renderer == null ) sprite_renderer = GetComponent<SpriteRenderer>();
            return sprite_renderer; 
        }
        set {  }
    }
    private Color color = Color.white;
    public Color Mycolor
    {
        get
        {
            return color;    
        }
        set
        {
            if (Sprite_Renderer != null)
            {
                if (value == Color.blue || value == Color.red || value == Color.magenta)
                {
                    color = value;
                    if (change_spriter) Sprite_Renderer.color = value;
                }
                else
                {
                    color = Color.white;
                    if (change_spriter) Sprite_Renderer.color = Color.white;
                }
            }
        }
    }
    void Start()
    {
        Mycolor = Color.white;
        gameObject.tag = "Shield";
    }
    public bool Compare_Color(Color color)
    {
        if (color == Mycolor && Mycolor != Color.white) return true;
        else if (color == Color.black) return true;
        else return false;
    }

}
