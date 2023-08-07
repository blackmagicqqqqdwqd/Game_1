using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lazerwall;
using static Turret;
using static Turret_Rot;

public class Scene_1 : MonoBehaviour
{
    public static Scene s;
    void Awake()
    {
        SC sc = new SC();
        s = new Scene(sc);
        StartCoroutine(s.InitializeRoutine());

    }
    private void Start()
    {
        //StartCoroutine(LazerWallsAtack());
        
        s.interactorsBase.GetInteractor<Turret_RotIterator>().Creat_Turret_Rot(-4, - 14,Color_state.red   );
      
        s.interactorsBase.GetInteractor<HP_UIInteractor>().Show_HP();
        //s.interactorsBase.GetInteractor<HP_UIInteractor>().Set_HP(0);
        //s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(new Vector2(0, -10), 10, Color_state.red); ;
    }
    private void Update()
    {
        //if(Input.GetMouseButton(1)) s.repositorysBase.GetRepository<LazerWallsRepository>().lazers[0].Move(new Vector3(0, 10*Time.deltaTime, 0));
        if (Input.GetMouseButton(0)) { s.interactorsBase.GetInteractor<TurretsInteractor>().DestroyTurren(s.repositorysBase.GetRepository<TurretsRepocitort>().turrets[0]); }
    }
    public IEnumerator LazerWallsAtack()
    {
        s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(new Vector2(0, -10), 4, Color_state.blue);
        yield return new WaitForSeconds(2);
        StartCoroutine(LazerWallsAtack());
    }
}
