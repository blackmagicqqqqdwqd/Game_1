using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Turret;

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
        s.interactorsBase.GetInteractor<TurretsInteractor>().CreatTurrent(2, 2); 
    }
    void Update()
    {
        
    }
}
