using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //panSpeed variable will control the speed of the camera when it pans
    public float panSpeed = 20f;
    //panBorderThickness variable will determine the width of the border at the edge of the screen that will trigger the camera to move
    public float panBorderThickness = 10f;
    //panLimit variable will determine the maximum and minimum x and y coordinates the camera can move to.
    public Vector2 panLimit;
    // Start is called before the first frame update
    public Vector2 padding;
    void Start()
    {
        // Debug.Log("ScreenSize Height:"+ Screen.height + "; Width: " + Screen.width);
        Vector2 mapSize = GameObject.Find("Map").GetComponent<SpriteRenderer>().bounds.size;
        panLimit = new Vector2(mapSize.x / 2 - padding.x, mapSize.y / 2 - padding.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log("MousePositionUpdate X:"+Input.mousePosition.x + ";  Y:"+ Input.mousePosition.y);
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            // Debug.Log("Activete!");
            pos.y += panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        //The Mathf.Clamp() method is used to keep the camera within the panLimit range.

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
        
    }
}
