using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Button_InPort : MonoBehaviour, IUIButton
{
    public GameObject PortUI;
    private InPortCanvasControl inPortConvasControl;
    private void Awake()
    {
        inPortConvasControl = PortUI.GetComponent<InPortCanvasControl>();

    }
    public void ButtonClicked()
    {
        inPortConvasControl.InPortButtonClicked();

    }


}
