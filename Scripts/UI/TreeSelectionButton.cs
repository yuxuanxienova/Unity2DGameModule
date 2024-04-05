using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSelectionButton : MonoBehaviour
{
    public ScienceTreeName treeName;
    public void TreeSelectionButtonClicked()
    {
        EventHandler.CallTreeSelectionButtonClickedEvent(treeName);
        

    }

}
