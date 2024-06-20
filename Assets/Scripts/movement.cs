using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour

{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
    public AudioSource audioSource;
    public AudioClip rightSound;
    public AudioClip leftSound;


    // Update is called once per frame
    void Update()
    {
        // float hmovement = Input.GetAxis("Horizontal")* movementSpeed;
        // float vmovement = Input.GetAxis("Vertical")* movementSpeed;
        // transform.Translate(new Vector3(hmovement,0,vmovement)*Time.deltaTime);


    }


    private void Start()
    {
        // Ensure the AudioSource component is attached to the GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (audioSource.playOnAwake)
        {
            audioSource.playOnAwake = false;
        }
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



    /*private void OnTriggerEnter(Collider other)
    {
        Vector3 position = other.transform.position;
        Quaternion rotation = other.transform.rotation;
        if (other.gameObject.tag == "RightAudio")
        {
            audioSource.PlayOneShot(rightSound);
        }
        else if (other.gameObject.tag =="left audio")
        {
            audioSource.PlayOneShot(leftSound);
        }

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


    }*/


    private void OnTriggerEnter(Collider other)
    {
        Vector3 position = other.transform.position;
        Quaternion rotation = other.transform.rotation;

        // Check if the collider is an audio collider
        if (other.gameObject.tag == "RightAudio")
        {
            PlaySound(rightSound);
        }
        else if (other.gameObject.tag == "LeftAudio")
        {
            PlaySound(leftSound);
        }
        else
        {
            // Check for other tags if not an audio collider
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

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            Debug.Log("Playing sound: " + clip.name);
            audioSource.PlayOneShot(clip);
        }
    }



}
