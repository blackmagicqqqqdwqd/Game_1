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
        s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(new Vector2(0, 0), 10, Color.red); ;
    }
    private void Update()
    {
        //if(Input.GetMouseButton(1)) s.repositorysBase.GetRepository<LazerWallsRepository>().lazers[0].Move(new Vector3(0, 10*Time.deltaTime, 0));

    }
}
