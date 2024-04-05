using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

public enum InterfaceType
{
    Inventory,
    Equipment,
    Chest
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject_SO : ScriptableObject{
    public string savePath;

    public ItemDatabaseObject database;
    public InterfaceType type;
    public Inventory Container;
    public InventorySlot[] GetSlots {get{return Container.Slots;}}
   
    public bool AddItem(Item _item, int _amount){
        if (EmptySlotCount <= 0){
            return false;
        }
        InventorySlot slot = FindItemOnInventory(_item);
        // for nonstackable item or the slot is empty
        // Debug.Log("_item.Id" + _item.Id);
        if(!database.ItemObjects[_item.Id].stackable || slot == null){
            // Debug.Log(" CallSetEmptySlot(_item, _amount)");
            SetEmptySlot(_item, _amount);
            return true;
        }
        //stackable and slot not empty
        slot.AddAmount(_amount);
        return true;

    }
    public int EmptySlotCount{
        get{
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if(GetSlots[i].item.Id <= -1){
                    counter++;
                }
            }
            return counter;
        }
    }
    public InventorySlot FindItemOnInventory(Item _item){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if(GetSlots[i].item.Id == _item.Id){
                return GetSlots[i];

            }
            
        }
        return null;
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            //Update the first empty slot to the slot that contain item
            //if Container.Items[i] is empty
            if(GetSlots[i].item.Id <= -1){
                // Debug.Log("SetEmptySlot -> UpdateSlot");
                
                GetSlots[i].UpdateSlot( _item, _amount);
                return GetSlots[i];
            }
            
        }
        //set up functionality for full inventory
        return null;
    }

    public void SwapItems( InventorySlot _Slot1, InventorySlot _Slot2){
        // Debug.Log("BeginSwapItems : item1(BeingDraged): " + _Slot1.item.Name + "; item2(HoveringOver): " + _Slot2.item.Name);
        
        if(_Slot2.CanPlaceInSlot(_Slot1.ItemObject) && _Slot1.CanPlaceInSlot(_Slot2.ItemObject)){
            // Debug.Log("SwapItem!");
            InventorySlot temp = new InventorySlot( _Slot2.item, _Slot2.amount);
            _Slot2.UpdateSlot( _Slot1.item, _Slot1.amount);
            _Slot1.UpdateSlot(  temp.item,  temp.amount); //problem

        }

    }

    public void RemoveItem(Item _item){
        for (int i =0 ;  i< GetSlots.Length; i++){
            if(GetSlots[i].item == _item){
                GetSlots[i].UpdateSlot( null, 0);

            }
        }
    }



    [ContextMenu("Save")]
    public void Save(){
        //----------------Begin: Use Player Editable JsonUtility --------------------------
        // string saveData = JsonUtility.ToJson(this,true);
        // BinaryFormatter bf = new BinaryFormatter();
        // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        // bf.Serialize(file, saveData);
        // file.Close();
        //-------------------------------End------------------------------------------------

        //----------------Begin: Use Player NotEditable IFormatter --------------------------
        Debug.Log("Save!! Name: " + this);
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
        //------------------------------End--------------------------------------------------

    }



    [ContextMenu("Load")]
    public void Load(){

        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            //----------------Begin: Use Player Editable JsonUtility --------------------------
            // BinaryFormatter bf = new BinaryFormatter();
            // FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath) , FileMode.Open);
            // JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            // file.Close();
            //-------------------------------End------------------------------------------------

            //----------------Begin: Use Player NotEditable IFormatter --------------------------
            Debug.Log("Start Loading!! Name: " + this);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i=0; i < GetSlots.Length; i++){
                GetSlots[i].UpdateSlot( newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            stream.Close();
            Debug.Log("LoadSuccessful");
            //------------------------------End--------------------------------------------------
        }

    }



    [ContextMenu("Clear")]
    public void Clear(){
        Container.Clear();
    }



    public void SaveInitialData(){
        string initialDataPath = savePath + "InitialData";
    //----------------Begin: Use Player Editable JsonUtility --------------------------
    // string saveData = JsonUtility.ToJson(this,true);
    // BinaryFormatter bf = new BinaryFormatter();
    // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
    // bf.Serialize(file, saveData);
    // file.Close();
    //-------------------------------End------------------------------------------------

    //----------------Begin: Use Player NotEditable IFormatter --------------------------
    Debug.Log("SaveInitialData!! Name: " + this);
    IFormatter formatter = new BinaryFormatter();
    Stream stream = new FileStream(string.Concat(Application.persistentDataPath, initialDataPath), FileMode.Create, FileAccess.Write);
    formatter.Serialize(stream, Container);
    stream.Close();
    //------------------------------End--------------------------------------------------

    }


    public void LoadInitialData(){
        string initialDataPath = savePath + "InitialData";

    if(File.Exists(string.Concat(Application.persistentDataPath, initialDataPath))){
        //----------------Begin: Use Player Editable JsonUtility --------------------------
        // BinaryFormatter bf = new BinaryFormatter();
        // FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath) , FileMode.Open);
        // JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        // file.Close();
        //-------------------------------End------------------------------------------------

        //----------------Begin: Use Player NotEditable IFormatter --------------------------
        Debug.Log("Start Loading InitialData!! Name: " + this);
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, initialDataPath), FileMode.Open, FileAccess.Read);
        Inventory newContainer = (Inventory)formatter.Deserialize(stream);
        for (int i=0; i < GetSlots.Length; i++){
            // Debug.Log("i: " + i);
            // Debug.Log("GetSlots[i]" + GetSlots[i]);
            // Debug.Log("newContainer.Slots[i]" + newContainer.Slots[i]);
            // Debug.Log("newContainer.Slots[i].item" + newContainer.Slots[i].item);
            GetSlots[i].UpdateSlot( newContainer.Slots[i].item, newContainer.Slots[i].amount);
        }
        stream.Close();
        Debug.Log("LoadSuccessful");
        //------------------------------End--------------------------------------------------
    }

    }
    





}
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[100];
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
}



public delegate void SlotUpdated(InventorySlot _slot);



[System.Serializable]
public class InventorySlot{

    public float gunEulerAngleLeftUpperLimit;// Note:Left limit is positiove from 0 to 180
    public float gunEulerAngleLeftLowerLimit;// Note:Left limit is positiove from 0 to 180
    public float gunEulerAngleRightUpperLimit;//Note Right limit is negative from 0 to -180
    public float gunEulerAngleRightLowerLimit;//Note Right limit is negative from 0 to -180
    public ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay;
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdated;

    public Item item = new Item();
    public int amount;

    public ItemObject_SO ItemObject{    
        get{
            // Debug.Log("Call Slot.ItemObject ;get; item: " + item);
            // Debug.Log("Call Slot.ItemObject ;get; item.Id: " +item.Id);
            if(item.Id >= 0){//>=0 Changed: =0 should be a null 
                // Debug.Log("begin return");
                // Debug.Log("parent: " + parent);
                // Debug.Log("parent.inventory: " + parent.inventory);
                // Debug.Log("parent.inventory.database: " +parent.inventory.database);
                // Debug.Log("parent.inventory.database.GetItem[item.Id]" + parent.inventory.database.GetItem[item.Id]);//problem!!!!!!
                // Debug.Log("finish return");
                return parent.inventory.database.GetItem[item.Id];
            }
            // Debug.Log("return null");
            return null;
        }
    }
    public InventorySlot(){
        UpdateSlot( new Item(),0);

    }
    public InventorySlot( Item _item, int _amount){
        // Debug.Log("Constructor: new InventorySlot: _item: " +  _item.Name);
        // Debug.Log("InventorySlot => UpdateSlot");
        UpdateSlot( _item, _amount);

    }
    public void UpdateSlot( Item _item, int _amount){
        if (OnBeforeUpdated != null){
            // Debug.Log("UpdateSlot -> BeforeOnBeforeUpdate " + "item: " + _item.Name);
            OnBeforeUpdated.Invoke(this);
            // Debug.Log("UpdateSlot -> AfterOnBeforeUpdate " + "item: " + _item.Name);
        }
        // Debug.Log("OnUpdate _item:" + _item.Name + "; _amount: " + _amount + "; SlotDisplayed: " + this.slotDisplay);
        item = _item;
        amount = _amount;

        if (OnAfterUpdate != null){
            // Debug.Log("UpdateSlot -> BeforeOnAfterUpdate " + "item: " + _item.Name);
            OnAfterUpdate.Invoke(this);
            // Debug.Log("UpdateSlot -> AfterOnAfterUpdate " + "item: " + _item.Name);
        }
    }
    public void RemoveItem(){
        UpdateSlot( new Item(), 0);
    }
    public void AddAmount(int value){
        UpdateSlot( item, amount += value);
        
    }
    public  bool CanPlaceInSlot(ItemObject_SO _itemObject){
        // Debug.Log("Call CanPlaceInSlot; _itemObject:" +  _itemObject);
        // Debug.Log("Call CanPlaceInSlot; _itemObject.data.Name:" + _itemObject.data.Name);
        if(AllowedItems.Length <=0 || _itemObject == null || _itemObject.data.Id < 0 ){
            return true;
        }

        for (int i = 0; i < AllowedItems.Length; i++){

            if(_itemObject.type == AllowedItems[i]){
                // Debug.Log("return true");
                return true;
            }
        }
        // Debug.Log("return false");

        return false;

    }
}
