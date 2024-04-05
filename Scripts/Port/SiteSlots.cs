using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SiteSlots : MonoBehaviour
{
    public PortSiteName portSiteName;
    public UnitName slotUnitName;
    public Image slotImage;
    public bool isSelected;
    public GameObject backGroundSelected;

    private void Awake()
    {
        isSelected = false;
        backGroundSelected.SetActive(false);
    }

    public void SlotOnClicked()
    {
        EventHandler.CallPortSiteSlotOnClickedEvent(gameObject);
    }

    public void UpdateSlot(UnitDetails _unitDetails)
    {
        // Debug.Log("UpdateSlot!!");
        slotUnitName = _unitDetails.unitName;
        slotImage.sprite = _unitDetails.unitSprite;
    }
}
