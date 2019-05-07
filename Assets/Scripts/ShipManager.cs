using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private ShipAtmosphere shipAtmosphere;

    private int maxCrewSize = 0;
    private int numControlStations = 0;
    private int numShowers = 0;
    private int numToilets = 0;

    private List<ShipRoom> rooms;
    private Dictionary<KeyValuePair<int, int>, GameObject> shipGrid = new Dictionary<KeyValuePair<int, int>, GameObject>();

    void Start()
    {
        shipAtmosphere = GetComponent<ShipAtmosphere>();

        UpdateShip();
    }

    void Update()
    {
        
    }

    private void UpdateShip()
    {
        // update the ship whenever its layout changes
        maxCrewSize = 0;
        numControlStations = 0;
        numShowers = 0;
        numToilets = 0;
        foreach (Transform childTransform in transform)
        {
            GameObject child = childTransform.gameObject;

            if (child.CompareTag("Bed"))
            {
                ++maxCrewSize;
            } else if (child.CompareTag("ControlStation"))
            {
                ++numControlStations;
            } else if (child.CompareTag("Shower"))
            {
                ++numShowers;
            } else if (child.CompareTag("Toilet"))
            {
                ++numToilets;
            }
        }
    }

    /*
    private void UpdateShipGrid()
    {
        foreach (Transform childTransform in transform)
        {
            GameObject child = childTransform.gameObject;

            int posX = Mathf.RoundToInt(childTransform.position.x);
            int posY = Mathf.RoundToInt(childTransform.position.y);
            shipGrid.Add(new KeyValuePair<int, int>(posX, posY), child);
        }
    }
    */
}
