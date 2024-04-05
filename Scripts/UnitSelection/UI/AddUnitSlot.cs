using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUnitSlot : MonoBehaviour
{
    [SerializeField]
    private UnitName  slotUnitName;
    public GameObject slotImageForObtainedUnit;
    public GameObject slotImageForNotObtainedUnit;
    public GameObject SelectionUI;

    public bool isSelected = false;
    public bool unitIsObtained = false;

    public void AddUnitSlotOnClicked()
    {
        //---------------ErrorHandle----------------
        if(slotUnitName == UnitName.Default){return;}
        //------------------------------------------
        EventHandler.CallAddUnitSlotOnClickedEvent(slotUnitName, isSelected);
        isSelected = !isSelected;
        SelectionUI.SetActive(isSelected);
        EventHandler.CallAfterAddUnitSlotClickedEvent();
    }

    public void UpdateUnitObtainedState()
    {
        
    }




}
