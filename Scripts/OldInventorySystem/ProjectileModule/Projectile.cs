using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Data Member
    public float m_speed =20;
    public float m_liveTime =100;
    public int kineticDamage = 0;
    public int energyDamage = 0;
    protected Transform m_transform;
    
    public Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        Destroy(this.gameObject, m_liveTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.Translate(new Vector3(0, m_speed*Time.deltaTime,0));
        
    }

    public void SetParentTransform(Transform _transform)
    {
        parentTransform = _transform;
        // Debug.Log("SetParent parentTransformObj: " + parentTransform.gameObject);
    }


}
