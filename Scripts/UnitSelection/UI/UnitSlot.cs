using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitSlot : MonoBehaviour
{
    public UnitName slotUnitName;
    public Image slotImage;


    public void SlotOnClicked()
    {
        EventHandler.CallUnitSlotOnClickedEvent(slotUnitName);
    }

    public void UpdateSlot(UnitDetails _unitDetails)
    {
        // Debug.Log("UpdateSlot!!");
        slotUnitName = _unitDetails.unitName;
        slotImage.sprite = _unitDetails.unitSprite;

    }


}
