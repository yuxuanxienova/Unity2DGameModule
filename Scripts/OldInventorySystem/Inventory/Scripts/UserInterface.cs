using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using CodeMonkey.Utils;
using System;
//A copyof DisplayInventory
public abstract class UserInterface : MonoBehaviour{
    public GameObject EquipmentCanvas;
    
    public InventoryObject_SO inventory ;
    public Dictionary< GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();   
    // Start is called before the first frame update
    // public void Start(){ 
    // // Debug.Log("UserInterface Awake(); inventory: " + inventory);      
    // for (int i = 0; i < inventory.GetSlots.Length; i++)
    // {
    //     //every time we loop through all the slot and set the slot.parent to the interface the slot is belonging to 
    //     inventory.GetSlots[i].parent = this;
    //     // Debug.Log("registor!!");
    //     inventory.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;

        
    // }
    // CreateSlots();
    // AddEvent(gameObject, EventTriggerType.PointerEnter, delegate{ OnEnterInterface(gameObject);} );
    // AddEvent(gameObject, EventTriggerType.PointerExit, delegate{ OnExitInterface(gameObject);} ); 

    // //--------------------RegisterEventInHandler-------------------
    // EventHandler.AfterBagOpenEvent += OnAfterBagOpenEvent;
    // //-------------------------------------------------------------   
    // }





    public void InitializeInterface(){ 
        // Debug.Log("UserInterface Awake(); inventory: " + inventory);      
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            //every time we loop through all the slot and set the slot.parent to the interface the slot is belonging to 
            inventory.GetSlots[i].parent = this;
            // Debug.Log("registor!!");
            inventory.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;

            
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate{ OnEnterInterface(gameObject);} );
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate{ OnExitInterface(gameObject);} ); 

        //--------------------RegisterEventInHandler-------------------
        EventHandler.AfterBagOpenEvent += OnAfterBagOpenEvent;
        //-------------------------------------------------------------   
    }



    private void OnAfterBagOpenEvent()
    {
        slotsOnInterface.UpdateSlotDisplay();
        
    }

    private void OnAfterSlotUpdate(InventorySlot _slot)
    {
        // Debug.Log("UserInterface:OnAfterSlotUpdate");
        // Debug.Log("_slot.item: " + _slot.item );
        // Debug.Log("_slot.item.Id:" + _slot.item.Id);
        // Debug.Log("_slot.slotDisplay: " + _slot.slotDisplay );
        //if not empty
        if(_slot.item.Id >0){//changed! >=0

           

            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;//inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");

        }
        else{
            //empty slot, if slotDisplay not null , we should set it's sprite and texts to null
            
            if(_slot.slotDisplay != null){
            
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

        }
        

        slotsOnInterface.UpdateSlotDisplay();
        
    }

    // Update is called once per frame
    void Update(){
        //--------------辅助刷新---------------
        // if(Input.GetKeyDown(KeyCode.U))
        // {
        //     slotsOnInterface.UpdateSlotDisplay(); 
        // }
        //--------------------------------------
               
    }


    public abstract void CreateSlots();



    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        //The reason we creat this funciton is that we ddon;t have to creat all this code every time we want to create an event
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj){
        MouseData.slotHoveredOver = obj;
    }
    public void OnExit(GameObject obj){
        MouseData.slotHoveredOver = null;
    }
    public void OnEnterInterface(GameObject obj){
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj){
        MouseData.interfaceMouseIsOver = null;
    }
    public void OnDragStart(GameObject obj){
        
        MouseData.tempItemBeingDragded = CreatTempItem(obj);

    }
    public GameObject CreatTempItem(GameObject obj){
        GameObject tempItem = null;
        // Debug.Log("slotsOnInterface[obj].item.Id: " + slotsOnInterface[obj].item.Id);
        if(slotsOnInterface[obj].item.Id >0){//changed!! >=0
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(0.5f,1);
            // tempItem.transform.SetParent(transform.parent);
            //---------------newcode-----------
            tempItem.transform.SetParent(EquipmentCanvas.transform);
            // tempItem.transform.SetParent(transform.parent.parent.parent.parent);//Set Parent to the Equipment Canvas Need to Fix!!!

            // Debug.Log("transform.parent.parent.parent.parent" + transform.parent.parent.parent.parent);
            //----------------------------------
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
            //we need to set the raycast of the object we're creating to be false because otherwise this sprite image recy transform
            //is going to be in the way of our mouse and will never fire the event
            img.raycastTarget =false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj){
        // Debug.Log("OnDragEnd: obj:  " + obj);
        //After finish dragging:
        //1. destroy the temp item being dragged

        Destroy(MouseData.tempItemBeingDragded);

        //2. check if mouse is over an interface
        //2.1 if not over an interface we just destroy it
        
        if(MouseData.interfaceMouseIsOver == null){
            slotsOnInterface[obj].RemoveItem();
            //we don't want to execute the remaining code in this function 
            return;
        }
        if (MouseData.slotHoveredOver){
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            // Debug.Log("OnDragEnd! and slotIsHoveredOverAnItem");
            // Debug.Log("slotsOnInterface[obj], obj: " + obj);
            // Debug.Log("mouseHoverSlotData.slotDisplay" + mouseHoverSlotData.slotDisplay);
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
    }
    public void OnDrag(GameObject obj){

        Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
         
        if (MouseData.tempItemBeingDragded!= null){

            MouseData.tempItemBeingDragded.GetComponent<RectTransform>().position = currentMousePosition;

        }

    }


}








public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragded;
    public static GameObject slotHoveredOver;
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        // Debug.Log("Start UpdateSlotDisplay");
        // Debug.Log("_slotsOnInterface: " + _slotsOnInterface);
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface){
            // Debug.Log("_slot : " + _slot);
            //-----------Error Handle-----------
            if(_slot.Key == null){continue;}
            //----------------------------------

            //if not empty
            if(_slot.Value.item.Id >=0){

                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;//inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");

            }
            else{
                //empty slot
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";

            }
        }

    }
}
