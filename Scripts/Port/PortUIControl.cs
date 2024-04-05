using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortUIControl : MonoBehaviour, IUnitUIControl
{
    public GameObject portUI;
    private bool isDisplayed;

    private void Awake()
    {
        isDisplayed = false;
        portUI.SetActive(isDisplayed);
    }

    public void UnitClicked()
    {
        //Debug.Log("port");
        isDisplayed = !isDisplayed;
        GameManager.Instance.ActivateMainCanvas();
        //portUI.GetComponent<InPortCanvasControl>().InPortCanvas.SetActive(false);
        portUI.SetActive(isDisplayed);
    }

    public void ActivateMainCanvas() 
    {
        GameManager.Instance.ActivateMainCanvas();
    }
}
