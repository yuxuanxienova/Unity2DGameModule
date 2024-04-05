using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneToGo;

    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneToGo);
    }

}
