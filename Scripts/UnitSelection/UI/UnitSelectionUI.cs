using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionUI : MonoBehaviour
{
    //the slot number shoulb be larger than the total number of units
    public List<GameObject> unitSlots = new List<GameObject>();
    public GameObject equipmentScreensContent;


    private void OnEnable()
    {
        EventHandler.AfterBagOpenEvent  += OnAfterBagOpenEvent;

        EventHandler.UnitSlotOnClickedEvent += OnUnitSlotOnClickedEvent;

        EventHandler.AfterAddUnitSlotClickedEvent += OnAfterAddUnitSlotClickedEvent;
    }
    private void OnDisable()
    {
        EventHandler.AfterBagOpenEvent  -= OnAfterBagOpenEvent;

        EventHandler.UnitSlotOnClickedEvent -= OnUnitSlotOnClickedEvent;

        EventHandler.AfterAddUnitSlotClickedEvent -= OnAfterAddUnitSlotClickedEvent;
    }

    private void OnAfterBagOpenEvent()
    {
        UpdateUnitSelectionUI();
    }
    private void OnUnitSlotOnClickedEvent(UnitName _unitName)
    {
        DeactivateAllEquipmentScreens();
        ActiveEquipmentScreenWithName(_unitName);

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
        for ( int i =0 ; i < unitSlots.Count ; i++)
        {
            UnitDetails emptyUnitDetails = new UnitDetails();
            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(emptyUnitDetails);           
        }

        //----------------Update UI in Slots--------------------------
        for ( int i =0 ; i < UnitManager.Instance.unitObtainedList.Count ; i++)
        {
            // Debug.Log("UpdateUnitSelectionUI");
            UnitName _unitName = UnitManager.Instance.unitObtainedList[i];

            UnitDetails _unitDetails = UnitManager.Instance.unitData.GetUnitDetails(_unitName);

            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(_unitDetails);
        }

    }

    
    public void DeactivateAllEquipmentScreens()
    {
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }

    public void ActiveEquipmentScreenWithName(UnitName _unitName)
    {
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            if (child.GetComponent<StaticInterface>().unitName == _unitName)
            {
                child.gameObject.SetActive(true);
            }
        }

    }
    //-----------------------------GM Method----------------------------------------
    public void GM_UpdateUnitSelectionUI()
    {   
        // Debug.Log("UnitManager.Instance.GM_AllUnitList.Count" + UnitManager.Instance.GM_AllUnitList.Count);
        //-----------------Clear UI in All Slot----------------------
        for ( int i =0 ; i < unitSlots.Count ; i++)
        {
            UnitDetails emptyUnitDetails = new UnitDetails();
            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(emptyUnitDetails);           
        }

        //----------------Update UI in Slots--------------------------
        for ( int i =0 ; i < UnitManager.Instance.GM_AllUnitList.Count ; i++)
        {
            // Debug.Log("UpdateUnitSelectionUI");
            UnitName _unitName = UnitManager.Instance.GM_AllUnitList[i];

            UnitDetails _unitDetails = UnitManager.Instance.unitData.GetUnitDetails(_unitName);

            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(_unitDetails);
        }

    }
    //----------------------------------Search Window-----------------------------
    public void UpdateScanUI()
    {
        

        //-----------------Clear UI in All Slot----------------------
        for ( int i =0 ; i < unitSlots.Count ; i++)
        {
            UnitDetails emptyUnitDetails = new UnitDetails();
            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(emptyUnitDetails);           
        }

        //----------------Update UI in Slots--------------------------
        for ( int i =0 ; i < UnitManager.Instance.scannedUnitList.Count ; i++)
        {
            // Debug.Log("UpdateUnitSelectionUI");
            UnitName _unitName = UnitManager.Instance.scannedUnitList[i];

            UnitDetails _unitDetails = UnitManager.Instance.unitData.GetUnitDetails(_unitName);

            unitSlots[i].GetComponent<UnitSlot>().UpdateSlot(_unitDetails);
        }


    }

}
