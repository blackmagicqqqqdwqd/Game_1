
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Te : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private LineRenderer lineRenderer;
    private Color color;
    void Start()
    {
        lineRenderer.positionCount = 2;
    }

    public void Change_color(Color color)
    {
        this.color = color;
        lineRenderer.endColor = color;
        lineRenderer.startColor = color;
    }
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        if(Physics2D.Linecast(transform.position, center.position))
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, center.position) ;
            if(hit.transform.tag == "Player")
            {
                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                lineRenderer.SetPosition(1, center.position);
            }
            else if(hit.transform.tag == "Shield")
            {
               lineRenderer.SetPosition(1, hit.point);
               if (hit.transform.GetComponent<Shield>().Compare_Color(color))
                {
                    lineRenderer.SetPosition(1, center.position);
                }
               else Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
            }
        }
        else
        {
            lineRenderer.SetPosition(1, center.position);
        }
    }
}
