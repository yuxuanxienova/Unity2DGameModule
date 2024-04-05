using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLoadingControl : MonoBehaviour
{
    public float XSpacing;
    public float YSpacing;
    public int NUM_COL;
    public GameObject pointIndicator;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;       
    }
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }
    private void OnAfterSceneLoadedEvent()
    {
        InstantiateObtainedUnitToScene();
        SetCameraToLoadPoint();
        EventHandler.CallAfterUnitsLoadedEvent(pointIndicator);

    }

    public void InstantiateObtainedUnitToScene()
    {
        Debug.Log("StartInstantiateObtainedUnitToScene()");
        for ( int i = 0 ; i < UnitManager.Instance.unitObtainedList.Count ; i++)
        {
            UnitName unitName_i = UnitManager.Instance.unitObtainedList[i];

            GameObject unitObj_i = Instantiate(UnitManager.Instance.unitData.GetUnitDetails(unitName_i).unitPrefab, pointIndicator.transform.position + GetOffset(i), Quaternion.identity);
            Debug.Log("Successfully Instantiate Unit: " + unitName_i);
        }

        EventHandler.CallAfterAllUnitInstantiatedEvent();

    }

    private void SetCameraToLoadPoint()
    {
        CameraManager.Instance.camera.transform.position = new Vector3(pointIndicator.transform.position.x , pointIndicator.transform.position.y , -10);
    }

    private Vector3 GetOffset(int index)
    {
        Debug.Log("xIndex(column): " + index % NUM_COL +  "; yIndex(row): " + index / NUM_COL);
       float xPos = XSpacing * (index % NUM_COL);
       float yPos = YSpacing * (index / NUM_COL);
       return new Vector3( xPos, yPos, 0);
    }
    


}
