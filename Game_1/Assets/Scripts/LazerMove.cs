using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class LazerMove : MonoBehaviour
{
    public Lazerwall lazerwall;
    void Update()
    {
        lazerwall.Move2(transform.position);
    }
}
