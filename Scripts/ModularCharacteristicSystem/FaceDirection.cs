using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour,IFaceDirection{
    private Vector3 eulerAngles;
    private IMoveVelocity imoveVelocity;
    private Transform m_transform;

    private void Awake(){
        //initialize the Direction
        m_transform = GetComponent<Transform>();
        eulerAngles = m_transform.eulerAngles;
        imoveVelocity = GetComponent<IMoveVelocity>();
         
    }
    public void SetFaceDirection( Vector3 directionVector){
        float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg - 90f;
        this.eulerAngles = new Vector3 (0 , 0 ,angle);
    }

    // Update is called once per frame
    void Update(){
        m_transform.eulerAngles = eulerAngles;
        
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
}
