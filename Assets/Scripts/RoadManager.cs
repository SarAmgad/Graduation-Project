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
        Vector3 position = other.transform.localPosition;
        Quaternion rotation = other.transform.localRotation;

        Debug.Log("Enteredddddddddddd");

        // Check for other tags if not an audio collider
        if (other.gameObject.tag == "DetectRight" && gameObject.tag == "Car")
        {
            spawnManager.SpawnManagerTriggerRight(position, rotation);
            // Debug.Log("Triggered Entered Right" + );

        }
        else if (other.gameObject.tag == "DetectLeft" && gameObject.tag == "Car")
        {
            // Vector3 position = other.transform.localPosition;
            // Quaternion rotation = other.transform.localRotation;

            spawnManager.SpawnManagerTriggerLeft(position, rotation);
            Debug.Log("Triggered Entered 111 = " + gameObject.name );
        }
        else if (other.gameObject.tag == "Cube" && gameObject.tag == "Car")
        {
            // Vector3 position = other.transform.localPosition;
            // Quaternion rotation = other.transform.localRotation;

            spawnManager.SpawnManagerTrigger(position, rotation);
            Debug.Log("Triggered Entered 222 " + gameObject.name + "   " + other.gameObject.name);
        }
        // audioSource.volume = 0.6f;
        // isLeftAudio = false;
        // isRightAudio = false;

        // if (isRightDetected)
        // {
        //     Debug.Log("Total Score" + score);
        //     if (other.gameObject.tag == "DetectRight")
        //     {
        //         score++;
        //         isRightDetected = false;
        //         Debug.Log("Total Score" + score);
        //     }
        //     else if (other.gameObject.tag == "Forward")
        //     {
        //         if (score > 0)
        //             score--;
        //         isRightDetected = false;
        //     }
        // }
        // if (isLeftDetected)
        // {
        //     if (other.gameObject.CompareTag("DetectLeft"))
        //     {
        //         score++;
        //         isLeftDetected = false;
        //         Debug.Log("Total Score" + score);
        //     }
        //     else if (other.gameObject.CompareTag("ForwardLeft"))
        //     {
        //         if (score > 0)
        //             score--;
        //         isLeftDetected = false;
        //     }
        // }

    }


}
