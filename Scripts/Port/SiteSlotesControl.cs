using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteSlotesControl : MonoBehaviour
{
    public List<GameObject> SiteSlots = new List<GameObject>();

    private void OnEnable()
    {
        EventHandler.PortSiteSlotOnClickedEvent += OnPortSiteSlotOnClickedEvent;


    }

    private void OnDisable()
    {
        EventHandler.PortSiteSlotOnClickedEvent -= OnPortSiteSlotOnClickedEvent;

    }
    private void OnPortSiteSlotOnClickedEvent(GameObject _siteSlot) 
    {
        DeselectAllSlots();
        SiteSlots siteSlots = _siteSlot.GetComponent<SiteSlots>();
        siteSlots.isSelected = !siteSlots.isSelected;
        siteSlots.backGroundSelected.SetActive(siteSlots.isSelected);

    }

    private void DeselectAllSlots() 
    {
        //Debug.Log("Deselect!");
        for (int i = 0; i < SiteSlots.Count; i++)
        {
            SiteSlots[i].GetComponent<SiteSlots>().isSelected = false;
            SiteSlots[i].GetComponent<SiteSlots>().backGroundSelected.SetActive(false);
        }
    }

}
