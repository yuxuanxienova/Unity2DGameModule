using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenuControl : MonoBehaviour
{
    public GameObject selectionMenuEquipmentScreensContent;
    // Start is called before the first frame update
    void Start()
    {
        InitializeSelectionMenuInventory();
        LoadInitialDataOfSelectionMenuInventory();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDisable()
    {
        Debug.Log("Selection Menu Unload");
        EventHandler.CallSelectionMenuUnloadEvent();
        
    }

    public void InitializeSelectionMenuInventory()
    {
        //-------------3.Initialize all selection menu equipment screens-------------
        int childCount2 =selectionMenuEquipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount2; i++)
        {
            Transform child = selectionMenuEquipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().InitializeInterface();
        }
        //------------------------------------------------------------
    }

    public void LoadInitialDataOfSelectionMenuInventory()
    {
        //-------------1.Load Initial Value of all selection menu equipment screens-------------
        int childCount2 = selectionMenuEquipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount2; i++)
        {
            Transform child = selectionMenuEquipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().inventory.LoadInitialData();
        }
        //------------------------------------------------------------
    }
}
