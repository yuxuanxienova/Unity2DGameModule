using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScienceTreesCanvasUI : MonoBehaviour
{ 
    public TextMeshProUGUI requiredPointText;
    public List<TreeUI> treeList;
    private ScienceTreeNode currentSelectedNode;
    private void OnEnable()
    {
        UpdateUI();
        EventHandler.TreeSelectionButtonClickedEvent+= OnTreeSelectionButtonClickedEvent;

    }
    private void OnDisable()
    {
        EventHandler.TreeSelectionButtonClickedEvent-= OnTreeSelectionButtonClickedEvent;

    }
    private void OnTreeSelectionButtonClickedEvent(ScienceTreeName _treeName)
    {
        DeactivateAllTreeScreens();
        ActiveteTreeUIWithName(_treeName);
        UpdateUI();


    }
    public void DeactivateAllTreeScreens()
    {
        int childCount = ScienceTreeManager.Instance.TreesScreenContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = ScienceTreeManager.Instance.TreesScreenContent.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }
    public void ActiveteTreeUIWithName(ScienceTreeName _treeName)
    {
        int childCount = ScienceTreeManager.Instance.TreesScreenContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = ScienceTreeManager.Instance.TreesScreenContent.transform.GetChild(i);
            TreeUI TreeUI_i = child.gameObject.GetComponent<TreeUI>();
            if(TreeUI_i.treeName == _treeName)
            {
                child.gameObject.SetActive(true);
            }
        }

    }





    public void ResearchButtonOnClicked()
    {
        //------------------Error Handle-----------------
        if(currentSelectedNode == null){Debug.Log("NO SELECT"); return;}
        //-----------------------------------------------
        int point = ScienceTreeManager.Instance.GetSciencePointWithName(currentSelectedNode.treeName);
        if(point < currentSelectedNode.requiredSciencePoint)
        {
            Debug.Log("Insufficient Science Point!!");
            return;
        }
        ScienceTreeManager.Instance.AddPointsToTree(currentSelectedNode.treeName, - currentSelectedNode.requiredSciencePoint);
        EventHandler.CallBeforeResearchButtonClicked();
        EventHandler.CallAfterResearchButtonClicked();
        UpdateUI();
    }

    public void UpdateUI()
    {
         UpdateRequiredPoint();
         UpdateSciencePointUIInTreeList();

    }
    public void SetCurrentSelectedNode(ScienceTreeNode _node)
    {
        currentSelectedNode = _node;
    }




    private void UpdateRequiredPoint()
    {
        //------Error Handle------
        if(currentSelectedNode==null){return;}
        //------------------------
        requiredPointText.text = "RequiredPoint: " + currentSelectedNode.requiredSciencePoint;
    }

    private void UpdateSciencePointUIInTreeList()
    {
        for( int i = 0 ; i < treeList.Count ; i++)
        {
            treeList[i].UpdateSciencePoint();
        }
    }


    
}
