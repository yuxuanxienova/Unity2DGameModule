using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour , IMoveVelocity
{
    [SerializeField] private float moveSpeed;

    private Vector3 velocityVector;

    
    //private Character_Base characterBase;

    private void Awake(){

        //characterBase = GatComponent<Character_Base>();
    }

    public void SetVelocity(Vector3 velocityVector){
        
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate(){
        transform.position += velocityVector * moveSpeed * Time.deltaTime;
        //characterBase.PlayMoveAnimm(velocityVector);

    }
}
