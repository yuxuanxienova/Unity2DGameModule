using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingTheCamera : MonoBehaviour
{ 
    public float rate;
    private void Awake()
    {
    }

    private void LateUpdate()
    {
        // Update the background object's position to mimic the camera movement
        transform.position = new Vector3(CameraManager.Instance.camera.transform.position.x * rate, CameraManager.Instance.camera.transform.position.y * rate, transform.position.z);
    }
}
