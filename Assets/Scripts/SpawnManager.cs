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

        

    public void SpawnManagerTrigger(GameObject parent)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Forward, parent);
    }

    public void SpawnManagerTriggerRight(GameObject parent)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Right, parent);
    }


    public void SpawnManagerTriggerLeft(GameObject parent)
    {
        roadSpawner.SpawnRoad(RoadSpawner.Direction.Left, parent);
    }
}
