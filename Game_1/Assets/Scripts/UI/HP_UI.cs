using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
   
}
public class HP_UIRepocitory : Repository
{
    public List<GameObject> Images { get; set; }
    public Vector3 start_point { get; set; }
    public override void Initialize()
    {
        start_point = new Vector3(-4.44f, 4.4f, 0);
        Images = new List<GameObject>();
    }
}
public class HP_UIInteractor:Interactor
{
    HP_UIRepocitory repocitory;
    public override void Initialize()
    {
        repocitory = Scene_1.s.repositorysBase.GetRepository<HP_UIRepocitory>();
    }
    public void Get_Hp()
    {

    }
    public void Show_HP()
    {
        for (int i = 0; i < Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory >().HP; i++)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            Image image = go.AddComponent<UnityEngine.UI.Image>();
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
            image.sprite = Resources.Load<Sprite>("Sprites/Heart");
            repocitory.Images.Add(go);
            if (i == 0) go.transform.position = repocitory.start_point;
            else
            {
                go.transform.position = repocitory.start_point + new Vector3(go.GetComponent<RectTransform>().sizeDelta.x * i, 0, 0); ;
            }

        }
    }
    public void Set_HP(int x) 
    {
        //0 1 2 3    2
       if (repocitory.Images.Count>0 && x >= 0)
        {
            int last_index = repocitory.Images.Count;
            for (int i = x; i < last_index; i++)
            {
                repocitory.Images[i].SetActive(false) ;
            }
            for (int i = 0; i < x; i++)
            {
                repocitory.Images[i].SetActive(true);
            }
        }




    }

}
