using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionThrust : MonoBehaviour, IMovePosition{
    private Vector3 movePosition;
    private MoveThrust moveThrust;
    private IFaceDirection ifaceDirection;
    private Rigidbody2D rigidbody2D;
   

    private void Awake(){
        //initialize the target position
         movePosition = transform.position;
         moveThrust = GetComponent<MoveThrust>();
         ifaceDirection = GetComponent<IFaceDirection>();
         rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetMovePosition(Vector3 movePosition){
        
        this.movePosition = movePosition;
    }

    private void FixedUpdate(){

        if(gameObject.tag == "Dead"){return;}
        
        //Preventing Oscillation
        if((movePosition - transform.position).magnitude>0.1){
            // Vector3 moveDir = (movePosition - transform.position).normalized;
            moveThrust.SetThrust(1);
            ifaceDirection.SetFaceDirection(this.movePosition - transform.position);
            
        }
        else{//Reach the target! Velocity set to zero to prevent oscillate around
            rigidbody2D.velocity = new Vector3(0,0,0);
            moveThrust.SetThrust(0);

        }


    }

    public Vector3 GetMovePosition()
    {
        return movePosition;
    }

}
