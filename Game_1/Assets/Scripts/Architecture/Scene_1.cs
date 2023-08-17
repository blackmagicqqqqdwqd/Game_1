using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_1 : MonoBehaviour
{
    private string map_key = "12120";
    public static Scene s;
    private int now_index = 0;
    public static bool Now_atack;

    void Awake()
    {
        // не трогать
        SC sc = new SC();
        s = new Scene(sc);
        StartCoroutine(s.InitializeRoutine());
        //
        //map_key = Random_map(1,4);
    }
    private void Start()
    {
        s.interactorsBase.GetInteractor<HP_UIInteractor>().Show_HP();
    }
    private void Update()
    {
        if (Now_atack == false)
        {
            switch (map_key[now_index])
            {
                case '1':
                    {
                        Now_atack = true;
                        now_index++;
                        switch (Random.Range(0, 2))
                        {
                            case 1:
                                s.interactorsBase.GetInteractor<LazerWallsInteractor>().atack_decreasing(new Vector2(0, -15));
                                break;
                            case 0:
                                s.interactorsBase.GetInteractor<LazerWallsInteractor>().atack_sleepers(Random.Range(5, 10), new Vector2(0, -15));
                                break;
                        }
                    }
                    break;
                case '2':
                    if (Now_atack == false)
                    {
                        Now_atack = true;
                        now_index++;
                        s.interactorsBase.GetInteractor<TurretsInteractor>().CircleAtack(4, Random.Range(2, 8), 0);
                    }
                    break;
                case '3':
                    if (Now_atack == false)
                    {
                        now_index++;
                        Now_atack = true;
                        s.interactorsBase.GetInteractor<RocketInteractor>().Creat_Controller();
                    }
                    break;
                case '0':
                    Now_atack = true;
                    break;
                case '4':
                    if (Now_atack == false)
                    {
                        now_index++;
                        Now_atack = true;
                        s.interactorsBase.GetInteractor<Memory_turrets_Interactor>().Atack();
                    }
                    break;
            }

        }
        if (Input.GetKeyDown(KeyCode.N)) StartCoroutine(s.repositorysBase.GetRepository<LazerwebRepocitory>().lazerwebController.LazerwebSpawn());
        if (Input.GetKeyDown(KeyCode.M)) s.interactorsBase.GetInteractor<LazerwebInteractor>().CreateController();
    }
    private string Random_map(int min, int max) // включительно
    {
        string map_key = "";
        for (int i = 1; i < 10; i++)
        {
            map_key += Random.Range(min, max + 1).ToString();
        }
        map_key += "0";
        return map_key;
    }
}
