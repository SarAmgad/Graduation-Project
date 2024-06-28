using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlowDownSign : MonoBehaviour
{
    // Start is called before the first frame update


    public float slowDownThreshold = 0.3f;
    private bool isSlowedDown = false;
    bool IsScoreUpdated = false;

    public int  score;

    // public StopSign stopSignScript;

    private Vector3 initialVelocity = new Vector3();

    MeshRenderer meshRendererSlowDownSign;


    void Start()
    {
        meshRendererSlowDownSign = GetComponent<MeshRenderer>();
        score = StopSign.scoreForDetectedSigns;


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            meshRendererSlowDownSign.enabled = true;
            Debug.Log("Vehicle entered the slow down.");
            initialVelocity = other.attachedRigidbody.velocity;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {

            if (other.attachedRigidbody.velocity.z - initialVelocity.z < slowDownThreshold)
            {
                if (!IsScoreUpdated)
                {

                    // Debug.Log("Current " + other.attachedRigidbody.velocity.z);
                    // Debug.Log("Initial " + initialVelocity.z);

                    isSlowedDown = true;
                    score++;
                    IsScoreUpdated = true;
                    Debug.Log("Vehicle stopped" + score);
                    Debug.Log("Initial " + initialVelocity.z);
                    

                }

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (!IsScoreUpdated)
            {
                isSlowedDown = false;
                if (score > 0)
                {
                    score--;
                }

            }
        }
        IsScoreUpdated = false;
    }
}
