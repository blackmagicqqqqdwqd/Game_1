using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massage_anim : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.gameObject.GetComponent<UI_Massage>().Massage_OFF();
    }
  
}
