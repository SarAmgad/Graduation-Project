using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class List : MonoBehaviour
{
    // public GameObject[] listofObjs;
    // public GameObject[] listofTicks;
    public GameObject preview;
    // public GameObject list;
    private float timer = 0f;
    
    private TrackedPoseDriver trackedPoseDriver;

    private void Start()
    {
        
    }

    private void Awake()
    {
        trackedPoseDriver = gameObject.transform.parent.GetComponent<TrackedPoseDriver>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer is > 2 and < 20)
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        }
        else if (timer >= 20)
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
            gameObject.SetActive(false);
        }
    }

    public void Preview(GameObject gameObject)
    {
        if (preview.transform.childCount > 0)
        {
            Destroy(preview.transform.GetChild(0).gameObject);
        }
        // Instantiate(gameObject, preview.transform);
        Instantiate(gameObject, preview.transform.position, gameObject.transform.rotation, preview.transform);
    }

    
}
