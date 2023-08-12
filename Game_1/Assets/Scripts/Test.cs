using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        animator.SetBool("blue_atack", false);
        animator.SetBool("red_atack", false);
        animator.SetBool("magenta_atack", false);
        animator.gameObject.GetComponent<Turret>().MyState = Turret.State.rotation_and_shoot;
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Debug.Log(stateInfo.tagHash);
    }
}
