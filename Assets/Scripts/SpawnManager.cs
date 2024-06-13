using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;
    RoadSpawner Direction;
    
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
    }

    
    void Update()
    {
        
    }

        

    public void SpawnManagerTrigger(Vector3 position, Quaternion rotation)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Forward, position,rotation);
    }

    public void SpawnManagerTriggerRight(Vector3 position, Quaternion rotation)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Right, position, rotation);
    }


    public void SpawnManagerTriggerLeft(Vector3 position,Quaternion rotation)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Left, position,rotation);
    }
}
