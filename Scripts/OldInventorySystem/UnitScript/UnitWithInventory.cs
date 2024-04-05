using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class UnitWithInventory : MonoBehaviour{
    public InventoryObject_SO inventory;
    public InventoryObject_SO equipment;
    public UnitName unitName;
    public ScienceTreeName scienceTreeName;
    public int sciencePoints;

    private FireModule fireModule;
    private IUnitHealth unitHealth;



    public Attribute[] attributes;

    private void Awake()
    {

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
            
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            //Will only trigger the OnBefore update and OnAfter update event in the equipment inventory 
            equipment.GetSlots[i].OnBeforeUpdated += OnBeforeSlotUpdated;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
            
        }

        // // get the fire module
        fireModule = GetComponent<FireModule>();
        unitHealth = GetComponent<UnitHealth>();


        
    }

    public void OnBeforeSlotUpdated( InventorySlot _slot){
        if (_slot.ItemObject == null){
            return ;
        }
        //The following code fired only when we unequip items

        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                //we only fired in the equipment interface
                // print(string.Concat("Removed" , _slot.ItemObject, "on" , _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                
                // Debug.Log("_slot.item.buffs.Length: " + _slot.item.buffs.Length);
                // Debug.Log("attributes.Length: " + attributes.Length);
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {                    
                    
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        
                        if (attributes[j].type == _slot.item.buffs[i].attribute){
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                        
                    }
                    
                }
                
                break;
            case InterfaceType.Chest:
                break;

            default:
                break;
        }
        

    }

    public void OnAfterSlotUpdate( InventorySlot _slot){
        //------------------ErrorHandle--------------------
        // Debug.Log("UnitWithInventory:OnAfterSlotUpdate");
        // Debug.Log("StartErrorHandle");
        // Debug.Log(" _slot: " + _slot );
        // Debug.Log("_slot.ItemObject:" + _slot.ItemObject);
        // Debug.Log("_slot.parent:" + _slot.parent);
        // Debug.Log("_slot.parent.inventory:" + _slot.parent.inventory);
        // Debug.Log("_slot.parent.inventory.type:" + _slot.parent.inventory.type);
        // Debug.Log("FinishErrorHandle");
        //----------------------------------------------

        if (_slot.ItemObject == null){
            return ;
        }
        //The following code fired only when we equipting items
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                //we only fired in the equipment interface
                // print(string.Concat("Placed" , _slot.ItemObject, "on" , _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
                //----------------------Updating the attributes--------------
                // Debug.Log("_slot.item.buffs.Length: " + _slot.item.buffs.Length);
                // Debug.Log("attributes.Length: " + attributes.Length);

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        
                        if (attributes[j].type == _slot.item.buffs[i].attribute){
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                            // Debug.Log("AddModifier: attributes[j].type:" + attributes[j].type);
                        }
                        
                    }
                    
                }
                //--------------------------------------------------------------

                //-----------------Updating the maxHealth---------------------
                
                if(unitHealth != null)
                {
                    unitHealth.UpdateMaxHealth();
                }
                
                
                //-----------------------------------------------------------


                break;
            case InterfaceType.Chest:
                break;

            default:
                break;
        }
        

    }





    // private void Update(){
    //     // if (Input.GetKeyDown(KeyCode.S)){
    //     //     inventory.Save();
    //     //     equipment.Save();
    //     // }
    //     // if(Input.GetKeyDown(KeyCode.L)){
    //     //     inventory.Load();
    //     //     equipment.Load();
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.A)){
    //     //     inventory.SaveInitialData();
    //     //     equipment.SaveInitialData();
    //     // }
    //     // if(Input.GetKeyDown(KeyCode.K)){
    //     //     inventory.LoadInitialData();
    //     //     equipment.LoadInitialData();
    //     // }

    // }
    
    //Call when the collider detect the collision
    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("Trigger!");
        //If the Player collide with the "Item"
        var item = collider.GetComponent<GroundItem>();
        if(item){
            Item _item = new Item(item.item);
            if( inventory.AddItem(_item,1)){
                Destroy(collider.gameObject);
            }
            
        }
        
    }

    // public void AttributeModified(Attribute attribute){
    //     Debug.Log(string.Concat(attribute.type, "was uodated! Value is now", attribute.value.ModifiedValue));
    // }




    //Clear the inventory once exit
    private void OnApplicationQuit(){
        inventory.Clear();
        equipment.Clear();
        
    }

    [System.Serializable]
    public class Attribute
    {
        [System.NonSerialized]
        private UnitWithInventory parent;
        public Attributes type;
        public ModifiableInt value = new ModifiableInt();//解决方法

        public void SetParent(UnitWithInventory _parent){
            parent = _parent;
            // value = new ModifiableInt(AttributeModified);//Problem!!! !!!!!!!!!!!!!!!!不删除baseValue永远是0但是删除会发生很多灵异事件//解决：上面加 = new ModifiableInt();

        }

        // public void AttributeModified(){   
        //     parent.AttributeModified(this);
        // }
    }

}
