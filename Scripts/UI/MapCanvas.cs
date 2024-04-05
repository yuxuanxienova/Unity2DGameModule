using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCanvas : MonoBehaviour
{
    public GameObject[] indicators;




    private void Start()
    {

       

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {


        }


    }

    public void OnClickedH1()
    {
        
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name != "M1P2"){return;}
        TransitionManager.Instance.Transition(currentScene.name, "H1");
        DeactivateAllUnitIndicator();
        ActivateIndicatorWithParentName("H1");
    }
    public void OnClickedM1P2()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name != "H1" && currentScene.name != "M1P3"){return;}
        TransitionManager.Instance.Transition(currentScene.name, "M1P2");
        DeactivateAllUnitIndicator();
        ActivateIndicatorWithParentName("M1P2");
    }
    public void OnClickedM1P3()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name != "M1P2" && currentScene.name != "M1P4" && currentScene.name != "M1P5"){return;}
        TransitionManager.Instance.Transition(currentScene.name, "M1P3");
        DeactivateAllUnitIndicator();
        ActivateIndicatorWithParentName("M1P3");
    }
    public void OnClickedM1P4()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name != "M1P3" ){return;}
        TransitionManager.Instance.Transition(currentScene.name, "M1P4");
        DeactivateAllUnitIndicator();
        ActivateIndicatorWithParentName("M1P4");
    }
    public void OnClickedM1P5()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name != "M1P3" ){return;}
        TransitionManager.Instance.Transition(currentScene.name, "M1P5");
        DeactivateAllUnitIndicator();
        ActivateIndicatorWithParentName("M1P5");
    }

    private void DeactivateAllUnitIndicator()
    {       
        foreach(GameObject indicator in indicators)
        {
            indicator.SetActive(false);
        }
    }

    private void ActivateIndicatorWithParentName(string _name)
    {       
        foreach(GameObject indicator in indicators)
        {
            Debug.Log("indicator.transform.parent.gameObject.name" + indicator.transform.parent.gameObject.name);
            if(indicator.transform.parent.gameObject.name == _name)
            {
                indicator.SetActive(true);

            }
        }

    }



}
