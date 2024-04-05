using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeUI : MonoBehaviour
{
    public ScienceTreeName treeName;
    public TextMeshProUGUI sciencePointText;




    public void UpdateSciencePoint()
    {
        //------------Error Handle-------------------
        if(ScienceTreeManager.Instance.GetSciencePointWithName(treeName) == -1)
        {
            ScienceTreeManager.Instance.AddPointsToTree(treeName , 0);

        }
        //-----------------------------------------------
        sciencePointText.text = "SciencePoint" + treeName +": " + ScienceTreeManager.Instance.GetSciencePointWithName(treeName);

    }


    //-------------------------------Test Function---------------
    public void TestAddingPoint()
    {
        ScienceTreeManager.Instance.AddPointsToTree(treeName,10);
    }



}

