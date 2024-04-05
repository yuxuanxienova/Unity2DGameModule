using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Import external packages
using CodeMonkey.Utils;

public class GameRTSController : MonoBehaviour
{
    //Data member
    [SerializeField] private Transform selectionAreaTransform;
    private Vector3 startPosition;
    private List<UnitRTS> selectedUnitRTSList;
    private void Awake()
    {
        //Initialize selected unit RTS list 
        selectedUnitRTSList = new List<UnitRTS>();

        //Hide the selection area
        selectionAreaTransform.gameObject.SetActive(false);
       
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Left Mouse Button Pressed
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = UtilsClass.GetMouseWorldPosition();
        }

        if (Input.GetMouseButton(0)){
            //Left Mouse Button Held Down


            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
            );
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;


        }
            
        

        if(Input.GetMouseButtonUp(0))
        {
            //Left Mouse Button Released
            // Debug.Log(UtilsClass.GetMouseWorldPosition() + " " + startPosition );
            selectionAreaTransform.gameObject.SetActive(false);


            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());

            //Deselect all Units
            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                unitRTS.SetSelectedVisible(false);
            }

            //Initially list is empty
            selectedUnitRTSList.Clear();

            foreach (Collider2D collider2D in collider2DArray)
            {
                // Debug.Log(collider2D);

                //Add collided unit to the list
                UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();
                if (unitRTS != null)
                {
                    //Select Units within Selection Area
                    unitRTS.SetSelectedVisible(true);

                    //Add to the list
                    selectedUnitRTSList.Add(unitRTS);
                }
            }
            // Debug.Log(selectedUnitRTSList.Count);
        }



        //Right Mouse Button Down
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition();

            //Calculate the Centre of all selected unit
            Vector3 sumUnitsPosition = new Vector3(0,0,0);
            int count = 0;
            foreach (UnitRTS unitRTS in selectedUnitRTSList){
                sumUnitsPosition = sumUnitsPosition + unitRTS.GetPosition();
                count ++;
            }
            Vector3 centrePosition = sumUnitsPosition / count;
            



            //Move to the offested target position
            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                Vector3  offset = unitRTS.GetPosition() - centrePosition;

                if (unitRTS.gameObject.tag == "Player") 
                {
                    unitRTS.MoveTo(moveToPosition + offset);
                }



            }

        }
        
    }
}
