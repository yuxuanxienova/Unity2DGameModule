using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThrust : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    
    private float thrust;
    private IFaceDirection ifaceDirection;

    private Rigidbody2D rigidbody2D;
    
    //private Character_Base characterBase;

    private void Awake(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        ifaceDirection = GetComponent<IFaceDirection>();
        //characterBase = GatComponent<Character_Base>();
    }

    public void SetThrust(float _thrust ){       
        this.thrust = _thrust;       
    }

    private void FixedUpdate(){
        if(gameObject.tag == "Dead"){return;}
        float normalizedAngles = ifaceDirection.GetEulerAngles()[2];
        float angleInRadians = normalizedAngles * Mathf.Deg2Rad;
        Vector3 thrustVector = new Vector3( -Mathf.Sin(angleInRadians),Mathf.Cos(angleInRadians),0);
        // Debug.Log("thrustVector" + thrustVector);

        rigidbody2D.AddForce(thrustVector *  thrust * this.thrustSpeed);
                
        //characterBase.PlayMoveAnimm(velocityVector);

    }
}
