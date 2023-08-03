using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntetactor : Interactor
{
    Repository repository;
    
    
    public override void Initialize()
    {
        Debug.Log(1);
    }
    public void Jump()
    {
        Debug.Log("jump");
    }
}
