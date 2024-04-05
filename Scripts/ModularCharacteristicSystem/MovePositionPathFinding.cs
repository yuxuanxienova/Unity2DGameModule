using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovePositionPathFinding : MonoBehaviour, IMovePosition
{   //Not Implemented Yet!!!!!!!!!!!!!
    private Vector3 movePosition;

    public void SetMovePosition(Vector3 movePosition){
        this.movePosition = movePosition;
    }
    public Vector3 GetMovePosition()
    {
        return this.movePosition;
    }

    private void Update(){
        Vector3 moveDir = (movePosition - transform.position).normalized;
        GetComponent<IMoveVelocity>().SetVelocity(moveDir);
    }




}
