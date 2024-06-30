using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        Transform parentTransform = otherGameObject.transform.parent;
        GameObject parentObject = parentTransform.gameObject;
        if (other.gameObject.tag == "DetectRight" && gameObject.tag == "Car")
        {
            spawnManager.SpawnManagerTriggerRight(parentObject);
        }
        else if (other.gameObject.tag == "DetectLeft" && gameObject.tag == "Car")
        {
            //debug.log("lefttt");
            spawnManager.SpawnManagerTriggerLeft(parentObject);
        }
        else if (other.gameObject.tag == "Cube" && gameObject.tag == "Car")
        {
            spawnManager.SpawnManagerTrigger(parentObject);
            //debug.log("Triggered Entered 222 " + gameObject.name + "   " + other.gameObject.name);
        }

    }


}
