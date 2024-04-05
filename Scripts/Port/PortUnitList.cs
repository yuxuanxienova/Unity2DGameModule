using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortUnitList : MonoBehaviour
{
    //the slot number shoulb be larger than the total number of units
    public List<GameObject> unitSlots = new List<GameObject>();


    private void OnEnable()
    {

        EventHandler.UnitSlotOnClickedEvent += OnUnitSlotOnClickedEvent;

        EventHandler.AfterAddUnitSlotClickedEvent += OnAfterAddUnitSlotClickedEvent;

        EventHandler.InPortCanvasOpenEvent += OnInPortCanvasOpenEvent;
    }
    private void OnDisable()
    {

        EventHandler.UnitSlotOnClickedEvent -= OnUnitSlotOnClickedEvent;

        EventHandler.AfterAddUnitSlotClickedEvent -= OnAfterAddUnitSlotClickedEvent;

        EventHandler.InPortCanvasOpenEvent -= OnInPortCanvasOpenEvent;
    }

    private void OnInPortCanvasOpenEvent() 
    {
        UpdateUnitSelectionUI();

    }

    private void OnUnitSlotOnClickedEvent(UnitName _unitName)
    {
        Debug.Log("OnUnitSlotClicked");
        UpdateUnitSelectionUI();

    }
    private void OnAfterAddUnitSlotClickedEvent()
    {
        // Debug.Log("OnAfterAddUnitSlotClickedEvent()");
        UpdateUnitSelectionUI();

    }


    public void UpdateUnitSelectionUI()
    {
        // Debug.Log("UpdateUnitSelectionUI()");
        // Debug.Log("UnitManager.Instance.unitObtainedList.Count" + UnitManager.Instance.unitObtainedList.Count);
        //-----------------Clear UI in All Slot----------------------
        for (int i = 0; i < unitSlots.Count; i++)
        {
            UnitDetails emptyUnitDetails = new UnitDetails();
            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(emptyUnitDetails);
        }

        //----------------Update UI in Slots--------------------------
        for (int i = 0; i < UnitManager.Instance.unitObtainedList.Count; i++)
        {
            // Debug.Log("UpdateUnitSelectionUI");
            UnitName _unitName = UnitManager.Instance.unitObtainedList[i];

            UnitDetails _unitDetails = UnitManager.Instance.unitData.GetUnitDetails(_unitName);

            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(_unitDetails);
        }

    }


}
