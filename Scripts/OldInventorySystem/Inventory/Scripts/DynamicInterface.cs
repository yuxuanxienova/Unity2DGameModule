using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    
    public GameObject inventoryPrefab;
    public int X_START;//-150
    public int Y_START;//371
    public int X_SPACE_BETWEEN_ITEM;//100
    public int NUMBER_OF_COLUM;//4
    public int Y_SPACE_BETWEEN_ITEMS;//100


    public override void CreateSlots(){
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            //bind an event to this prefab
            //delegate{ OnEnter(obj);} passing a function with parameter
            //Add five event using diifferent trigger type
            AddEvent(obj, EventTriggerType.PointerEnter, delegate{ OnEnter(obj);} );
            AddEvent(obj, EventTriggerType.PointerExit, delegate{ OnExit(obj);} );
            AddEvent(obj, EventTriggerType.BeginDrag, delegate{ OnDragStart(obj);} );
            AddEvent(obj, EventTriggerType.EndDrag, delegate{ OnDragEnd(obj);} );
            AddEvent(obj, EventTriggerType.Drag, delegate{ OnDrag(obj);} );
            inventory.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
            
        }

    }
    //Get the position of the slot
    private Vector3 GetPosition(int i){
        return new Vector3( X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUM)) , Y_START + (-Y_SPACE_BETWEEN_ITEMS * ( i/NUMBER_OF_COLUM)) , 0f);
    }
}
