using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FireModule : MonoBehaviour
{
    public float scaleFactor = 0.00187f;
    public InventoryObject_SO equipmentInventory;
    protected Transform m_transform;
    protected IFaceDirection iFaceDirection;
    private CountDownControl countDownControl;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
        iFaceDirection = GetComponent<IFaceDirection>();
        countDownControl = GetComponent<CountDownControl>();
            
    }

    // Update is called once per frame
    void Update()
    {
        // PlayerFireToMousePosition();
        
    }

    //Given the slot Index we find the projectile initial position
    Vector3 ProjectileInitPosition(int index )
    {

        return equipmentInventory.GetSlots[index].slotDisplay.transform.localPosition;

    }

    public void FireProjectile( Vector3 _targetPosition){
        //------------------ErrorHandle------------------------
        if(countDownControl == null){Debug.Log("Error!: CountDownControlNotFound obj:" + gameObject);return;}
        if(countDownControl.flagFireDicWithSlotIndex == null){return;}
        //------------------------------------------------------------


        for (int i = 0; i < equipmentInventory.GetSlots.Length ; i++)
        {  
            //------------------ErrorHandle------------------------
            
            if(!countDownControl.flagFireDicWithSlotIndex.ContainsKey(i)){continue;}
            //----------------------------------------------------        
            if(countDownControl.flagFireDicWithSlotIndex[i] == false){continue;}
            else
            {
                //Flag == true

                //1. 置位 flag fire
                countDownControl.flagFireDicWithSlotIndex[i] = false;
                //2. ------------执行以下开火代码----------------
            }

            float range_i =  equipmentInventory.GetSlots[i].item.range;
            float distToUnit = DistanceToUnit(_targetPosition,m_transform.position);
            float spreadingScale =distToUnit/range_i; 
            float spreading_i = equipmentInventory.GetSlots[i].item.spreading * spreadingScale;
            Vector3 randomOffset = new Vector3(Random.Range(-spreading_i, spreading_i) , Random.Range(-spreading_i, spreading_i) , 0);
            Vector3 randomOffset2 = new Vector3(Random.Range(-spreading_i, spreading_i) , Random.Range(-spreading_i, spreading_i) , 0);
            Vector3 randomOffset3 = new Vector3(Random.Range(-spreading_i, spreading_i) , Random.Range(-spreading_i, spreading_i) , 0);
            


            //-----------Gun1 : id = 1-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 1 && distToUnit <=  range_i ){ 
                FireGunWithSlotIndex( i, _targetPosition +  randomOffset) ;
                                                
            }
            //------------------------------------------


            //-----------Gun3 : id = 4-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 4 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ;  
                FireGunWithSlotIndex( i, _targetPosition + randomOffset2) ; 
                                                 
            }
            //------------------------------------------

            //----------- id = 21-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 21 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ;  
                                                 
            }
            //------------------------------------------
            
            //----------- id = 22-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 22 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ;  
                FireGunWithSlotIndex( i, _targetPosition + randomOffset2) ;
                                                 
            }
            //------------------------------------------

            //----------- id = 23-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 23 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ; 
                FireGunWithSlotIndex( i, _targetPosition + randomOffset2) ; 
                FireGunWithSlotIndex( i, _targetPosition + randomOffset3) ;
                                                 
            }
            //------------------------------------------

            //----------- id = 24-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 24 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ; 
                                                 
            }
            //------------------------------------------

            //----------- id = 25-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 25 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ; 
                FireGunWithSlotIndex( i, _targetPosition + randomOffset2) ; 
                                                 
            }
            //------------------------------------------

            //----------- id = 26-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 26 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ; 
                                                 
            }
            //------------------------------------------

            //----------- id = 27-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 27 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ; 
            }
            //------------------------------------------

            //----------- id = 28-----------------
            if( equipmentInventory.GetSlots[i].item.Id == 28 && distToUnit <=  range_i ){    
                FireGunWithSlotIndex( i, _targetPosition + randomOffset) ; 
            }
            //------------------------------------------
            
        }    


    }

    protected void FireGunWithSlotIndex( int i , Vector3 _targetPosition){
        Debug.Log("FireGunWithSlotIndex:" + i);
        // if(equipmentInventory.GetSlots[i].slotDisplay == null)
        // {
        //     return ;
        // }
        

        //angle of the ship facing 
        Quaternion rotation = Quaternion.Euler(iFaceDirection.GetEulerAngles());
        

        //gunPosition
        Vector3 gunPosition =  m_transform.position +  rotation * ProjectileInitPosition(i) * scaleFactor;

        //relative position
        Vector3 relativePos = _targetPosition - gunPosition ;

        //The angle the projectile should facing
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90f;
        Vector3 projectileEulerAngles = new Vector3 (0 , 0 ,angle);


        //calculate gun euler angle and only fire if the angle is within the limit
        float gunAngle = (projectileEulerAngles - m_transform.eulerAngles)[2];
        Vector3 gunEulerAngle = new Vector3 (0, 0, NormalizeAngle(gunAngle));
        float  leftUpperLimit =    equipmentInventory.GetSlots[i].gunEulerAngleLeftUpperLimit;
        float  leftLowerLimit =   equipmentInventory.GetSlots[i].gunEulerAngleLeftLowerLimit;
        float  rightUpperLimit =    equipmentInventory.GetSlots[i].gunEulerAngleRightUpperLimit;
        float  rightLowerLimit =   equipmentInventory.GetSlots[i].gunEulerAngleRightLowerLimit;



        GameObject projectile = ProjectileManager.Instance.GetProjectile[equipmentInventory.GetSlots[i].item.Id];

        if(( leftUpperLimit >= gunEulerAngle[2] && gunEulerAngle[2] >= leftLowerLimit) || ( rightUpperLimit >= gunEulerAngle[2] && gunEulerAngle[2] >= rightLowerLimit) )
        {


            GameObject projectileObj = Instantiate( projectile, gunPosition, Quaternion.Euler(projectileEulerAngles));
            Projectile objProjectileComponent = projectileObj.GetComponent<Projectile>();
            objProjectileComponent.SetParentTransform(transform);
            Debug.Log("SuccessfullyFireGunWithSlotIndex:" + i);

        
        }

    }



    Vector3 NormalizedEulerAngles(Transform transform)
    {
        Vector3 euler = transform.eulerAngles;
        euler.x = NormalizeAngle(euler.x);
        euler.y = NormalizeAngle(euler.y);
        euler.z = NormalizeAngle(euler.z);
        return euler;
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }
        else if (angle < -180f)
        {
            angle += 360f;
        }
        return angle;
    }
    private float DistanceToUnit( Vector3 _targetPos, Vector3 _myPos)
    {
        Vector3 relativePos = _targetPos - _myPos; 
        float targetDis = Mathf.Abs(relativePos.magnitude);

        return targetDis;
    }

    //-------------Methods Used For Test---------------------------

    private void PlayerFireToMousePosition()
    {
        if ( this.gameObject.tag == "Player" && Input.GetMouseButtonDown(0) )
        {
           FireProjectile(UtilsClass.GetMouseWorldPosition());
        }

    }


        
}




