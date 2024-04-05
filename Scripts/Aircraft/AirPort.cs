using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AirPort : MonoBehaviour
{
    public GameObject aircraftPrefab;
    public GameObject airportIndicator;
    public GameObject takeoffIndicator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeployAircraft() 
    {
        // Get the direction from the aircraft to the airport indicator
        Vector3 direction = takeoffIndicator.transform.position - airportIndicator.transform.position;

        // Use LookRotation to create a rotation that aligns with the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Vector3 aircraftEulerAngles = new Vector3(0, 0, angle);
        // Instantiate the aircraft with the specified rotation
        GameObject aircraft = Instantiate(aircraftPrefab, airportIndicator.transform.position, Quaternion.Euler(aircraftEulerAngles));
        IMovePosition aircraftMovePosition = aircraft.GetComponent<IMovePosition>();
        Vector3 randomOffset = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        aircraftMovePosition.SetMovePosition(takeoffIndicator.transform.position + randomOffset);
    }
}
