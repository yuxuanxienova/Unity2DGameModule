using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>,ISaveable
{
    public GameObject gameRTSController;
    public GameObject mainCanvas;
    public GameObject equipmentCanvas;
    


    private void OnEnable()
    {
        EventHandler.AfterBagClosedEvent += OnAfterBagClosedEvent;
        EventHandler.AfterBagOpenEvent += OnAfterBagOpenEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterBagClosedEvent -= OnAfterBagClosedEvent;   
        EventHandler.AfterBagOpenEvent -= OnAfterBagOpenEvent;
    }

    private void OnAfterBagOpenEvent()
    {
        gameRTSController.SetActive(false);

    }

    private void OnAfterBagClosedEvent()
    {
        gameRTSController.SetActive(true);

    }




    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);

        //-------保存数据-------
        ISaveable saveable = this;
        saveable.SaveableRegister();
        //-----------------------
        
    }
    void Update()
    {

    }


    public void ActivateMainCanvas()
    {
        mainCanvas.SetActive(true);
    }

    public void DeactivateMainCanvas()
    {

        mainCanvas.SetActive(false);
    }

    public void ActivateGameRTSController()
    {
        gameRTSController.SetActive(true);

    }

    public void DeactivateGameRTSController()
    {
        gameRTSController.SetActive(false);
    }

    public void ActivateEquipmentCanvas()
    {
        equipmentCanvas.SetActive(true);

    }
    public void DeactivateEquipmentCanvas()
    {
        equipmentCanvas.SetActive(false);
    }
//---------------实现接口ISaveable--------------
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {

    }
//--------------------------------------------
}
