using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemToolTip : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;

    public void UpdateItemName(ItemName _itemName)
    {
        itemNameText.text = _itemName switch
        {
            ItemName.Key => "A Key",
            ItemName.Gun => "A Gun",
            _ => ""

        };

    }

}
