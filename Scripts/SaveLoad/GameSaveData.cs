using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData 
{
    //-------------GameManager-------
    public int gameWeek;
    //------------------------------

    //----------TransitionManager---------
    public string currentScene;
    //------------------------------------

    //-------------Object Manager-----------------
    public  Dictionary<ItemName, bool> itemAvailableDict ;
    public Dictionary<string, bool> interactiveStateDict ;
    //----------------------------------------

    //----------Inventory Manager-------------
    public List<ItemName> itemList;
    //-------------------------------------

    //----------Health Manager-------------
    public Dictionary<UnitName, int> unitCurrentHealthDictionary;
    public Dictionary<UnitName, int> unitMaxHealthDic;
    public Dictionary<UnitName, bool> unitFlagDeadDic;
    //-------------------------------------

    //----------Unit Manager-------------
    public List<UnitName> unitObtainedList;
    //-----------------------------------

    //----------Science Tree Manager----------
    public Dictionary<NodeName , bool > nodeIsUnlockedDic;
    public Dictionary<ScienceTreeName , int > sciencePointInTrees;
    //----------------------------------------

}
