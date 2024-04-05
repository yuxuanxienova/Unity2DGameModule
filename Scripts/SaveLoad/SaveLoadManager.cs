using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFolder;
    private List<ISaveable>saveableList = new List<ISaveable>();
    private Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();

    protected override void Awake()
    {
        base.Awake();
        //设定保存路径
        jsonFolder = Application.persistentDataPath + "/SAVE/";
        Debug.Log("SavePath: " + Application.persistentDataPath + "/SAVE/");
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
        // Debug.Log("OnStartNewGameEventSaveLoadManager");
        var resultPath = jsonFolder + "data.sav";
        //开始新游戏会覆盖以前的进度
        if (File.Exists(resultPath))
        {
            Debug.Log("NewGame!!Delete Saved Data");
            
            File.Delete(resultPath);
        }

    }


    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }

    public void Save()
    {
        //清空旧数据
        saveDataDict.Clear();

        //存入新数据
        foreach (var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name, saveable.GenerateSaveData());
        }

        //序列化和写入文件

        var resultPath = jsonFolder + "data.sav";

        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);
        // Debug.Log("Save in :" + resultPath);

        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);

        }

        File.WriteAllText(resultPath, jsonData);
        Debug.Log("SaveSuccessful");

    }

    public void Load()
    {
        var resultPath = jsonFolder + "data.sav";
        //读出数据并反序列化
        if(!File.Exists(resultPath)) return;

        var stringData = File.ReadAllText(resultPath);

        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);

        //恢复数据
        foreach (var saveable  in saveableList)
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);
        }


    }

}
