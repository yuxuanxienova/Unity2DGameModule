using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitManager : Singleton<UnitManager>, ISaveable
{
    public GameObject equipmentScreensContent;
    public UnitDataList_SO unitData;

    public List<UnitName> unitObtainedList = new List<UnitName>();
    public List<UnitName> GM_AllUnitList = new List<UnitName>();
    public List<UnitName> scannedUnitList = new List<UnitName>();
    private Dictionary<PortSiteName,UnitName> PortSiteUnitDict = new Dictionary<PortSiteName, UnitName>();

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
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;

    }
    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;

    }
    private void OnStartNewGameEvent(int obj)
    {
        unitObtainedList.Clear();

    }
    private void  OnBeforeSceneUnloadEvent()
    {
         scannedUnitList.Clear();

    }
    



    //-----------Operate Unit Obtained List-----------------------------

    public void AddUnitToUnitObtainedList( UnitName _unitName)
    {
        if(!unitObtainedList.Contains(_unitName))
        {
            unitObtainedList.Add(_unitName);
        }
    }

    public void RemoveUnitFromUnitObtainedList( UnitName _unitName)
    {
        if(unitObtainedList.Contains(_unitName))
        {
            Debug.Log("unitObtainedList.Remove(_unitName)");
            unitObtainedList.Remove(_unitName);
        }

    }

    public void SortUnitObtainedList()
    {

    }

    //---------Operate Port Site Unit Dict------------------------

    public void AddUnitToPortSiteDict(UnitName _unitName, PortSiteName _portSiteName) 
    {
        if (PortSiteUnitDict.ContainsKey(_portSiteName))
        {
            //This port site is occupied

        }
        else 
        {
            //This port site is available
            PortSiteUnitDict.Add(_portSiteName, _unitName);
        }

    }

    public void RemoveUnitFromPortSiteDict(UnitName _unitName, PortSiteName _portSiteName) 
    {
        if (PortSiteUnitDict.ContainsKey(_portSiteName)) 
        {
            //There is a unit on this site
            PortSiteUnitDict.Remove(_portSiteName);
        }
    }




    //---------------实现接口ISaveable--------------
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.unitObtainedList = this.unitObtainedList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.unitObtainedList = saveData.unitObtainedList; 
   
    }

    //-------------------------------------------------



}
