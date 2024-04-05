using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainCanvas : MonoBehaviour
{
    public GameObject equipmentCanvas;
    public GameObject  scienceTreeCanvas;
    public Animator scienceTreeButtonAnimator;
    public UnitSelectionUI unitSelectionUI;
    public TextMeshProUGUI scanButtonTMPro;

    private void OnEnable()
    {
        EventHandler.ScanEvent += OnScanEvent;
        EventHandler.BeenDestroyedEvent += OnBeenDestroyedEvent;

    }
    private void OnDisable()
    {
        EventHandler.ScanEvent -= OnScanEvent;
        EventHandler.BeenDestroyedEvent -= OnBeenDestroyedEvent;

    }

    private void OnScanEvent(UnitWithInventory _unitBeenScanned)
    {
        UpdateScanButtonUI();



    }
    private void OnBeenDestroyedEvent(UnitWithInventory _unitBeenDestroyed)
    {
        if(_unitBeenDestroyed.gameObject.tag == "Player"){return;}
        scienceTreeButtonAnimator.SetTrigger("animate");

    }
    

    private void LateUpdate()
    {
        // UpdateScanButtonUI();
        
    }
    public void OpenScienceTree()
    {
        scienceTreeCanvas.SetActive(true);
        EventHandler.CallAfterScienceTreeOpened();

    }
    public void equipmentButtonOnClicked()
    {
        equipmentCanvas.SetActive(true);
        EventHandler.CallAfterBagOpenEvent();
        GameManager.Instance.DeactivateMainCanvas();
        unitSelectionUI.DeactivateAllEquipmentScreens();

    } 
    
    public void OnClickSearchButton()
    {
        equipmentCanvas.SetActive(true);
        unitSelectionUI.DeactivateAllEquipmentScreens();
        unitSelectionUI.UpdateScanUI();

    }

    private void UpdateScanButtonUI()
    {
        int scanUnitNum = UnitManager.Instance.scannedUnitList.Count;
        if(scanUnitNum == 0)
        {
            scanButtonTMPro.text ="";
        }
        else
        {
            scanButtonTMPro.text = scanUnitNum.ToString();
        }
    }


}
