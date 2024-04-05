using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPortCanvasControl : MonoBehaviour
{
    public GameObject InPortCanvas;
    public GameObject Buttons;
    private bool isDisplayed;
    private void Awake()
    {
        isDisplayed = false;
        InPortCanvas.SetActive(isDisplayed);
    }
    public void InPortButtonClicked()
    {
        isDisplayed = !isDisplayed;
        InPortCanvas.SetActive(isDisplayed);
        Buttons.SetActive(!isDisplayed);
        GameManager.Instance.DeactivateMainCanvas();
        EventHandler.CallInPortCanvasOpenEvent();
    }

    public void ReturnButtonClicked() 
    {
        isDisplayed = !isDisplayed;
        InPortCanvas.SetActive(isDisplayed);
        Buttons.SetActive(!isDisplayed);
        GameManager.Instance.ActivateMainCanvas();
    }

}
