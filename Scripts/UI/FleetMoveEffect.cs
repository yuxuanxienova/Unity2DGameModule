using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetMoveEffect : MonoBehaviour
{
    public List<UnitRTS> unitRTSList;

    public GameObject positionIndicator;
    
    // private void OnEnable()
    // {
    //     EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;

    // }
    // private void OnDisable()
    // {
    //     EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    // }

    // private void OnAfterSceneLoadedEvent()
    // {
    //     FleetMoveInMenu(positionIndicator.transform.position);

    // }

    private void Start()
    {
        FleetMoveInMenu(positionIndicator.transform.position);

    }


    private void FleetMoveInMenu(Vector3 moveToPosition)
    {
        Debug.Log("FleetMoveInMenu");
        

        //Calculate the Centre of all selected unit
        Vector3 sumUnitsPosition = new Vector3(0,0,0);
        int count = 0;
        foreach (UnitRTS unitRTS in  unitRTSList){
            sumUnitsPosition = sumUnitsPosition + unitRTS.GetPosition();
            count ++;
        }
        Vector3 centrePosition = sumUnitsPosition / count;
        



        //Move to the offested target position
        foreach (UnitRTS unitRTS in  unitRTSList)
        {
            Vector3  offset = unitRTS.GetPosition() - centrePosition;

            unitRTS.MoveTo(moveToPosition + offset);

        }

    }
}
