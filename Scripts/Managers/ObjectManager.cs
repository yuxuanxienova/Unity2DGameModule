using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour,ISaveable
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();

    private void Start()
    {
        //-------保存数据-------
        ISaveable saveable = this;
        saveable.SaveableRegister();
        //-----------------------
    }


    //每次激活这个物品时都会调用OnEnable
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }

    private void OnStartNewGameEvent(int  obj)
    {
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();

    }


    private void OnBeforeSceneUnloadEvent()
    {
        //Update ItemAvailable dict
        foreach (var item in FindObjectsOfType<Item_Remake>() )
        {           
            if (!itemAvailableDict.ContainsKey (item.itemName))
            {                
                itemAvailableDict.Add(item.itemName, true);
            }
        }

        //Update Interactive Dictionary
        foreach (var item in FindObjectsOfType<Interactive>() )
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                Debug.Log("update interactive state dic!");
                Debug.Log("Name: " + item.name + "State: " + item.isDone);
                interactiveStateDict[item.name] = item.isDone;
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }           

        }

    }

    private void OnAfterSceneLoadedEvent()
    {
        //Update ItemAvailable dict
        //如果在字典中则更新状态，不在则添加
        foreach (var item in FindObjectsOfType<Item_Remake>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
            }
            else
            {
                item.gameObject.SetActive( itemAvailableDict[item.itemName]);
            }
        }

        //Update Interactive Dictionary
        foreach (var item in FindObjectsOfType<Interactive>() )
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                item.isDone = interactiveStateDict[item.name] ;
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }           

        }

    }


    private void OnUpdateUIEvent(ItemDetails itemDetails, int arg2)
    {
        if (itemDetails !=  null)
        {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }
//---------------实现接口ISaveable--------------
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemAvailableDict = this.itemAvailableDict;
        saveData.interactiveStateDict = this.interactiveStateDict;

        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvailableDict = saveData.itemAvailableDict;
        this.interactiveStateDict = saveData.interactiveStateDict;
       
    }

//-------------------------------------------------
}
