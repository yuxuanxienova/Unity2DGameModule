using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForce : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    

    private Vector3 forceVector;
    private Rigidbody2D rigidbody2D;
    
    //private Character_Base characterBase;

    private void Awake(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        //characterBase = GatComponent<Character_Base>();
    }

    public void SetForce(Vector3 _forceVector ){
        
        this.forceVector = _forceVector;
        
    }

    private void FixedUpdate(){

        rigidbody2D.AddForce(this.forceVector *  this.thrustSpeed);
        
        

        
        //characterBase.PlayMoveAnimm(velocityVector);

    }
}
