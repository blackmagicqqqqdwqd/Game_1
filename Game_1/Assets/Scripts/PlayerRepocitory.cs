using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepocitory : Repository
{
    int damag;
    int hp;
    public override void Initialize()
    {
        damag = 10;
        hp = 10;
    }
}
