using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataks : MonoBehaviour
{
    //string s = "120";
    public int now_index;
    List<string> list = new List<string>() { "1", "2", "0" };
    bool load;
    void Start()
    {
        load = true;
        //StartCoroutine(atack());
    }

    void Update()
    { 
      
     
    }/*
    public IEnumerator atack()
    {
        while   (load)
        {
            if (Scene_1.Now_atack == false)
            {
                var v = list[now_index];
                Debug.Log(now_index);
              
                    switch (v)
                    {
                        case "1":
                            if (Scene_1.Now_atack == false)
                            {
                            now_index = 1;
                                StartCoroutine(Scene_1.s.interactorsBase.GetInteractor<LazerWallsInteractor>().atack_sleepers(2, new Vector2(0, -10)));
                            }
                               
                            break;
                        case "2":
                            if (Scene_1.Now_atack == false)
                            {
                            now_index++;
                            StartCoroutine(Scene_1.s.interactorsBase.GetInteractor<TurretsInteractor>().CircleAtack(4, 6, 0));
                            }
                          
                            break;
                        case "0":
                            load = false;
                            break;
                        default:
                            load = false;
                            break;
                    }



            }
            yield return null;
        }
       
    }*/
}
