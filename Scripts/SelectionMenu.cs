using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenu : MonoBehaviour
{
    public void StartTheGame()
    {
        TransitionManager.Instance.Transition("SelectionMenu", "H1");
    }

}
