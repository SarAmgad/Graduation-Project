using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour

{
    static int score = 0;
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
    public AudioSource audioSource;
    public AudioClip rightSound;
    public AudioClip leftSound;

    float finalVehicleRotation;
    float initialVehicleRotation;

    bool isRightAudio = false;
    bool IsScoreUpdated = false;
    bool isRightDetected = false;
    bool isLeftDetected = false;


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
              isRightDetected = true;
            if (!IsScoreUpdated)
            {
                PlaySound(rightSound);
                initialVehicleRotation = gameObject.transform.localEulerAngles.y;
                Debug.Log("Initialllll Rotattt" + initialVehicleRotation);
              //  StartCoroutine(DelayCheckRotation());
                isRightAudio = true;
                IsScoreUpdated = true;
            }


        }
        else if (other.gameObject.tag == "LeftAudio")
        {
            PlaySound(leftSound);
            initialVehicleRotation = gameObject.transform.localEulerAngles.y;
            Debug.Log("Lefttttttt");
           // StartCoroutine(DelayCheckRotation());
            isRightAudio = true;
            IsScoreUpdated = true;
            isLeftDetected = true;
        }
        else
        {
            // Check for other tags if not an audio collider
            if (other.gameObject.tag == "Rightbox" && gameObject.tag == "Car")
            {

                spawnManager.SpawnManagerTriggerRight(position, rotation);
            }
            else if (other.gameObject.tag == "Leftbox" && gameObject.tag == "Car")
            {

                spawnManager.SpawnManagerTriggerLeft(position, rotation);
            }
            else if (other.gameObject.tag == "Cube" && gameObject.tag == "Car")
            {

                spawnManager.SpawnManagerTrigger(position, rotation);
                Debug.Log("Triggered Entered" + score);
            }
        }
        if(isRightDetected){
            Debug.Log("nnnnnnnnn" + score);
            if(other.gameObject.tag == "DetectRight"){
                score++;
                isRightDetected = false;
                Debug.Log("nnnnnnnnn" + score);
            }else if(other.gameObject.tag == "Forward"){
                if(score > 0)
                    score --;
                isRightDetected = false;
            }
        }
        if(isLeftDetected){
            if(other.gameObject.tag == "DetectLeft"){
                score++;
                isLeftDetected = false;
                Debug.Log("nnnnnnnnn" + score);
            }else if(other.gameObject.tag == "ForwardLeft"){
                if(score > 0)
                    score --;
                isLeftDetected = false;
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

    // IEnumerator DelayCheckRotation()
    // {
    //     yield return new WaitForSeconds(2f);
    //     CheckRightRotation();
    //     CheckLeftRotation();
    //     finalVehicleRotation = gameObject.transform.localEulerAngles.y;
    //     Debug.Log("Initiall" + initialVehicleRotation);
    //     IsScoreUpdated = false;
    // }
    // void CheckRightRotation()
    // {
    //     Debug.Log("finalVehicle" + finalVehicleRotation);
    //     if (isRightAudio && (finalVehicleRotation > initialVehicleRotation))
    //     {
    //         Debug.Log("Right Rotation" + finalVehicleRotation + "Initiall+30" + (initialVehicleRotation + 30));
    //         Debug.Log("Initiall" + initialVehicleRotation);
    //         score++;

    //         Debug.Log("Scoreeeeeeee" + score);
    //     }
    //     else
    //     {
    //         if (score == 0)
    //             score = 0;
    //         else
    //             score--;
    //         Debug.Log("Left Rotation" + score);
    //     }
    // }
    // void CheckLeftRotation()
    // {
    //     if (!isRightAudio && (finalVehicleRotation < initialVehicleRotation))
    //     {
    //         score++;
    //         Debug.Log("Scoreeeeeeee" + score);
    //     }
    //     else
    //     {
    //         if (score == 0)
    //             score = 0;
    //         else
    //             score--;
    //         Debug.Log("Left Rotation" + score);
    //     }
    // }
}