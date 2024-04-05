using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanModule : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        //--------------Error Handle-------------------
        if(this.gameObject.tag == "Enemy"){return;}//only works for player
        //-----------------------------------------------
        Debug.Log("OnCollisionEnter2D");
        if(other.gameObject.tag == "Dead")
        {
            Debug.Log("DeadObject!!!");

            UnitWithInventory unit  = other.gameObject.GetComponent<UnitWithInventory>();
            //-----------Error Handle-------------
            if(unit == null){Debug.Log("Error!! No Component UnitWithInventory"); return;}
            if(unit.unitName == UnitName.Default ){Debug.Log("Error! Invalid unit name Default"); return;}
            if(UnitManager.Instance.scannedUnitList.Contains(unit.unitName)){return;}
            //------------------------------------
            UnitManager.Instance.scannedUnitList.Add(unit.unitName);
            EventHandler.CallScanEvent(unit);
            //Add Science point depend on the ship



        }
        
    }


}
