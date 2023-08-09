using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class GAMEOVER_UI : MonoBehaviour
{

}
public class GAMEOVER_UIRepocitory : Repository
{
    public GameObject Image { get; set; }
    public Vector3 point;
    public override void Initialize()
    {
        point = new Vector3(0, 0, 0);
        Image = new GameObject();
    }
}
public class GAMEOVER_UIInteractor : Interactor
{
    GAMEOVER_UIRepocitory repocitory;
    public override void Initialize()
    {
        repocitory = Scene_1.s.repositorysBase.GetRepository<GAMEOVER_UIRepocitory>();
    }
    public void Show_goscreen()
    {        
        GameObject.FindGameObjectWithTag("Slave").GetComponent<SpriteRenderer>().enabled = true; // ¬ключать паузу
    }
}
