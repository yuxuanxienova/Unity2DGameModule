using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public GameObject camera;
    public CameraController cameraController;





    private void OnEnable()
    {
        EventHandler.AfterBagOpenEvent += OnAfterBagOpenEvent;
        EventHandler.AfterBagClosedEvent += OnAfterBagClosedEvent;
        
    }

    private void OnDisable()
    {
        EventHandler.AfterBagOpenEvent -= OnAfterBagOpenEvent;
        EventHandler.AfterBagClosedEvent -= OnAfterBagClosedEvent;
        
    }

    private void OnAfterBagOpenEvent()
    {
        cameraController.enabled = false;

    }

    private void OnAfterBagClosedEvent()
    {
        cameraController.enabled = true;

    }
    

}
