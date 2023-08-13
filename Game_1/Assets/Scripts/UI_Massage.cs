using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UI_Massage : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();    
    }
    public void Massage_ON(Color_state color)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.color = Color.red;
        animator.SetBool("massage_on", true);
    }
    public void Massage_OFF() => StartCoroutine(wait());
    IEnumerator wait()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(1);
        yield return new WaitForSeconds(2);
        animator.SetBool("massage_on", false);
        spriteRenderer.enabled = false;
    }

}
