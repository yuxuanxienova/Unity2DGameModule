using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownControl : MonoBehaviour
{
    public float timeIntervalDecision = 10f;
    private float timeRemainingDecision;
    private Animator animator;

    public InventoryObject_SO equipmentInventory;
    private Dictionary<int , float> timeIntervalDicWithSlotIndex = new Dictionary<int , float>();
    private Dictionary<int , float> timeRemainingDicWithSlotIndex = new Dictionary<int , float>();
    public Dictionary<int , bool> flagFireDicWithSlotIndex = new Dictionary<int , bool>();

    private void OnEnable()
    {
        EventHandler.AfterBagClosedEvent += OnAfterBagClosedEvent;

    }
    private void OnDisable()
    {
        EventHandler.AfterBagClosedEvent -= OnAfterBagClosedEvent;

    }
    private void OnAfterBagClosedEvent()
    {
         InitializeWeaponCountDown();

    }


    // Start is called before the first frame update
    void Start()
    {
        //-----------------InitializeDecisionCountDown---------------------
        animator = GetComponent<Animator>();
        timeRemainingDecision = timeIntervalDecision;
        //-----------------------------------------------------------------

        //-----------------InitializeWeaponCountDown-------------------------
        InitializeWeaponCountDown();
        //-------------------------------------------------------------------
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDecisionCountDown();
        UpdateWeaponCountDown();
    }

    private void UpdateDecisionCountDown()
    {
        timeRemainingDecision -= Time.deltaTime;
        if(timeRemainingDecision <= 0)
        {
            timeRemainingDecision = timeIntervalDecision;
            animator.SetBool("FlagDecision", true);
        }
    }

    public void InitializeWeaponCountDown()
    {
        // Debug.Log("StartInitializeWeaponCountDown()");
        for (int i = 0; i < equipmentInventory.GetSlots.Length ; i++)
        {
           
            //-------------------------Handle Error---------------------------------------------------------
            // Debug.Log("equipmentInventory.GetSlots[i].item.coolDownTime" + equipmentInventory.GetSlots[i].item.coolDownTime);
            if(equipmentInventory.GetSlots[i].item == null || equipmentInventory.GetSlots[i].item.coolDownTime == null){continue;}
            if(equipmentInventory.GetSlots[i].item.coolDownTime == 0){continue;}
            if(timeIntervalDicWithSlotIndex.ContainsKey(i) != timeIntervalDicWithSlotIndex.ContainsKey(i))
            {
                Debug.Log("Error!!: CountDownControl=>InitializeWeaponCountDown() timeIntervalDicWithSlotIndex.ContainsKey(i) != timeIntervalDicWithSlotIndex.ContainsKey(i) ; i = " + i);
                continue;
            }
            if(timeIntervalDicWithSlotIndex.ContainsKey(i) != flagFireDicWithSlotIndex.ContainsKey(i))
            {
                Debug.Log("Error!!: CountDownControl=>InitializeWeaponCountDown() timeIntervalDicWithSlotIndex.ContainsKey(i) != flagFireDicWithSlotIndex.ContainsKey(i) ; i = " + i);
                continue;
            }
            //-----------------------------------------------------------------------------------------------------
            // Debug.Log("!timeIntervalDicWithSlotIndex.ContainsKey(i)" + !timeIntervalDicWithSlotIndex.ContainsKey(i)  );
            

            if(!timeIntervalDicWithSlotIndex.ContainsKey(i))
            {
                timeIntervalDicWithSlotIndex.Add(i, equipmentInventory.GetSlots[i].item.coolDownTime);
                timeRemainingDicWithSlotIndex.Add(i, timeIntervalDicWithSlotIndex[i]);
                flagFireDicWithSlotIndex.Add(i, false);
            }
            else
            {
                timeIntervalDicWithSlotIndex[i] = equipmentInventory.GetSlots[i].item.coolDownTime;
                timeRemainingDicWithSlotIndex[i] = timeIntervalDicWithSlotIndex[i];
                flagFireDicWithSlotIndex[i] = false;
            }

            //Debug.Log("Successfully Initialize CountDown of : " + equipmentInventory.GetSlots[i].item.Name + "; timeInterval: " + timeIntervalDicWithSlotIndex[i] + "; timeRemaining: " + timeRemainingDicWithSlotIndex[i]);
        }

    }



    private void UpdateWeaponCountDown()
    {
        
        for (int i = 0; i < equipmentInventory.GetSlots.Length ; i++)
        {
           
            //-------------------------Handle Error---------------------------------------------------------
            // Debug.Log("equipmentInventory.GetSlots[i].item.coolDownTime" + equipmentInventory.GetSlots[i].item.coolDownTime);
            if(equipmentInventory.GetSlots[i].item == null || equipmentInventory.GetSlots[i].item.coolDownTime == null){continue;}
            if(equipmentInventory.GetSlots[i].item.coolDownTime == 0){continue;}
            if(timeIntervalDicWithSlotIndex.ContainsKey(i) != timeIntervalDicWithSlotIndex.ContainsKey(i))
            {
                Debug.Log("Error!!: CountDownControl=>InitializeWeaponCountDown() timeIntervalDicWithSlotIndex.ContainsKey(i) != timeIntervalDicWithSlotIndex.ContainsKey(i) ; i = " + i);
                continue;
            }
            if(!timeIntervalDicWithSlotIndex.ContainsKey(i) || !timeRemainingDicWithSlotIndex.ContainsKey(i))
            {
                // Debug.Log("Error!!: Not Initialized; i= " + i);
                continue;
            }           
            //-----------------------------------------------------------------------------------------------------
            if(flagFireDicWithSlotIndex[i] == false)
            {
                //-------只有在flag为false时计算冷却， 为true但仍未开火时不进行计算-------------------------
                timeRemainingDicWithSlotIndex[i] -= Time.deltaTime;
            }
            
            if(timeRemainingDicWithSlotIndex[i] <= 0)
            {
                timeRemainingDicWithSlotIndex[i] = timeIntervalDicWithSlotIndex[i];
                flagFireDicWithSlotIndex[i] = true;
            }
            // Debug.Log("Successfully Update CountDown of : " + equipmentInventory.GetSlots[i].item.Name + "; timeInterval: " + timeIntervalDicWithSlotIndex[i] + "; timeRemaining: " + timeRemainingDicWithSlotIndex[i]);
        }

    }


}
