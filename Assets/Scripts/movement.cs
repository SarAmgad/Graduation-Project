using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour

{
    public static int score = 0;
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
        float currentSpeed = VehicleRigidBody.velocity.magnitude;

        if (isAccelerating)
        {
            PlaySound(accelerationClip);
            // isAccelerating = true;
            // isDecelerating = false;
        }

        else if (isDecelerating)
        {
            PlaySound(decelerationClip);
            // isDecelerating = true;
            // isAccelerating = false;
        }
        // Update previous speed for the next frame
        previousSpeed = currentSpeed;



    }



    private void OnTriggerEnter(Collider other)
    {
        Vector3 position = other.transform.position;
        Quaternion rotation = other.transform.rotation;

        // ////debug.log("Entered");


        // Check if the collider is an audio collider
        if (other.gameObject.tag == "RightAudio")
        {
            audioSource.volume = 0.2f;
            isRightDetected = true;
            if (!isRightAudio)
            {
                other.GetComponent<AudioSource>().Play();
                // PlaySound(rightSound);
                initialVehicleRotation = gameObject.transform.localEulerAngles.y;
                ////debug.log("Initialllll Rotattt" + initialVehicleRotation);
                //  StartCoroutine(DelayCheckRotation());
                isRightAudio = true;
                // IsScoreUpdated = true;
            }

        }
        else if (other.gameObject.tag == "LeftAudio")
        {
            audioSource.volume = 0.2f;
            if (!isLeftAudio)
            {
                other.GetComponent<AudioSource>().Play();
                isLeftAudio = true;
            }
            //PlaySound(leftSound);
            initialVehicleRotation = gameObject.transform.localEulerAngles.y;
            ////debug.log("Lefttttttt");
            // StartCoroutine(DelayCheckRotation());
            //isRightAudio = true;
            // IsScoreUpdated = true;
            isLeftDetected = true;
        }

        audioSource.volume = 0.6f;
        isLeftAudio = false;
        isRightAudio = false;

        if (isRightDetected)
        {
            ////debug.log("Total Score" + score);
            if (other.gameObject.tag == "DetectRight")
            {
                score++;
                isRightDetected = false;
                ////debug.log("Total Score" + score);
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
                ////debug.log("Total Score" + score);
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
            // ////debug.log("Playing sound: " + clip.name);
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
    //     ////debug.log("Initiall" + initialVehicleRotation);
    //     IsScoreUpdated = false;
    // }
    // void CheckRightRotation()
    // {
    //     ////debug.log("finalVehicle" + finalVehicleRotation);
    //     if (isRightAudio && (finalVehicleRotation > initialVehicleRotation))
    //     {
    //         ////debug.log("Right Rotation" + finalVehicleRotation + "Initiall+30" + (initialVehicleRotation + 30));
    //         ////debug.log("Initiall" + initialVehicleRotation);
    //         score++;

    //         ////debug.log("Scoreeeeeeee" + score);
    //     }
    //     else
    //     {
    //         if (score == 0)
    //             score = 0;
    //         else
    //             score--;
    //         ////debug.log("Left Rotation" + score);
    //     }
    // }
    // void CheckLeftRotation()
    // {
    //     if (!isRightAudio && (finalVehicleRotation < initialVehicleRotation))
    //     {
    //         score++;
    //         ////debug.log("Scoreeeeeeee" + score);
    //     }
    //     else
    //     {
    //         if (score == 0)
    //             score = 0;
    //         else
    //             score--;
    //         ////debug.log("Left Rotation" + score);
    //     }
    // }
}