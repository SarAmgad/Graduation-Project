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
        // score = StopSign.scoreForDetectedSigns;


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Car"))
        {
           //Debug.log("Nameeee" + other.name);
            Transform parentTransform = other.gameObject.transform.parent;
            GameObject parentObject = parentTransform.gameObject;
            Rigidbody rigidParent = parentObject.GetComponent<Rigidbody>();
            meshRendererSlowDownSign.enabled = true;
           //Debug.log("Vehicle entered the slow down.");
            initialVelocity = rigidParent.velocity;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            GameObject otherGameObject = other.gameObject;
            Transform parentTransform = otherGameObject.transform.parent;
            GameObject parentObject = parentTransform.gameObject;
            Rigidbody rigidParent = parentObject.GetComponent<Rigidbody>();
           //Debug.log("Car velocity "+rigidParent.velocity);
            if ((rigidParent.velocity.z - initialVelocity.z) < slowDownThreshold)
            {
                if (!IsScoreUpdated)
                {

                    ////Debug.log("Current " + other.attachedRigidbody.velocity.z);
                    ////Debug.log("Initial " + initialVelocity.z);

                    isSlowedDown = true;
                    StopSign.scoreForDetectedSigns ++;
                    IsScoreUpdated = true;
                   //Debug.log("Vehicle stopped" + score);
                   //Debug.log("Initial " + initialVelocity.z);
                    

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
                if (StopSign.scoreForDetectedSigns > 0)
                {
                    StopSign.scoreForDetectedSigns --;
                }

            }
            IsScoreUpdated = false;
        }
        
    }
}
