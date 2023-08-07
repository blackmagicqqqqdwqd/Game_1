using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courotines : MonoBehaviour
{
    private static Courotines m_instance;
    private static Courotines instance
    {
        get
        {
            if(m_instance == null)
            {
                var go = new GameObject("[Coroutine MANAGER]");
                m_instance = go.AddComponent<Courotines>();
            }
            return m_instance;
            
        }
        set
        {

        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
