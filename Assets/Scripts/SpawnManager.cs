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

        

    public void SpawnManagerTrigger(GameObject parent, Vector3 position, Quaternion rotation)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Forward,parent, position,rotation);
    }

    public void SpawnManagerTriggerRight(GameObject parent, Vector3 position, Quaternion rotation)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Right, parent, position, rotation);
    }


    public void SpawnManagerTriggerLeft(GameObject parent, Vector3 position,Quaternion rotation)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Left, parent, position,rotation);
    }
}
