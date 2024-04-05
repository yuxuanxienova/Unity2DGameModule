using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceTreeNode : MonoBehaviour
{
    //public Image nodeImage
    //public string nodeDescription
    public ScienceTreesCanvasUI scienceTreesCanvasUI;
    public ScienceTreeName treeName;
    public NodeName nodeName;
    public int requiredSciencePoint;
    // public bool canBeUnLocked;
    public bool isUnlocked;
    public bool isSelected;
    public GameObject selectedUI;
    public GameObject unlockedUI;
    public Image nodeImage;
    public List<ScienceTreeNode> childNodes;
    private string hexadecimalColor_Unlocked = "#FFFFFF";
    private string hexadecimalColor_Locked = "#000000";
    

    private void OnEnable()
    {
        EventHandler.BeforeResearchButtonClicked += OnBeforeResearchButtonClicked;
        EventHandler.AfterResearchButtonClicked += OnAfterResearchButtonClicked;

    }
    private void OnDisable()
    {
        EventHandler.BeforeResearchButtonClicked -= OnBeforeResearchButtonClicked;
        EventHandler.AfterResearchButtonClicked -= OnAfterResearchButtonClicked;

    }
    private void OnBeforeResearchButtonClicked()
    {
        //----------Error Handle-------------
        if(isUnlocked == true){return;}
        //-----------------------------------
        if(isSelected && CanBeUnlocked())
        {
            isUnlocked = true;
            unlockedUI.SetActive(isUnlocked);
        }

    }

    private void OnAfterResearchButtonClicked()
    {

        UpdateNodeUI();

    }

    public void NodeOnClicked()
    {
        DeselectAllOtherNodes();
        isSelected = !isSelected;
        selectedUI.SetActive(isSelected);
        scienceTreesCanvasUI.SetCurrentSelectedNode(this);
        UpdateNodeUI();
        scienceTreesCanvasUI.UpdateUI();
        

    }
    public void UpdateNodeUI()
    {
        //--------Error Handle-------
        if(selectedUI == null){Debug.Log("GameObject:" + this.gameObject + "noSelectionUI");return;} 
        if(unlockedUI == null){Debug.Log("GameObject:" + this.gameObject + "noUnlockedUI");return;}     
        //---------------------------
        selectedUI.SetActive(isSelected);
        unlockedUI.SetActive(isUnlocked);
        //-----------------Change Color-------------------
        if(CanBeUnlocked())
        {
            Color newColor;
            if (ColorUtility.TryParseHtmlString(hexadecimalColor_Unlocked, out newColor))
            {
                nodeImage.color = newColor; // Assign the new color to the Image component
            }
            else
            {
                Debug.LogError("Invalid hexadecimal color value!");
            }
            
        }
        else
        {
            Color newColor;
            if (ColorUtility.TryParseHtmlString(hexadecimalColor_Locked, out newColor))
            {
                nodeImage.color = newColor; // Assign the new color to the Image component
            }
            else
            {
                Debug.LogError("Invalid hexadecimal color value!");
            }

        }
        //-----------------------------------------------

    }
    private void DeselectAllOtherNodes()
    {
        foreach (var node in FindObjectsOfType<ScienceTreeNode>() )
        {
            //--------Error Handle-------
            if(node == null){continue;}
            //---------------------------
            if(node.nodeName == nodeName)
            {
                continue;
            }
            node.isSelected = false;
            node.UpdateNodeUI();
        }

    }
    private bool CanBeUnlocked()
    {
        //-------------Error Handle--------------
        if(childNodes == null){return true;}
        //---------------------------------------
        for( int i = 0; i < childNodes.Count; i++ )
        {
            //--------------Error Handle--------------
            if(childNodes[i] == null){continue;}
            //-------------------------------------
            if(childNodes[i].isUnlocked == false)
            {
                return false;
            }
        }
        return true;

    }



}




