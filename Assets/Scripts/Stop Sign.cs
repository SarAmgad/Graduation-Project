using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : MonoBehaviour
{
    // Start is called before the first frame update
    public float stopThreshold = 0f;
    private bool isStopped = false;
    bool IsScoreUpdated = false;
    public static int scoreForDetectedSigns;
    private Vector3 initialVelocity = new Vector3();
    MeshRenderer meshRendererStopSign;

    void Start()
    {
        meshRendererStopSign = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            meshRendererStopSign.enabled = true;
            Debug.Log("Vehicle entered the stop sign area.");
            initialVelocity = other.attachedRigidbody.velocity;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {

            Debug.Log("Current " + other.attachedRigidbody.velocity.z);
            Debug.Log("Initial " + initialVelocity.z);
            if (other.attachedRigidbody.velocity.z == stopThreshold)
            {
                isStopped = true;

                if (!IsScoreUpdated)
                {
                    scoreForDetectedSigns++;
                    IsScoreUpdated = true;
                }

                Debug.Log("Vehicle stopped" + scoreForDetectedSigns);
                Debug.Log("Initial " + initialVelocity.z);

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {

            Debug.Log("Current " + other.attachedRigidbody.velocity.z);
            Debug.Log("Initial " + initialVelocity.z);
            if (!IsScoreUpdated)
            {
                isStopped = false;

                if (scoreForDetectedSigns > 0)
                {
                    scoreForDetectedSigns++;
                }
                IsScoreUpdated = true;
                Debug.Log("Vehicle stopped" + scoreForDetectedSigns);
                Debug.Log("Initial " + initialVelocity.z);

            }

        }
    }
}
