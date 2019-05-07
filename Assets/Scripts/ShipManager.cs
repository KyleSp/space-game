using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public float shipSpeed;
    public float shipRotateSpeed;

    public struct ShipStats
    {
        public int maxCrewSize;
        public int numEngines;
        public int numWeapons;
        public int numSensors;
        public int numWarpCores;
        public int numControlStations;
        public int numShowers;
        public int numToilets;
        public int numCrates;
        public int numAirlocks;

        public void ResetCounts()
        {
            maxCrewSize = 0;
            numEngines = 0;
            numWeapons = 0;
            numSensors = 0;
            numWarpCores = 0;
            numControlStations = 0;
            numShowers = 0;
            numToilets = 0;
            numCrates = 0;
            numAirlocks = 0;
        }

        public void AddToStats(GameObject obj)
        {
            if (obj.CompareTag("Bed"))
            {
                ++maxCrewSize;
            } else if (obj.CompareTag("Engine"))
            {
                ++numEngines;
            } else if (obj.CompareTag("Weapon"))
            {
                ++numWeapons;
            } else if (obj.CompareTag("Sensor"))
            {
                ++numSensors;
            } else if (obj.CompareTag("WarpCore"))
            {
                ++numWarpCores;
            } else if (obj.CompareTag("ControlStation"))
            {
                ++numControlStations;
            } else if (obj.CompareTag("Shower"))
            {
                ++numShowers;
            } else if (obj.CompareTag("Toilet"))
            {
                ++numToilets;
            } else if (obj.CompareTag("Crate"))
            {
                ++numCrates;
            } else if (obj.CompareTag("Airlock"))
            {
                ++numAirlocks;
            }
        }

        public void PrintStats()
        {
            Debug.Log("Max Crew Size: " + maxCrewSize);
            Debug.Log("Num Engines: " + numEngines);
            Debug.Log("Num Weapons: " + numWeapons);
            Debug.Log("Num Sensors: " + numSensors);
            Debug.Log("Num Warp Cores: " + numWarpCores);
            Debug.Log("Num Control Stations: " + numControlStations);
            Debug.Log("Num Showers: " + numShowers);
            Debug.Log("Num Toilets: " + numToilets);
            Debug.Log("Num Airlocks: " + numAirlocks);
        }
    }

    private ShipStats shipStats;
    private Rigidbody rb;
    private ShipAtmosphere shipAtmosphere;

    // private List<ShipRoom> rooms;
    // private Dictionary<KeyValuePair<int, int>, GameObject> shipGrid = new Dictionary<KeyValuePair<int, int>, GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shipAtmosphere = GetComponent<ShipAtmosphere>();

        UpdateShip();
    }

    void Update()
    {
        
    }

    private void UpdateShip()
    {
        // update the ship whenever its layout changes
        shipStats.ResetCounts();
        foreach (Transform childTransform in transform)
        {
            GameObject child = childTransform.gameObject;

            shipStats.AddToStats(child);
        }

        shipStats.PrintStats();
    }

    public void ControlShip(Vector2 input)
    {
        rb.angularVelocity = shipRotateSpeed * new Vector3(0, 0, -input.x);

        Vector3 angle = rb.rotation.eulerAngles;
        float x = input.y * Mathf.Cos(Mathf.Deg2Rad * angle.z);
        float y = input.y * Mathf.Sin(Mathf.Deg2Rad * angle.z);
        rb.velocity = shipSpeed * new Vector3(x, y, 0);
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
