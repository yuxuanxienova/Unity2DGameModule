using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "UnitDataList_SO" , menuName = "Unit/UnitDataList_SO")]
public class UnitDataList_SO : ScriptableObject
{
    public List<UnitDetails> UnitDetailsList;

    public UnitDetails GetUnitDetails(UnitName _unitName)
    {
        return UnitDetailsList.Find( i => i.unitName ==  _unitName);

    }
    

}

[System.Serializable]
public class UnitDetails
{
    public UnitName unitName;
    public Sprite  unitSprite;
    public GameObject unitPrefab;
    public InventoryObject_SO unitEquipmentInventory;

    public UnitDetails(){
        unitName = UnitName.Default;
        unitSprite = null;
        unitEquipmentInventory = null;
    }



}

public enum UnitName
{
    Default,
    ship1,
    ship2,
    ship3,
    Enemy,
    SKM1,
    SKM2,
    SKM3,
    ID2_1,
    ID2_2,
    ID2_3,
    ID2_4,
    ID2_5

}
