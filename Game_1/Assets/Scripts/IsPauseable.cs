using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHavePause
{
    public void Activate();

    public void Deactivate();
 
}
public enum Color_state
{
    red = 1,
    blue,
    purple,
    none,
    random
}
