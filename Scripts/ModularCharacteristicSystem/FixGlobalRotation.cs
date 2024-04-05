using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixGlobalRotation : MonoBehaviour
{


    private void LateUpdate()
    {
        // Set the global rotation of the child object to the initial rotation
        transform.rotation = Quaternion.identity;
    }
}
