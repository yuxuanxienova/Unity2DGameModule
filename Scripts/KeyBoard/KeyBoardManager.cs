using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyBoardManager : MonoBehaviour
{
    public GameObject myBag;
    public GameObject mapCanvas;
    bool mapOpen;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
         OpenMyBag();
         OpenMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
        OpenMap();
        // testFunction();
        
    }
    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isOpen = ! isOpen;
            myBag.SetActive(isOpen);
            if(isOpen)
            {
                EventHandler.CallAfterBagOpenEvent();
            }
            if(!isOpen)
            {
                EventHandler.CallAfterBagClosedEvent();
            }

            

        }
    }

    void OpenMap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapOpen = !mapOpen;
            mapCanvas.SetActive(mapOpen);
            Debug.Log("mapOpen" + mapOpen);

        }
    }

    //-------------------Methods for Testing-----------------------
    void testFunction()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T!!");
            ScienceTreeManager.Instance.scienceTreesCanvasUI.DeactivateAllTreeScreens();

        }
    }
}
