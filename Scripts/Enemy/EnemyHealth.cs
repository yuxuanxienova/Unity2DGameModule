using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IUnitHealth
{
    public EnemyData_SO templateData;
    private EnemyData_SO enemyData_SO;

    public GameObject healthBar;
    private Transform barTransform;

    void Awake() 
    {
        enemyData_SO = Instantiate(templateData);
        barTransform = healthBar.transform.Find("HealthBar_Bar");
    }

    public void UpdateCurrentHealth(int addedValue)
    {
        int currentHealth = enemyData_SO.currentHealth;
        int maxHealth = enemyData_SO.maxHealth;
        Animator animator = GetComponent<Animator>();
        //-----------Error Handle---------------
        if (enemyData_SO == null) { Debug.Log("[ERROR] [EnemyHealth]enemyData_SO == null"); }
        //------------------------------------
        currentHealth += addedValue;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        //Update!
        enemyData_SO.currentHealth = currentHealth;
        UpdateHealthBar();
        //Update animator
        animator.SetBool("FlagDead", false);
        if (currentHealth == 0 )
        {
            //----------------------The Unit is Destroy----------------------
            EventHandler.CallBeforeUnitDestroyedEvent(this.gameObject);
            animator.SetTrigger("TriggerDestroy");
            //  HealthManager.Instance.SetFlagDead(unitName, true);
            //  animator.SetBool("FlagDead" , true);
            //---------------------------------------------------------------
        }

    }
    private void UpdateHealthBar()
    {
        int currentHealth = enemyData_SO.currentHealth;
        int maxHealth = enemyData_SO.maxHealth;
        //------------Error Handle----------------
        if (barTransform == null) { Debug.Log("Error!!! barTransform == null"); return; }
        //-------------------------------------
        float percentage = (float)currentHealth / (float)maxHealth;
        // Debug.Log("percentage" + percentage);
        barTransform.localScale = new Vector3(percentage, 1, 1);
    }

    public void UpdateMaxHealth()
    {
        
    }
}
