using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour

{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;


    // Update is called once per frame
    void Update()
    {
        // float hmovement = Input.GetAxis("Horizontal")* movementSpeed;
        // float vmovement = Input.GetAxis("Vertical")* movementSpeed;
        // transform.Translate(new Vector3(hmovement,0,vmovement)*Time.deltaTime);


    }



    /*private void OnTriggerEnter(Collider other) 
    {


     Vector3 position = other.transform.position;
     Quaternion rotation = other.transform.rotation;

     if(other.gameObject.tag == "Rightbox")
     {


         spawnManager.SpawnManagerTriggerRight(position,rotation);

     }


     if(other.gameObject.tag == "Leftbox")
     {


         spawnManager.SpawnManagerTriggerLeft(position,rotation);

     }
     else
     {
         spawnManager.SpawnManagerTrigger(position,rotation);

     }


    }*/



    private void OnTriggerEnter(Collider other)
    {
        Vector3 position = other.transform.position;
        Quaternion rotation = other.transform.rotation;

        if (other.gameObject.tag == "Rightbox")
        {
            spawnManager.SpawnManagerTriggerRight(position, rotation);
        }
        else if (other.gameObject.tag == "Leftbox")
        {
            spawnManager.SpawnManagerTriggerLeft(position, rotation);
        }
        else if(other.gameObject.tag == "Cube")
        {
            spawnManager.SpawnManagerTrigger(position, rotation);
            Debug.Log("Triggered Entered");
        }


    }



}
