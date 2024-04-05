using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCanvas : MonoBehaviour
{
    public void ReturnButtonClicked()
    {
        EventHandler.CallAfterBagClosedEvent();
    }

}
