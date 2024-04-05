
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUnitBehaviour: StateMachineBehaviour
{
    private GameObject target;
    private GameObject[] targetList;
    private float searchRange;
    private float combatRange;
    private IMovePosition imovePosition;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        imovePosition = animator.gameObject.GetComponent<IMovePosition>();
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
        //-----------------Error Handle--------------------
        // Debug.Log("Start Logging");
        // Debug.Log("animaotr: " + animator);
        // Debug.Log("animator.gameObject: " + animator.gameObject);
        // Debug.Log("animator.gameObject.tag: " + animator.gameObject.tag);
        //---------------------------------------------------

        //----------------------Update Logic For Enemy AI-----------------------
        if(animator.gameObject.tag == "Enemy")
        {
            TargetIdentification(animator);
            FollowManeuver(animator); 
            FlagFollowUpdate(animator); 
        }
        //-----------------------------------------------------------------------

        //----------------------Update Logic For Player AI-----------------------
        if(animator.gameObject.tag == "Player")
        {
            TargetIdentification(animator);
            FlagFollowUpdate(animator); 
        }
        //-----------------------------------------------------------------------

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

    private void FollowManeuver(Animator animator)
    {
        //--------------Error Handle-----------------
        if (target == null) { return; }
        //-------------------------------------------

        if(animator.GetBool("FlagDecision"))
        {
            imovePosition.SetMovePosition(target.transform.position);
            animator.SetBool("FlagDecision", false); 
        } 

    }
    private void FlagFollowUpdate(Animator animator)
    {
        float targetDis = DistanceToUnit( target, animator);
        // UnityEngine.Debug.Log("TargetDis: " + targetDis);
        if(targetDis > searchRange)
        {
            animator.SetBool("FlagFollow", false);
        }
        if(targetDis < combatRange)
        {
            animator.SetBool("FlagCombat", true);

        }

    }

    private float DistanceToUnit( GameObject _target, Animator animator)
    {
        //----------------Error Handle--------------
        if(_target == null){return 9999999;}
        //------------------------------------------
        Vector3 relativePos = _target.transform.position - animator.transform.position; 
        float targetDis = Mathf.Abs(relativePos.magnitude);

        return targetDis;
    }
}
