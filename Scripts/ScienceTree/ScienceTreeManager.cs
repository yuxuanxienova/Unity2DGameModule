using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceTreeManager : Singleton<ScienceTreeManager>, ISaveable
{
    private Dictionary<NodeName , bool > nodeIsUnlockedDic = new Dictionary<NodeName, bool>();
    private Dictionary<ScienceTreeName , int > sciencePointInTrees = new Dictionary<ScienceTreeName , int>();
    public ScienceTreesCanvasUI scienceTreesCanvasUI;
    public GameObject TreesScreenContent;

    void Start()
    {
        //-------保存数据-------
        ISaveable saveable = this;
        saveable.SaveableRegister();
        //-----------------------
        
    }
    private void OnEnable()
    {

        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        EventHandler.AfterResearchButtonClicked += OnAfterResearchButtonClicked;
        EventHandler.AfterScienceTreeOpened += OnAfterScienceTreeOpened;
        EventHandler.BeenDestroyedEvent += OnBeenDestroyedEvent;
        

    }

    private void OnDisable()
    {

        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        EventHandler.AfterResearchButtonClicked -= OnAfterResearchButtonClicked;
        EventHandler.AfterScienceTreeOpened -= OnAfterScienceTreeOpened;
        EventHandler.BeenDestroyedEvent -= OnBeenDestroyedEvent;
        

    }
    private void OnBeenDestroyedEvent(UnitWithInventory _unitBeenDestroyed)
    {
        //------ErrorHandle------
         if(_unitBeenDestroyed.scienceTreeName == ScienceTreeName.Default){Debug.Log("Error! Can't Add Science Point!!!!");return;}
        if(_unitBeenDestroyed.scienceTreeName == null){Debug.Log("Error! Can't Add Science Point!!!!");return;}
        if(_unitBeenDestroyed.sciencePoints == null){Debug.Log("Error! Can't Add Science Point!!!!");return;}
        //----------------------
        
         AddPointsToTree(_unitBeenDestroyed.scienceTreeName,_unitBeenDestroyed.sciencePoints );
         Debug.Log("Successfully Add " + _unitBeenDestroyed.sciencePoints + " Points To " +  _unitBeenDestroyed.scienceTreeName);
         scienceTreesCanvasUI.UpdateUI();

    }

    private void OnAfterScienceTreeOpened()
    {
        LoadNodeIsUnlockedDic();

    }

    private void OnStartNewGameEvent(int  obj)
    {
        nodeIsUnlockedDic.Clear();
        sciencePointInTrees.Clear();

    }
    private void OnAfterResearchButtonClicked()
    {
        UpdateNodeIsUnlockedDic();
        
    }




    public void  UpdateNodeIsUnlockedDic()
    {
        foreach( var Node in FindObjectsOfType<ScienceTreeNode>())
        {
            //----------- Error Handle---------------------
            if (Node == null){continue;}
            if (Node.nodeName == NodeName.Default){Debug.Log("Invalid NodeName = Default; obj: " + Node.gameObject);continue;}
            //---------------------------------------------
            if (!nodeIsUnlockedDic.ContainsKey(Node.nodeName))
            {
                nodeIsUnlockedDic.Add(Node.nodeName, Node.isUnlocked);
            }
            else
            {
                nodeIsUnlockedDic[Node.nodeName] = Node.isUnlocked;
            }

        }

    }

    public void LoadNodeIsUnlockedDic()
    {
        Debug.Log("LoadNodeIsUnlockedDic()");
        foreach( var Node in FindObjectsOfType<ScienceTreeNode>())
        {
            //----------- Error Handle---------------------
            if (Node == null){Debug.Log("Error!! Node == null");continue;}
            if (Node.nodeName == NodeName.Default){Debug.Log("Invalid NodeName = Default; obj: " + Node.gameObject);continue;}
            //---------------------------------------------
            if (!nodeIsUnlockedDic.ContainsKey(Node.nodeName))
            {
                // Debug.Log("Fail to load node: " + Node.nodeName);
                //-------------initialize the node----------
                Node.isUnlocked = false;
                nodeIsUnlockedDic.Add(Node.nodeName, Node.isUnlocked);
                Node.UpdateNodeUI();
                //-------------------------------------------
            }
            else
            {
                Node.isUnlocked = nodeIsUnlockedDic[Node.nodeName];
                Debug.Log("Successfully Load Node: " + Node.nodeName + "; IsUnlocked: " + Node.isUnlocked);
            }
        }

    }

    public  void AddPointsToTree(ScienceTreeName _treeName, int _points)
    { 
        //------------Error Handle--------------
        if(_treeName == null){return;}
        //--------------------------------------
        if(!sciencePointInTrees.ContainsKey(_treeName))
        {
            sciencePointInTrees.Add(_treeName, _points);
        }
        else
        {
            sciencePointInTrees[_treeName] += _points;
        }
    }

    public int GetSciencePointWithName( ScienceTreeName _treeName)
    {
        //--------Error Handle--------------
        if(_treeName == null){return -1;}
        if(!sciencePointInTrees.ContainsKey(_treeName)){return -1;}
        //----------------------------------
        return sciencePointInTrees[_treeName];
    }
    //---------------实现接口ISaveable--------------
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.nodeIsUnlockedDic = this.nodeIsUnlockedDic;
        saveData.sciencePointInTrees = this.sciencePointInTrees;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.nodeIsUnlockedDic = saveData.nodeIsUnlockedDic;
        this.sciencePointInTrees = saveData.sciencePointInTrees;
   
    }

    //-------------------------------------------------
    

}
