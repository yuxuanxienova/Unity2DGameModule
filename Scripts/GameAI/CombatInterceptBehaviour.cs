using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInterceptBehaviour : StateMachineBehaviour
{
    private GameObject target;
    private Rigidbody2D targetRigidbody2D; 
    private GameObject[] targetList;
    private float combatRange;
    private float combatLowerRange;
    private IMovePosition imovePosition;
    private IndicatorControl indicatorControl;
    private FireModule fireModule;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireModule = animator.GetComponent<FireModule>();
        imovePosition = animator.gameObject.GetComponent<IMovePosition>();
        combatRange = animator.GetFloat("CombatRange");
        combatLowerRange = animator.GetFloat("CombatLowerRange");
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


        if (target == null) { return; }

        targetRigidbody2D = target.GetComponent<Rigidbody2D>();
        //-----------------disable the indicator------------------
        // indicatorControl = animator.gameObject.GetComponent<IndicatorControl>();
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //-----------------------Error Handle--------------------------

        //-------------------------------------------------------------

        //----------------------Update Logic For Enemy AI-----------------------
        if(animator.gameObject.tag == "Enemy")
        {
            TargetIdentification( animator);
            FlagCombatUpdate( animator );
            CombatInterceptManeuver(animator);
            CombatFire(animator);
        }
        //-----------------------------------------------------------------------

        //----------------------Update Logic For Player AI-----------------------
        if(animator.gameObject.tag == "Player")
        {
            TargetIdentification( animator);
            FlagCombatUpdate( animator );
            CombatFire(animator);
            // LoggingCurrentState(animator);
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

    private void CombatFire(Animator animator)
    {
        if(target == null){return;}
        if(target.tag == "Dead"){return;}

        fireModule.FireProjectile(target.transform.position);
    
    }


    private void CombatInterceptManeuver(Animator animator)
    {
        //-----------------------1.Maneuver Logic For Enemy AI----------------------------------
        if(animator.gameObject.tag == "Enemy"){
            if(animator.GetBool("FlagDecision"))
            {
                //
                //  Vector3 relativePos =  animator.transform.position - target.transform.position;
                //
                Vector3 targetVelocity = targetRigidbody2D.velocity;
                Vector3 offsetVec = new Vector3( 0 , 0 , 0);

                Vector3 targetPosPredict = target.transform.position + targetVelocity  * 10;
                Vector3 relativePos =  animator.transform.position - targetPosPredict;

                //----------disable the indicator----------------
                //  indicatorControl.SetTargetPosPredict(targetPosPredict);

                float angleRelativePosVec = NormalizeAngle(Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90f);
                float angleTargetVelocityVec = NormalizeAngle(Mathf.Atan2(targetVelocity.y, targetVelocity.x) * Mathf.Rad2Deg - 90f);

                float angleBetween  =NormalizeAngle( angleTargetVelocityVec - angleRelativePosVec);

                if (angleBetween > 0)
                {
                    offsetVec = RotationAboutZ( relativePos.normalized , 90 );

                }
                else
                {
                    offsetVec = RotationAboutZ( relativePos.normalized , -90 );
                }
                imovePosition.SetMovePosition( targetPosPredict + offsetVec *  combatLowerRange );
                animator.SetBool("FlagDecision", false); 
            }
        } 
        //-----------------------------------------------------------------------------------------
        //-----------------------1.Maneuver Logic For Player AI----------------------------------
        //---------------------------------------------------------------------------------------

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
                if (newTargetDis < DistanceToUnit(target , animator) || target.gameObject.tag=="Dead")
                {
                    //renew target
                    target = targetList[i];
                    OnCallTargetChanged();
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
                    OnCallTargetChanged();
                }           
            }   
        }
        //--------------------------------------------------------------------------------------------         
    }

    private void FlagCombatUpdate(Animator animator)
    {
        float targetDis = DistanceToUnit( target, animator);
        if(targetDis > combatRange)
        {
            animator.SetBool("FlagCombat", false);
        }

    }
    private float DistanceToUnit( GameObject _target, Animator animator)
    {
        


        //----------------Error Handle--------------
        if(_target == null){return 9999999;}
        if(_target.tag == "Dead"){return 9999999;}
        //------------------------------------------

        Vector3 relativePos = _target.transform.position - animator.transform.position; 
        float targetDis = Mathf.Abs(relativePos.magnitude);

        return targetDis;
    }

    private void OnCallTargetChanged()
    {
        targetRigidbody2D = target.GetComponent<Rigidbody2D>();

    }
    float NormalizeAngle(float angle)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }
        else if (angle < -180f)
        {
            angle += 360f;
        }
        return angle;
    }
    private Vector3 RotationAboutZ(Vector3 _vec , float degree)
    {
        
        float rad = Mathf.Deg2Rad * degree; 
        float xOut = Mathf.Cos(rad) * _vec.x - Mathf.Sin(rad) * _vec.y;
        float yOut = Mathf.Sin(rad) * _vec.x + Mathf.Cos(rad) * _vec.y;
        float zOut = _vec.z;

        return new Vector3(xOut , yOut , zOut);

    }

    private void LoggingCurrentState(Animator animator)
    {
        //-----------------------2.Identification Logic For Player AI----------------------------------
        if(animator.gameObject.tag == "Player")
        {
            Debug.Log("Target: " + target);
        }
        //--------------------------------------------------------------------------------------------     

    }
}
