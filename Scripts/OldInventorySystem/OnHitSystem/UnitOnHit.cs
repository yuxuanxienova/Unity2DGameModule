using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOnHit : MonoBehaviour
{
    public InventoryObject_SO equipmentInventory;

    public GameObject HitIndicatorPrefab;
    private FireModule fireModule;
    private UnitWithInventory unitWithInventory;

    private GameObject unitEquipmentScreen;
    private UnitName unitName;
    private IUnitHealth unitHealth;
    // Start is called before the first frame update
    void Awake()
    {

        fireModule = GetComponent<FireModule>();

        unitWithInventory = GetComponent<UnitWithInventory>();

        unitHealth = GetComponent<IUnitHealth>();

        unitName = unitWithInventory.unitName;

        Transform parentTransform = UnitManager.Instance.equipmentScreensContent.transform;

        for ( int i = 0; i < parentTransform.childCount; i++ )
        {
            if(parentTransform.GetChild(i).gameObject.tag != "EquipmentScreen")
            {
                continue;
            }
            //We get a equipmentScreen
            StaticInterface staticInterface = parentTransform.GetChild(i).gameObject.GetComponent<StaticInterface>();
            if(staticInterface.unitName == unitName)
            {
                unitEquipmentScreen = parentTransform.GetChild(i).gameObject;
            }
        }

        // Debug.Log("unitEquipmentScreen: " + unitEquipmentScreen);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {

        var colliderProjectile = collider.GetComponent<Projectile>();

        if(colliderProjectile)
        {
            //--------------------Only ProjectileFrom Unit With Different Tag Will Cause Damage-------------------------
            if(colliderProjectile.parentTransform.gameObject.tag == this.tag)
            {
                // Debug.Log("SameTag!!!");
                return;
                
            }  



            //--------------------Display Hit Position on UI Screen-------------------------
            Vector3 relativeHitPosition = collider.transform.position - transform.position;
            Vector3 relativeHitPositionOnScreen = relativeHitPosition / fireModule.scaleFactor;
            // Debug.Log(" relativeHitPosition" + relativeHitPosition);
            // Debug.Log(" relativeHitPositionOnScreen" + relativeHitPositionOnScreen);
            var obj = Instantiate(HitIndicatorPrefab, new Vector3(0,0,0), Quaternion.identity);
            obj.transform.SetParent(unitEquipmentScreen.transform);
            obj.transform.localScale = new Vector3(1,1,1);
            obj.transform.localPosition = relativeHitPositionOnScreen;
            //-----------------------------------------------------------------------------

            //--------------Find the slot being hit and read the armor value-----------------

            //------1. Find the slot Being Hit--------------------
            int indexSlotOnHit = 0;//initialize
            float recordDisToSlot = 999999f;//initialize
            for (int i = 0; i < equipmentInventory.GetSlots.Length ; i++)
            {
                if(equipmentInventory.GetSlots[i].slotDisplay == null)
                {
                    continue;
                }
                Vector3 vecHitToSlot = equipmentInventory.GetSlots[i].slotDisplay.transform.localPosition - relativeHitPositionOnScreen;
                float distHitToSlot = vecHitToSlot.magnitude;
                if(recordDisToSlot > distHitToSlot)
                {
                    recordDisToSlot = distHitToSlot;
                    indexSlotOnHit = i;
                }
            }
            Debug.Log("indexSlotOnHit: " + indexSlotOnHit);

            //------2. Read the Armor Value and Calculate Damage----

            int kineticArmorValue = 0;//initialize
            int energyArmorValue  = 0;//initialize

            for (int i = 0; i < equipmentInventory.GetSlots[indexSlotOnHit].item.buffs.Length ; i++)
            {
                if(equipmentInventory.GetSlots[indexSlotOnHit].item.buffs[i].attribute == Attributes.KineticArmorValue)
                {
                    kineticArmorValue = equipmentInventory.GetSlots[indexSlotOnHit].item.buffs[i].value;
                }

                if(equipmentInventory.GetSlots[indexSlotOnHit].item.buffs[i].attribute == Attributes.EnergyArmorValue)
                {
                    energyArmorValue = equipmentInventory.GetSlots[indexSlotOnHit].item.buffs[i].value;
                }
            }
            int damage = SetToZeroIfNegative(colliderProjectile.kineticDamage - kineticArmorValue) + SetToZeroIfNegative(colliderProjectile.energyDamage - energyArmorValue);
            Debug.Log("Damage: " + damage);



            //----------------------------------------------------------------------------

            //UpdateHealth
            unitHealth.UpdateCurrentHealth( - damage  );

            
            
            

            

            Destroy(collider.gameObject);
        }
        
    }
    private int SetToZeroIfNegative(int input)
    {
        if (input < 0)
        {
            return 0;
        }
        
        return input;
    }
}
