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
        Vector3 position = other.transform.position;
        Quaternion rotation = other.transform.localRotation;
        //debug.log("Enteredddddddddddd");

        GameObject otherGameObject = other.gameObject;
        Transform parentTransform = otherGameObject.transform.parent;
        GameObject parentObject = parentTransform.gameObject;
        //debug.log("nameeeeeeeee "+ parentObject.name);
        // Check for other tags if not an audio collider
        if (other.gameObject.tag == "DetectRight" && gameObject.tag == "Car")
        {
            //debug.log("righttt");
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
