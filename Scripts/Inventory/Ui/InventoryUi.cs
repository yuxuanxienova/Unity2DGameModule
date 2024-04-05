using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    public Button leftButton, rightButton;

    public SlotUi slotUi;

    public int currentIndex;

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnUpdateUIEvent ( ItemDetails itemDetails, int index)
    {
        if (itemDetails ==  null)
        {
            slotUi.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }

        else
        {
            currentIndex = index;
            slotUi.SetItem(itemDetails);
        }
    }

}
