using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{
    private Vector3 movePosition;
    private IMoveVelocity imoveVelocity;

    private void Awake(){
        //initialize the target position
         movePosition = transform.position;
         imoveVelocity = GetComponent<IMoveVelocity>();
    }

    public void SetMovePosition(Vector3 movePosition){
        
        this.movePosition = movePosition;
    }

    public Vector3 GetMovePosition()
    {
        return movePosition;
    }

    private void FixedUpdate(){
        
        //Preventing Oscillation
        if((movePosition - transform.position).magnitude>0.1){
            Vector3 moveDir = (movePosition - transform.position).normalized;
            imoveVelocity.SetVelocity(moveDir);
        }
        else{//Reach the target! Velocity set to zero to prevent oscillate around
            imoveVelocity.SetVelocity(new Vector3(0,0,0));
        }

    }

}
