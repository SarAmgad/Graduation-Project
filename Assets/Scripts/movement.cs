using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour

{
    static int score = 0;

    public float movementSpeed = 10f;
    public AudioSource audioSource;
    public AudioClip accelerationClip;
    public AudioClip decelerationClip;

    public AudioClip startingAudio;

    private float previousSpeed;
    public bool isAccelerating = false;
    public bool isDecelerating = false;
    public SpawnManager spawnManager;
    // public AudioClip rightSound;
    // public AudioClip leftSound;

    float finalVehicleRotation;
    float initialVehicleRotation;

    bool isRightAudio = false;
    bool isLeftAudio = false;
    bool IsScoreUpdated = false;
    bool isRightDetected = false;
    bool isLeftDetected = false;


    public bool isEdgeDetected = false;

    private Rigidbody VehicleRigidBody;
    float speed;
    float pitch;




    // Update is called once per frame



    private void Start()
    {
        // Ensure the AudioSource component is attached to the GameObject
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
        // if (audioSource.playOnAwake)
        // {
        //     audioSource.playOnAwake = false;
        // }
        PlaySound(startingAudio);
        VehicleRigidBody = GetComponent<Rigidbody>();
        previousSpeed = VehicleRigidBody.velocity.magnitude;

        //audioSource.loop = true;


    }
    void Update()
    {
        // float hmovement = Input.GetAxis("Horizontal")* movementSpeed;
        // float vmovement = Input.GetAxis("Vertical")* movementSpeed;
        // transform.Translate(new Vector3(hmovement,0,vmovement)*Time.deltaTime);
        //PlaySound(mainAudio);
        float currentSpeed = VehicleRigidBody.velocity.magnitude;

        if (!isAccelerating)
        {
            PlaySound(accelerationClip);
            isAccelerating = true;
            // isDecelerating = false;
        }

        else if (!isDecelerating)
        {
            PlaySound(decelerationClip);
            isDecelerating = true;
            // isAccelerating = false;
        }
        // Update previous speed for the next frame
        previousSpeed = currentSpeed;



    }



    private void OnTriggerEnter(Collider other)
    {
        Vector3 position = other.transform.position;
        Quaternion rotation = other.transform.rotation;

        Debug.Log("Entered");


        // Check if the collider is an audio collider
        if (other.gameObject.tag == "RightAudio")
        {
            audioSource.volume = 0.5f;
            isRightDetected = true;
            if (!isRightAudio)
            {
                other.GetComponent<AudioSource>().Play();
                // PlaySound(rightSound);
                initialVehicleRotation = gameObject.transform.localEulerAngles.y;
                Debug.Log("Initialllll Rotattt" + initialVehicleRotation);
                //  StartCoroutine(DelayCheckRotation());
                isRightAudio = true;
                // IsScoreUpdated = true;
            }

        }
        else if (other.gameObject.tag == "LeftAudio")
        {
            audioSource.volume = 0.5f;
            if (!isLeftAudio)
            {
                other.GetComponent<AudioSource>().Play();
                isLeftAudio = true;
            }
            //PlaySound(leftSound);
            initialVehicleRotation = gameObject.transform.localEulerAngles.y;
            Debug.Log("Lefttttttt");
            // StartCoroutine(DelayCheckRotation());
            //isRightAudio = true;
            // IsScoreUpdated = true;
            isLeftDetected = true;
        }

        // Check for other tags if not an audio collider
        if (other.gameObject.tag == "DetectRight" && gameObject.tag == "Car")
        {


            spawnManager.SpawnManagerTriggerRight(position, rotation);
            Debug.Log("Triggered Entered Right" + score);

        }
        else if (other.gameObject.tag == "DetectLeft" && gameObject.tag == "Car")
        {

            spawnManager.SpawnManagerTriggerLeft(position, rotation);
            Debug.Log("Triggered Entered" + score);
        }
        else if (other.gameObject.tag == "Cube" && gameObject.tag == "Car")
        {

            spawnManager.SpawnManagerTrigger(position, rotation);
            Debug.Log("Triggered Entered" + score);
        }
        audioSource.volume = 1f;
        isLeftAudio = false;
        isRightAudio = false;

        if (isRightDetected)
        {
            Debug.Log("Total Score" + score);
            if (other.gameObject.tag == "DetectRight")
            {
                score++;
                isRightDetected = false;
                Debug.Log("Total Score" + score);
            }
            else if (other.gameObject.tag == "Forward")
            {
                if (score > 0)
                    score--;
                isRightDetected = false;
            }
        }
        if (isLeftDetected)
        {
            if (other.gameObject.CompareTag("DetectLeft"))
            {
                score++;
                isLeftDetected = false;
                Debug.Log("Total Score" + score);
            }
            else if (other.gameObject.CompareTag("ForwardLeft"))
            {
                if (score > 0)
                    score--;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            audioSource.volume = 0.5f;
            VehicleRigidBody.velocity = Vector3.zero;
            VehicleRigidBody.angularVelocity = Vector3.zero;
            isEdgeDetected = true;
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            isEdgeDetected = false;
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