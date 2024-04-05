using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    //DataMember
    private GameObject selectedGameObject;
    public IMovePosition movePosition;
    

    public Transform m_transform;

    private void Awake(){
        selectedGameObject = transform.Find("Selected").gameObject;
        movePosition = GetComponent<IMovePosition>();
        
        m_transform = GetComponent<Transform>();
        SetSelectedVisible(false);
    }
    //Functions
    public void SetSelectedVisible(bool visible)
    {
        //-------------ErrorHandle-------------------
        if(selectedGameObject == null){return;}
        //-------------------------------------------
        selectedGameObject.SetActive(visible);
    }
    public Vector3 GetPosition(){
        return m_transform.position;
    }

    public void MoveTo(Vector3 targetPosition){
        Debug.Log("[UnitRTS][MoveTo] targetPosition:" + targetPosition);
        movePosition.SetMovePosition(targetPosition);
        
        
    }
}
