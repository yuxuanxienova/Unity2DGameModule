


using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{  
    private GameObject target;
    private GameObject[] targetList;
    private float combatRange;
    private float searchRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        searchRange = animator.GetFloat("SearchRange");
        combatRange = animator.GetFloat("CombatRange");
        //------------InitializeTarget------------------
        if(animator.gameObject.tag == "Enemy")
        {
            target = GameObject.FindGameObjectWithTag("Player");

        }
        if(animator.gameObject.tag == "Player")
        {
            target = GameObject.FindGameObjectWithTag("Enemy");

        }
        //----------------------------------------------

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        TargetIdentification(animator);
        FlagFollowUpdate(animator);     
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Implement code that sets up animation IK (inverse kinematics)
    }
    private void TargetIdentification(Animator animator)
    {

        //-----------------------1.Identification Logic For Enemy AI----------------------------------
        if(animator.gameObject.tag == "Enemy")
        {
            targetList = GameObject.FindGameObjectsWithTag("Player"); 
            for (int i = 0; i < targetList.Length; i++)
            {
                float newTargetDis = DistanceToUnit( targetList[i], animator);

                if (newTargetDis < DistanceToUnit(target , animator))
                {

                    //renew target
                    target = targetList[i];
                }           
            }   
        }
        //--------------------------------------------------------------------------------------------  

        //-----------------------2.Identification Logic For Player AI----------------------------------
        if(animator.gameObject.tag == "Player")
        {
            targetList = GameObject.FindGameObjectsWithTag("Enemy"); 
            for (int i = 0; i < targetList.Length; i++)
            {
                float newTargetDis = DistanceToUnit( targetList[i], animator);
                if (newTargetDis < DistanceToUnit(target , animator))
                {
                    //renew target
                    target = targetList[i];
                }           
            }   
        }
        //--------------------------------------------------------------------------------------------  
    }

    private void FlagFollowUpdate(Animator animator)
    {
        //------------------Error Handle----------------------
        if(target == null){return;}
        //---------------------------------------------------
        Vector3 relativePos = target.transform.position - animator.transform.position; 
        float targetDis = Mathf.Abs(relativePos.magnitude);
        // UnityEngine.Debug.Log(targetDis);
        if(targetDis < searchRange)
        {
            animator.SetBool("FlagFollow", true);
        }

        if(targetDis < combatRange)
        {
            animator.SetBool("FlagCombat", true);

        }
    }
    private float DistanceToUnit( GameObject _target, Animator animator)
    {
        //----------------Error Handle----------------------
        if(_target == null){return 99999999;}
        //-------------------------------------------------
        
        Vector3 relativePos = _target.transform.position - animator.transform.position; 
        float targetDis = Mathf.Abs(relativePos.magnitude);

        return targetDis;
    }

}
