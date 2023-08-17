using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer), typeof(SpriteRenderer))]

public class Memory_turrets : MonoBehaviour
{
    public Vector3 atack_position;
    Transform target;
    public enum State
    {
        show_color, atack, move, destroy, move_on_spawn_zone, wait,
    }
    
    public State my_state = State.wait;
    public  GameObject m;
    public int show_number;
    public List<Color> colors = new List<Color>();
    private float lenth;
    private Color color = Color.white;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    public Color Mycolor
    {
        get
        {

            return color;
        }
        set
        {
            if (lineRenderer != null)
            {
                if (value == Color.blue || value == Color.red || value == Color.magenta)
                {
                    color = value;
                    lineRenderer.startColor = value;
                    lineRenderer.endColor = value;
                }
                else if (value == Color.black)
                {
                    var v = new[] { Color.blue, Color.red, Color.magenta };
                    color = v[UnityEngine.Random.Range(0, v.Length)];
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                }
                else
                {
                    color = Color.white;
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                }
            }


        }
    }
    public void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        target = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform;
        Creet_Colors(1);
        //atack_position = gameObject.transform.position + new Vector3(0, 0, 0);
        my_state = State.wait;
    }
    public void Update()
    {
        
            switch (my_state)
            {
                case State.move_on_spawn_zone:
                    Move_on_atack();
                    break;
                case State.atack:
                    Atack(m.GetComponent<memory_turrets_controller>());
                    break;
            }
        
 
    }
    public void Creet_Colors(int count)
    {
        Color[] col = new[] { Color.red, Color.blue, Color.magenta };
        for (int i = 0; i < count; i++)
        {
            colors.Add(col[Random.Range(0, col.Length)]);
        }
    }
    public IEnumerator Show_color(memory_turrets_controller memory)
    {
       
        gameObject.GetComponent<SpriteRenderer>().color = colors[show_number];
        yield return new WaitForSeconds(1);
        spriteRenderer.color = Color.white;
        show_number++;
        yield return new WaitForSeconds(1);
        memory.process_underway = false;
    }
    IEnumerator wait(memory_turrets_controller memory)
    {
        yield return new WaitForSeconds(1);
        memory.process_underway = false;

    }
    public void Atack(memory_turrets_controller memory)
    {   
       
      
        lineRenderer.SetColors(colors[show_number], colors[show_number]);
        if (lineRenderer.positionCount != 2)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, transform.position);
        }
        lineRenderer.SetPosition(0, transform.position);

        if ((lineRenderer.GetPosition(1) - target.position).magnitude != 0)
        {
            lineRenderer.SetPosition(1, Vector3.MoveTowards(transform.position, target.position, lenth));
            lenth += 20 * Time.deltaTime;
        }


        if (Physics2D.Linecast(transform.position, lineRenderer.GetPosition(1)))
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, target.position);
            if (hit.transform.tag == "Player")
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                lineRenderer.SetPosition(1, target.position);
                show_number++;
                StartCoroutine(wait(memory));
                my_state = State.wait;
                lineRenderer.positionCount = 0;
                if (show_number >= colors.Count)
                {
                    my_state = State.destroy;
                }
                //transform.parent.GetComponent<Turret_controller>().atack = false;
            }
            else if (hit.transform.tag == "Shield")
            {
                // lineRenderer.SetPosition(1, hit.point);
                if (hit.transform.GetComponent<Shield>().Compare_Color(colors[show_number]))
                {
                    lineRenderer.SetPosition(1, hit.transform.position);
                    show_number++;
                    my_state = State.wait;
                    StartCoroutine(wait(memory));
                    lineRenderer.positionCount = 0;
                    if (show_number >= colors.Count)
                    {
                        my_state = State.destroy;
                    }
                    //transform.parent.GetComponent<Turret_controller>().atack = false;
                }
                else
                {
                    lineRenderer.SetPosition(1, hit.transform.position);
                    show_number++;
                    my_state = State.wait;
                    StartCoroutine(wait(memory));
                    lineRenderer.positionCount = 0;
                    Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                    if (show_number >= colors.Count)
                    {
                        my_state = State.destroy;
                    }
                }
            }
        }


    }
    public void Move_on_atack()
    {
        if (((transform.position - atack_position).magnitude != 0))
        {
            transform.position = Vector3.MoveTowards(transform.position, atack_position, 3 * Time.deltaTime);
        }
        else if ((transform.position - atack_position).magnitude == 0)
        {
            my_state = State.show_color;
        }
    }
}


