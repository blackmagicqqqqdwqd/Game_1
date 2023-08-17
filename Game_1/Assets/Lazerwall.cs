
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Lazerwall : MonoBehaviour
{
    [SerializeField] private int inacculacy;
    [SerializeField] private Transform point_1;
    [SerializeField] private Transform point_2;
    private int witght = 10;
    void Start()
    {
        //Change_color(Color.red); 
    }

    public void Lazer_line(int witght)
    {
        point_1.transform.position = transform.position + new Vector3(witght / 2, 0, 0);
        point_2.transform.position = transform.position - new Vector3(witght / 2, 0, 0);
    }

    void Update()
    {
        Lazer_line(witght);
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
        if (v.x > (1 + inacculacy) || v.x < (0 - inacculacy) || v.y > (1 + inacculacy) || v.y < (0 - inacculacy))
        {

            if (Scene_1.s.repositorysBase.GetRepository<LazerWallsRepository>().lazer_Wall_Contoller != null)
            {
                Scene_1.s.repositorysBase.GetRepository<LazerWallsRepository>().lazer_Wall_Contoller.lazers.Remove(this);
            }
            Destroy(gameObject);
        }
    }
    public void Change_color(Color color)
    {
        point_1.GetComponent<Te>().Change_color(color);
        point_2.GetComponent<Te>().Change_color(color);
    }
}
