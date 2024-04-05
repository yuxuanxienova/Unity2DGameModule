using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;

public static class EventHandler
{
    public static event Action<ItemDetails, int > UpdateUIEvent;

    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }

    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        Debug.Log("CallBeforeSceneUnloadEvent");
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectedEvent;

    public static void CallItemSelectedEvent(ItemDetails _itemDetails, bool _isSelected)
    {
        ItemSelectedEvent?.Invoke(_itemDetails , _isSelected);
    }

    public static event Action<ItemName> ItemUsedEvent;
    public static void CallItemUsedEvent(ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }



    public static event Action<int> StartNewGameEvent;
    public static void CallStartNewGameEvent( int gameWeek)
    {
        StartNewGameEvent?.Invoke(gameWeek);
    }

    public static event Action AfterBagOpenEvent;
    public static void CallAfterBagOpenEvent()
    {
        AfterBagOpenEvent?.Invoke();
    }

    public static event Action AfterBagClosedEvent;
    public static void CallAfterBagClosedEvent()
    {
        AfterBagClosedEvent?.Invoke();
    }

    public static event Action<UnitName> UnitSlotOnClickedEvent;
    public static void CallUnitSlotOnClickedEvent(UnitName _unitName)
    {
        UnitSlotOnClickedEvent?.Invoke(_unitName);
    }

    public static event Action<UnitName, bool> AddUnitSlotOnClickedEvent;
    public static void CallAddUnitSlotOnClickedEvent(UnitName _unitName, bool _isSelected)
    {
        AddUnitSlotOnClickedEvent?.Invoke(_unitName, _isSelected);
    }

    public static event Action AfterAddUnitSlotClickedEvent;
    public static void CallAfterAddUnitSlotClickedEvent()
    {
        AfterAddUnitSlotClickedEvent?.Invoke();
    }

    public static event Action BeforeMapOpenEvent;
    public static void CallBeforeMapOpenEvent()
    {
        BeforeMapOpenEvent?.Invoke();
    }
    public static event Action BeforeResearchButtonClicked;
    public static void CallBeforeResearchButtonClicked()
    {
        BeforeResearchButtonClicked?.Invoke();
    }

    public static event Action AfterResearchButtonClicked;
    public static void CallAfterResearchButtonClicked()
    {
        AfterResearchButtonClicked?.Invoke();
    }

    public static event Action  AfterScienceTreeOpened;
    public static void CallAfterScienceTreeOpened()
    {
        AfterScienceTreeOpened?.Invoke();
    }

    public static event Action<GameObject> AfterUnitsLoadedEvent;

    public static void CallAfterUnitsLoadedEvent( GameObject _loadPointIndicator)
    {
        AfterUnitsLoadedEvent?.Invoke(_loadPointIndicator);

    }

    public static Action<GameObject> BeforeUnitDestroyedEvent;
    public static void CallBeforeUnitDestroyedEvent(GameObject _unitObj)
    {
        BeforeUnitDestroyedEvent?.Invoke(_unitObj);
    }

    public static Action<UnitWithInventory> ScanEvent;
    public static void CallScanEvent(UnitWithInventory _unitBeenScanned)
    {
        ScanEvent?.Invoke(_unitBeenScanned);

    }

    public static Action<UnitWithInventory> BeenDestroyedEvent;
    public static void CallBeenDestroyedEvent(UnitWithInventory _unitBeenDestroyed)
    {
        BeenDestroyedEvent?.Invoke(_unitBeenDestroyed);

    }

    public static Action AfterAllUnitInstantiatedEvent;
    public static void CallAfterAllUnitInstantiatedEvent()
    {
        AfterAllUnitInstantiatedEvent?.Invoke();
    }

    public static Action SelectionMenuUnloadEvent;
    public static void CallSelectionMenuUnloadEvent()
    {
        SelectionMenuUnloadEvent?.Invoke();
    }

    public static Action<ScienceTreeName> TreeSelectionButtonClickedEvent;
    public static void CallTreeSelectionButtonClickedEvent(ScienceTreeName _treeName)
    {
        TreeSelectionButtonClickedEvent?.Invoke(_treeName);
    }

    public static Action InPortCanvasOpenEvent;
    public static void CallInPortCanvasOpenEvent() 
    {
        InPortCanvasOpenEvent?.Invoke();
    }

    public static event Action<GameObject> PortSiteSlotOnClickedEvent;
    public static void CallPortSiteSlotOnClickedEvent(GameObject _portSiteSlot)
    {
        PortSiteSlotOnClickedEvent?.Invoke(_portSiteSlot);
    }














}
