using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerMovementMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(1)){
            GetComponent<MovePositionDirect>().SetMovePosition(UtilsClass.GetMouseWorldPosition());
        }
        
    }
}
