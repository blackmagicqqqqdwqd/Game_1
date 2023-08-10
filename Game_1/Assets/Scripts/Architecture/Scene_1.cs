using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lazerwall;
using static Turret;
using static Turret_Rot;

public class Scene_1 : MonoBehaviour
{
    public static Scene s;
    int now_index = 0;
    public static bool Now_atack;
    void Awake()
    {
        SC sc = new SC();
        s = new Scene(sc);
        StartCoroutine(s.InitializeRoutine());

    }
    private void Start()
    {

        //s.interactorsBase.GetInteractor<Turret_RotIterator>().Creat_Turret_Rot(-4, - 14,Color_state.red   );
       // s.interactorsBase.GetInteractor<TurretsInteractor>().CircleAtack(3.5f,6,0);
       // s.interactorsBase.GetInteractor<TurretsInteractor>().CreatTurrent(2, 2, Color_state.random);
        s.interactorsBase.GetInteractor<HP_UIInteractor>().Show_HP();
        //s.interactorsBase.GetInteractor<GAMEOVER_UIInteractor>().Show_goscreen();
        //s.interactorsBase.GetInteractor<HP_UIInteractor>().Set_HP(0);
        //s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(new Vector2(0, -10), 10, Color_state.red); ;
    }
    private void Update()
    {
        string ss = "110";
       
        if (Now_atack == false )
        {
           
            switch (ss[now_index])
            {
                case '1':
                    if (Now_atack == false)
                    {
                        now_index++;
                        s.interactorsBase.GetInteractor<LazerWallsInteractor>().atack_sleepers(2, new Vector2(0, -10));
                    }

                    break;
                case '2':
                    if (Now_atack == false)
                    {
                        now_index++;
                        Scene_1.s.interactorsBase.GetInteractor<TurretsInteractor>().CircleAtack(4, 6, 0);
                    }

                    break;
                case '0':
                    Now_atack = false;
                    break;
              
            }
          
        }
        //if(Input.GetMouseButton(1)) s.repositorysBase.GetRepository<LazerWallsRepository>().lazers[0].Move(new Vector3(0, 10*Time.deltaTime, 0));
        //if (Input.GetMouseButton(0)) { s.interactorsBase.GetInteractor<TurretsInteractor>().DestroyTurren(s.repositorysBase.GetRepository<TurretsRepocitort>().turrets[0]); }
    }
    public IEnumerator LazerWallsAtack()
    {
        s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(new Vector2(0, -10), 4 , new Vector2(4,10), Color_state.random);
        yield return new WaitForSeconds(2);
        StartCoroutine(LazerWallsAtack());
    }
}
