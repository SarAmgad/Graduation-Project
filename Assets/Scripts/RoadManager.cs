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
        Debug.Log("Enteredddddddddddd");

        GameObject otherGameObject = other.gameObject;
        Transform parentTransform = otherGameObject.transform.parent;
        GameObject parentObject = parentTransform.gameObject;
        Debug.Log("nameeeeeeeee "+ parentObject.name);
        // Check for other tags if not an audio collider
        if (other.gameObject.tag == "DetectRight" && gameObject.tag == "Car")
        {
            Debug.Log("righttt");
            spawnManager.SpawnManagerTriggerRight(parentObject);

        }
        else if (other.gameObject.tag == "DetectLeft" && gameObject.tag == "Car")
        {
            Debug.Log("lefttt");
            spawnManager.SpawnManagerTriggerLeft(parentObject);
        }
        else if (other.gameObject.tag == "Cube" && gameObject.tag == "Car")
        {
            spawnManager.SpawnManagerTrigger(parentObject);
            Debug.Log("Triggered Entered 222 " + gameObject.name + "   " + other.gameObject.name);
        }

    }


}
