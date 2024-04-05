using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public UnitName unitName;
    //creat an array to contain all the slot
    public GameObject[] slots;


    public override void CreateSlots()
    {
        //Debug.Log("StaticInterface CreateSlots() ; Name: " + unitName);
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        
        
        for (int i = 0; i < slots.Length; i++)
        {
            
            
            var obj = slots[i];
            //bind an event to this prefab
            //delegate{ OnEnter(obj);} passing a function with parameter
            //Add five event using diifferent trigger type
            AddEvent(obj, EventTriggerType.PointerEnter, delegate{ OnEnter(obj);} );
            AddEvent(obj, EventTriggerType.PointerExit, delegate{ OnExit(obj);} );
            AddEvent(obj, EventTriggerType.BeginDrag, delegate{ OnDragStart(obj);} );
            AddEvent(obj, EventTriggerType.EndDrag, delegate{ OnDragEnd(obj);} );
            AddEvent(obj, EventTriggerType.Drag, delegate{ OnDrag(obj);} );

            //==============Initialize all slotDisplay=====================
            inventory.GetSlots[i].slotDisplay = obj;
            //=============================================================
            
            //add it to the display item
            slotsOnInterface.Add(obj,inventory.GetSlots[i]);
            
        }
       
    }

    public void RelinkSlotDisplay()
    {
        for (int i = 0; i < slots.Length; i++)
        {           
            var obj = slots[i];
            inventory.GetSlots[i].slotDisplay = obj;          
        }
    }

    // ----------------Test Method---------------------
    public void LogSlotDisplay()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(" inventory.GetSlots[i].slotDisplay : " +  inventory.GetSlots[i].slotDisplay);

        }

    }
}
