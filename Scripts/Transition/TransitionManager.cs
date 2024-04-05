using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : Singleton<TransitionManager>,ISaveable
{
    // public string startScene;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;
    public string savedScene;

    

    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent( int obj )
    {


    }

    private void Start()
    {
        //-------保存数据-------
        ISaveable saveable = this;
        saveable.SaveableRegister();
        //-----------------------
    }
    public void Transition(string from, string to  , Action callback = null)
    {
        if (!isFade)
            StartCoroutine(TransitionToScecne(from, to , callback));

    }

    private IEnumerator TransitionToScecne( string from, string to , Action callback )
    {
        // ---------- existing code ----------
        yield return Fade(1);
        if(from != string.Empty)
        {
            
        EventHandler.CallBeforeSceneUnloadEvent();
        yield return SceneManager.UnloadSceneAsync(from);

        }

        yield return SceneManager.LoadSceneAsync(to , LoadSceneMode.Additive);

        //设置新场景为激活的场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();
        yield return Fade(0);

        // Execute the callback if provided
        //By passing a callback function to the Transition() method and executing it in the TransitionToScene() coroutine after the transition is finished, 
        //you can ensure that the () method is called at the appropriate time, after the transition is completed.
        callback?.Invoke();



    
    }

    //淡入淡出场景
    //“targetAlpha” 1 是黑， 0 是白
    private IEnumerator Fade (float targetAlpha)
    {

        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha , speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }
//---------------实现接口ISaveable--------------
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        // saveData.currentScene = SceneManager.GetActiveScene().name;
        saveData.currentScene = this.savedScene;
        return saveData;
        
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        // Transition("Menu", saveData.currentScene);    
        this.savedScene = saveData.currentScene;
    }
//--------------------------------------------------
}
