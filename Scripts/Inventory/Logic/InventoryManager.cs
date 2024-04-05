using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject equipmentScreensContent;

    public GameObject inventoryScreen;

    public bool isInitialized;

    private void OnEnable()
    {
        EventHandler.SelectionMenuUnloadEvent += OnSelectionMenuUnloadEvent;
    }

    private void OnDisable()
    {
        EventHandler.SelectionMenuUnloadEvent -= OnSelectionMenuUnloadEvent;
    }

    private void OnSelectionMenuUnloadEvent()
    {
        if(!isInitialized)
        {
            InitializeAllInventory();
            isInitialized = true;
        }
        else
        {
            RelinkSlotDisplayOfEquipmentInventory();
        }
        LoadInitialDataOfAllInventory();

    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveInitialDataOfAllInventory();

        }
    }

    public void InitializeAllInventory()
    {
        //-------------1.Initialize all equipment screens-------------
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().InitializeInterface();
        }
        //------------------------------------------------------------

        //--------------2.Initialize  inventory-----------------------
        inventoryScreen.GetComponent<DynamicInterface>().InitializeInterface();
        //------------------------------------------------------------
    }

    public void SaveInitialDataOfAllInventory()
    {
        //-------------1.Save Initial Value of all equipment screens-------------
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().inventory.SaveInitialData();
        }
        //------------------------------------------------------------

        //--------------2.Save Initial Value of all  inventory-----------------------
        inventoryScreen.GetComponent<DynamicInterface>().inventory.SaveInitialData();
        //------------------------------------------------------------

    }

    public void LoadInitialDataOfAllInventory()
    {
        //-------------1.Load Initial Value of all equipment screens-------------
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().inventory.LoadInitialData();
        }
        //------------------------------------------------------------

        //--------------2.Load Initial Value of all  inventory-----------------------
        inventoryScreen.GetComponent<DynamicInterface>().inventory.LoadInitialData();
        //------------------------------------------------------------



    }

    public void SaveAllInventory()
    {
        //-------------1.Save Initial Value of all equipment screens-------------
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().inventory.Save();
        }
        //------------------------------------------------------------

        //--------------2.Save Initial Value of all  inventory-----------------------
        inventoryScreen.GetComponent<DynamicInterface>().inventory.Save();
        //------------------------------------------------------------

    }

    public void LoadAllInventory()
    {
        //-------------1.Load Initial Value of all equipment screens-------------
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().inventory.Load();
        }
        //------------------------------------------------------------

        //--------------2.Load Initial Value of all  inventory-----------------------
        inventoryScreen.GetComponent<DynamicInterface>().inventory.Load();
        //------------------------------------------------------------

    }

    public void UpdateAllInventory()
    {
        //-------------1.Update all equipment screens-------------
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            ExtensionMethods.UpdateSlotDisplay(child.gameObject.GetComponent<StaticInterface>().slotsOnInterface);
        }
        //------------------------------------------------------------

        //--------------2.Update all  inventory-----------------------
        ExtensionMethods.UpdateSlotDisplay(inventoryScreen.GetComponent<DynamicInterface>().slotsOnInterface);
        //------------------------------------------------------------

    }

    public void RelinkSlotDisplayOfEquipmentInventory()
    {
        //-------------1.Load Initial Value of all equipment screens-------------
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().RelinkSlotDisplay();
        }
        //------------------------------------------------------------

    }

    //----------------Test Methods---------------------
    public void LogAllSlotDisplayInEquipmentInventory()
    {
        int childCount = equipmentScreensContent.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Transform child = equipmentScreensContent.transform.GetChild(i);
            child.gameObject.GetComponent<StaticInterface>().LogSlotDisplay();
        }

    }

}
