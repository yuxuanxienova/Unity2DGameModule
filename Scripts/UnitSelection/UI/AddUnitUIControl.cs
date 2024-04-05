using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUnitUIControl : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.AddUnitSlotOnClickedEvent += OnAddUnitSlotOnClicked;
        

    }
    private void OnDisable()
    {
        EventHandler.AddUnitSlotOnClickedEvent -= OnAddUnitSlotOnClicked;
        

    }

    private void OnAddUnitSlotOnClicked(UnitName _unitName , bool _isSelected)
    {
        if(!_isSelected)
        {
            UnitManager.Instance.AddUnitToUnitObtainedList( _unitName);

        }
        if(_isSelected)
        {
            Debug.Log("UnitManager.Instance.RemoveUnitFromList(_unitName)");
            UnitManager.Instance.RemoveUnitFromUnitObtainedList(_unitName);
        }

    }


}
