using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_1 : MonoBehaviour
{
    void Start()
    {
        SC sc = new SC();
        Scene s = new Scene(sc);
        StartCoroutine(s.InitializeRoutine());
    }

    void Update()
    {
        
    }
}
