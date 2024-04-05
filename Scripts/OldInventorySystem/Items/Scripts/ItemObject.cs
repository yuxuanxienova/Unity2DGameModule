using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    EnergyArmor,
    KineticArmor,
    Food,
    EnergyWeapon,
    KineticWeapon,
    Default
}

public enum Attributes{

    Agility,
    Intellect,
    Stamina,
    Strength,
    KineticArmorValue,
    EnergyArmorValue,
}
[CreateAssetMenu(fileName = "New Item" , menuName = "Inventory System/Items/item")]
public class ItemObject_SO : ScriptableObject
{


    public Sprite uiDisplay;
    public bool stackable;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public Item data = new Item();


    //Optional, some function that use to create the item
    public Item CreateItem(){
        Item newItem = new Item(this);
        return newItem; 
    }

}
//Add System.Serializable so it is displayed in the unity
[System.Serializable]
public class Item{
    public string Name;
    public int Id = -1;
    public float coolDownTime;
    public float range;
    public float spreading;
    public ItemBuff[] buffs;
    public Item(){
        Name = "";
        Id = -1;
    }
    public Item(ItemObject_SO item){
        Name = item.name;
        Id = item.data.Id;
        range = item.data.range;
        coolDownTime = item.data.coolDownTime;
        spreading = item.data.spreading;
        //---pass the buff on the itemObject to item calss---
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0;  i< buffs.Length; i++){
            
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max){
                attribute = item.data.buffs[i].attribute
            };
        }
        //---------------------------------------------------
    }
}
//Add System.Serializable so it is displayed in the unity
[System.Serializable]
public class  ItemBuff : IModifier{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    //The Constructor:
    public ItemBuff(int _min, int _max){
        min = _min;
        max = _max;
        GenerateValue();
    }
    //------------------

    public void AddValue(ref int baseValue)
    {
        baseValue  += value;
    }

    public void GenerateValue(){
        value =  UnityEngine.Random.Range(min,max);
    }
}