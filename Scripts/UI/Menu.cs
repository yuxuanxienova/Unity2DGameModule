using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        //加载游戏进度并跳转
        SaveLoadManager.Instance.Load();
        InventoryManager.Instance.LoadAllInventory();
        TransitionManager.Instance.Transition("Menu",TransitionManager.Instance.savedScene);
        GameManager.Instance.ActivateMainCanvas();
        GameManager.Instance.ActivateGameRTSController();


        

    }

    public void GoBackToMenu()
    {
        InventoryManager.Instance.SaveAllInventory();
        GameManager.Instance.DeactivateMainCanvas();
        GameManager.Instance.DeactivateGameRTSController();  
        var currentScene = SceneManager.GetActiveScene().name;

        //-------------跳转并保存改进版-------------------------------  
            TransitionManager.Instance.savedScene = SceneManager.GetActiveScene().name;
            TransitionManager.Instance.Transition(currentScene, "Menu", () =>
            {
                // This callback is executed after the transition is complete
                SaveLoadManager.Instance.Save();
            });
        //------------------------------------------------------------
        //-------------跳转并保存普通版-------------------------------
            // TransitionManager.Instance.Transition(currentScene, "Menu", null);
            // SaveLoadManager.Instance.Save();
        //-----------------------------------------------------

    }

    // public void StartNewGame()
    // {
    //     EventHandler.CallStartNewGameEvent(0);
    // }

    public void StartNewGame()
    {
        // InventoryManager.Instance.LoadInitialDataOfAllInventory();
        TransitionManager.Instance.Transition("Menu", "SelectionMenu");
        EventHandler.CallStartNewGameEvent(0);
        GameManager.Instance.ActivateMainCanvas();
        GameManager.Instance.ActivateGameRTSController();        
    }

}
