using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDirection : MonoBehaviour,IFaceDirection{
    [SerializeField] public float turnSpeed;
    private Vector3 targetEulerAngles;
    private IMoveVelocity imoveVelocity;
    
    private Transform m_transform;
    private Rigidbody2D m_rigidbody2D;

    private void Awake(){
        //initialize the Direction
        m_transform = GetComponent<Transform>();
        targetEulerAngles = m_transform.eulerAngles;
        imoveVelocity = GetComponent<IMoveVelocity>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        
         
    }
    public void SetFaceDirection( Vector3 directionVector){
        float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg - 90f;
        this.targetEulerAngles = new Vector3 (0 , 0 ,NormalizeAngle(angle));
    }
    public Vector3 GetEulerAngles(){

        return NormalizedEulerAngles(m_transform); 
    }

    Vector3 NormalizedEulerAngles(Transform transform)
    {
        Vector3 euler = transform.eulerAngles;
        euler.x = NormalizeAngle(euler.x);
        euler.y = NormalizeAngle(euler.y);
        euler.z = NormalizeAngle(euler.z);
        return euler;
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

    // Update is called once per frame
    void Update(){
        if(gameObject.tag == "Dead"){return;}
        Vector3 normalizedEulerAngles = NormalizedEulerAngles(m_transform);
        float turnDir = NormalizeAngle(targetEulerAngles[2] - normalizedEulerAngles[2]);

        //Preventing Oscillation
        if(Math.Abs((turnDir))>0.2){
            
            m_rigidbody2D.AddTorque(Math.Sign(turnDir) * turnSpeed);
            // Debug.Log(" targetEulerAngles"+targetEulerAngles);
            // Debug.Log(" normalizedEulerAngles"+( normalizedEulerAngles));
            // Debug.Log("turnDir" + turnDir);
            
        }
        else{//Reach the target! Velocity set to zero to prevent oscillate around
            m_rigidbody2D.AddTorque(0);
            m_rigidbody2D.angularVelocity = 0 ;
            
        }
        
    }
}
