using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour,IUnitHealth
{
    public GameObject healthBar;
    private Transform barTransform;

    private UnitWithInventory unitWithInventory;
    private UnitName  unitName;
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.AfterAllUnitInstantiatedEvent += OnAfterAllUnitInstantiatedEvent;

    }
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.AfterAllUnitInstantiatedEvent -= OnAfterAllUnitInstantiatedEvent;

    }
    private void OnAfterAllUnitInstantiatedEvent()
    {
        HealthManager.Instance.RegisterHealth( unitWithInventory);
        UpdateCurrentHealth(0);

    }

    private void OnAfterSceneLoadedEvent()
    {
        HealthManager.Instance.RegisterHealth( unitWithInventory);
        UpdateCurrentHealth(0);
    }
    // Start is called before the first frame update
    void Awake()
    {

        unitWithInventory = GetComponent<UnitWithInventory>();
        unitName = unitWithInventory.unitName;
        barTransform = healthBar.transform.Find("HealthBar_Bar"); 
    }



    public void UpdateCurrentHealth(int addedValue = 0)
    {
        Animator animator = GetComponent<Animator>();
        HealthManager.Instance.UpdateCurrentHealth(unitName , addedValue);
        int currentHealth = HealthManager.Instance.GetCurrentHealth(unitName);
        bool flagDead = HealthManager.Instance.GetFlagDead(unitName);
        //-----------------Error Handle------------------------
        if(currentHealth == -1){Debug.Log("FailToGetCurrentHealth UnitHealth; UnitName:" + unitName);return;}
        if(animator == null){Debug.Log("FailToGetAnimator UnitHealth; UnitName:" + unitName);return;}
        //-----------------------------------------------------
        animator.SetBool("FlagDead" , flagDead);
        if(currentHealth == 0 && flagDead == false)
        {
            //----------------------The Unit is Destroy----------------------
             EventHandler.CallBeforeUnitDestroyedEvent(this.gameObject);
             animator.SetTrigger("TriggerDestroy");
            //  HealthManager.Instance.SetFlagDead(unitName, true);
            //  animator.SetBool("FlagDead" , true);
            //---------------------------------------------------------------
        }
        Debug.Log("FinishUpdateCurrentHealth: " + currentHealth + "; Name: " + unitWithInventory.unitName);
        UpdateHealthBar();



    }

    public void UpdateMaxHealth()
    {
        HealthManager.Instance.UpdateMaxHealth(unitWithInventory);
    }

    private void UpdateHealthBar()
    {
        int currentHealth = HealthManager.Instance.GetCurrentHealth(unitName);
        int maxHealth = HealthManager.Instance.GetMaxHealth(unitName);
        //------------Error Handle----------------
        if(currentHealth == -1){Debug.Log("FailToGetCurrentHealth UnitHealth; UnitName:" + unitName);return;}
        if(maxHealth == -1){Debug.Log("FailToGetMaxHealth UnitHealth; UnitName:" + unitName);return;}
        if(barTransform == null){Debug.Log("Error!!! barTransform == null"); return;}
        //-------------------------------------
        float percentage = (float)currentHealth / (float)maxHealth;
        // Debug.Log("percentage" + percentage);
        barTransform.localScale = new Vector3( percentage , 1 , 1);
    }

}
