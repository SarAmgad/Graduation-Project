using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : MonoBehaviour
{
    // Start is called before the first frame update
    float stopThreshold = 0;
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
            GameObject otherGameObject = other.gameObject;
            Transform parentTransform = otherGameObject.transform.parent;
            GameObject parentObject = parentTransform.gameObject;
            Rigidbody rigidParent = parentObject.GetComponent<Rigidbody>();
            
            meshRendererStopSign.enabled = true;
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
            Debug.Log("Initial " + initialVelocity.z);
            if (rigidParent.velocity.z == stopThreshold)
            {
                isStopped = true;

                if (!IsScoreUpdated)
                {
                    scoreForDetectedSigns++;
                    // Debug.Log(" " + scoreForDetectedSigns);
                    IsScoreUpdated = true;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        Transform parentTransform = otherGameObject.transform.parent;
        GameObject parentObject = parentTransform.gameObject;
        Rigidbody rigidParent = parentObject.GetComponent<Rigidbody>();
        if (other.CompareTag("Car"))
        {
            if (!IsScoreUpdated)
            {
                isStopped = false;

                if (scoreForDetectedSigns > 0)
                {
                    scoreForDetectedSigns++;
                }
                IsScoreUpdated = false;

            }

        }
    }
}
