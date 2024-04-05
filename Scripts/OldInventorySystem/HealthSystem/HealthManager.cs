using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : Singleton<HealthManager>, ISaveable
{
    //contains unit name and current health
    private Dictionary<UnitName, int> unitCurrentHealthDictionary = new Dictionary<UnitName, int>();
    private Dictionary<UnitName, int> unitMaxHealthDic = new Dictionary<UnitName, int>();

    private Dictionary<UnitName, bool> unitFlagDeadDic = new Dictionary<UnitName, bool>();
  

    // Start is called before the first frame update
    void Start()
    {
        //-------保存数据-------
        ISaveable saveable = this;
        saveable.SaveableRegister();
        //-----------------------
        
    }
    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }
    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }
    private void OnStartNewGameEvent(int obj)
    {
        unitCurrentHealthDictionary.Clear();
        unitMaxHealthDic.Clear();
        unitFlagDeadDic.Clear();
    }
    public bool GetFlagDead(UnitName _unitName)
    {
        return unitFlagDeadDic[_unitName];

    }
    public void SetFlagDead(UnitName _unitName , bool flag)
    {
        //---------------Error Handle--------------------
        if(_unitName == UnitName.Default){Debug.Log("InvalidUnitName!!  SetFlagDead _unitName == _unitName.Default"); return ;}
        if(!unitCurrentHealthDictionary.ContainsKey(_unitName)){Debug.Log("Fail To SetFlagDead!! HealthManager-> SetFlagDead(); UnitName: " + _unitName);return ;}
        //----------------------------------------------

        unitFlagDeadDic[_unitName] = flag;

    }
    public int GetCurrentHealth(UnitName _unitName)
    {
        //---------------Error Handle--------------------
        if(!unitCurrentHealthDictionary.ContainsKey(_unitName)){Debug.Log("FailToGetCurrentHealth!! HealthManager->GetCurrentHealth(); UnitName: " + _unitName);return -1 ;}
        //----------------------------------------------

        return unitCurrentHealthDictionary[_unitName];

    }

    public void UpdateCurrentHealth(UnitName _unitName , int addedValue)
    {
        //---------------Error Handle--------------------
        if(_unitName == UnitName.Default){Debug.Log("InvalidUnitName!!  UpdateCurrentHealth _unitName == _unitName.Default"); return ;}
        if(!unitCurrentHealthDictionary.ContainsKey(_unitName)){Debug.Log("Fail To UpdateCurrentHealth!! HealthManager-> UpdateCurrentHealth(); UnitName: " + _unitName);return ;}
        //----------------------------------------------
        
        
        unitCurrentHealthDictionary[_unitName] += addedValue;
        if (unitCurrentHealthDictionary[_unitName] >= unitMaxHealthDic[_unitName])
        {
            unitCurrentHealthDictionary[_unitName] = unitMaxHealthDic[_unitName];
        }
        if(unitCurrentHealthDictionary[_unitName] <= 0)
        {
            unitCurrentHealthDictionary[_unitName] = 0;
        }
        // Debug.Log("Successfully UpdateCurrentHealthOnManager: " + unitCurrentHealthDictionary[_unitName] + "; Name: " + _unitName);


    }

    public int GetMaxHealth(UnitName _unitName)
    {
        //---------------Error Handle--------------------
        if(_unitName ==  UnitName.Default){Debug.Log("InvalidUnitName!! GetMaxHealth _unitName == _unitName.Default"); return -1;}
        if(!unitMaxHealthDic.ContainsKey(_unitName)){Debug.Log("FailToGetMaxHealth!! HealthManager->GetMaxHealth; UnitName: " + _unitName);return -1 ;}
        //----------------------------------------------

        return unitMaxHealthDic[_unitName];

    }

    public void UpdateMaxHealth(UnitWithInventory _unitWithInventory)
    {
        //----------Error Handle-------------
        if(_unitWithInventory ==  null){Debug.Log("Error!!!  HealthManager->UpdateMaxHealth: _unitWithInventory ==  null");return ;}
        // if(_unitWithInventory.unitName == UnitName.Default){"Error!!  HealthManager->UpdateMaxHealth: _unitWithInventory.unitName == UnitName.Default"; return;}
        if(!unitMaxHealthDic.ContainsKey(_unitWithInventory.unitName)){Debug.Log("FailToUpdateMaxHealth!! HealthManager-> UpdateMaxHealth; UnitName: " + _unitWithInventory.unitName);return ;}
        //--------------------------------------
        //Update
        int newMaxHealth = 0;
        var attributes = _unitWithInventory.attributes;


        for (int i = 0; i < attributes.Length; i++)
        {   
            if (attributes[i].type == Attributes.Strength)
            {
                newMaxHealth = attributes[i].value.ModifiedValue * 10;
                unitMaxHealthDic[_unitWithInventory.unitName] = newMaxHealth;
            }
            
        }
        // Debug.Log("Successfully UpdateMaxHealthOnManager: " + newMaxHealth + "; Name: " + _unitWithInventory.unitName);

    }
    public void RegisterHealth( UnitWithInventory _unitWithInventory)
    {
        //----------Error Handle-------------
        if(_unitWithInventory ==  null){Debug.Log("Error!!!  HealthManager->RegisterHealth: _unitWithInventory ==  null");return ;}
        // if(_unitWithInventory.unitName == UnitName.Default){"Error!!  HealthManager->RegisterHealth: _unitWithInventory.unitName == UnitName.Default"; return;}
        if(unitMaxHealthDic.ContainsKey(_unitWithInventory.unitName) != unitCurrentHealthDictionary.ContainsKey(_unitWithInventory.unitName)){Debug.Log("Error!! HealthManager->unitMaxHealthDic.ContainsKey(_unitWithInventory.unitName) != unitCurrentHealthDictionary.ContainsKey(_unitWithInventory.unitName)"); return;}
        //-----------------------------------


        //---------1.Register MaxHealth------------------------
        if(!unitMaxHealthDic.ContainsKey(_unitWithInventory.unitName))
        {
            //Register
            int maxHealth = 0;
            var attributes = _unitWithInventory.attributes;


            for (int i = 0; i < attributes.Length; i++)
            {   
                if (attributes[i].type == Attributes.Strength)
                {
                    maxHealth = attributes[i].value.ModifiedValue * 10;
                    unitMaxHealthDic.Add(_unitWithInventory.unitName , maxHealth);
                }
                
            }
            // Debug.Log("Successfully RegisterMaxHealth: " + maxHealth + "; Name: " + _unitWithInventory.unitName);
        }
        else
        {
            // Debug.Log("AlreadyRegistered MaxHealth!!" + "; MaxHealth = " + unitMaxHealthDic[_unitWithInventory.unitName] +    "; Name: " + _unitWithInventory.unitName);
        }


        //------------2.RegisterCurrentHeal And Initialized------------
        if(!unitCurrentHealthDictionary.ContainsKey(_unitWithInventory.unitName))
        {
            unitCurrentHealthDictionary.Add(_unitWithInventory.unitName , unitMaxHealthDic[_unitWithInventory.unitName]);
            // Debug.Log("Successfully Register Current Health; Current Health:" + unitCurrentHealthDictionary[_unitWithInventory.unitName] +  " ; Name: " + _unitWithInventory.unitName);

        }
        else
        {
            // Debug.Log("AlreadyRegistered CurrentHealth!!" + "; CurrentHealth = " + unitCurrentHealthDictionary[_unitWithInventory.unitName] +    "; Name: " + _unitWithInventory.unitName);

        }

        //-----------3.Register Flag Dead And Initialization -------------
        if(!unitFlagDeadDic.ContainsKey(_unitWithInventory.unitName))
        {
            unitFlagDeadDic.Add(_unitWithInventory.unitName, false);
            // Debug.Log("Successfully Register FlagDead ;FlagDead:" + unitFlagDeadDic[_unitWithInventory.unitName] +  " ; Name: " + _unitWithInventory.unitName);
        }
        else
        {
            // Debug.Log("AlreadyRegistered FlagDead!!" + ";FlagDead: " +unitFlagDeadDic[_unitWithInventory.unitName] +    "; Name: " + _unitWithInventory.unitName);
        }

    }




    //---------------实现接口ISaveable--------------
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.unitCurrentHealthDictionary = this.unitCurrentHealthDictionary;
        saveData.unitMaxHealthDic = this.unitMaxHealthDic;
        saveData.unitFlagDeadDic = this.unitFlagDeadDic;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.unitMaxHealthDic = saveData.unitMaxHealthDic;
        this.unitCurrentHealthDictionary = saveData.unitCurrentHealthDictionary;
        this.unitFlagDeadDic = saveData.unitFlagDeadDic;
    }

    //-------------------------------------------------
}
