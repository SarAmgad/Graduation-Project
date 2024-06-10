using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class List : MonoBehaviour
{
    // public static GameObject[] listofTicks;
    public GameObject preview;
    private float timer = 0f;
    private bool doneButton = false;
    
    private TrackedPoseDriver trackedPoseDriver;
    private void Awake()
    {
        trackedPoseDriver = gameObject.transform.parent.GetComponent<TrackedPoseDriver>();
    }

    private void Update()
    {
        // if (!StartingScene.level2) return;
        // timer += Time.deltaTime;
        // if (timer > 2 && !doneButton)
        // {
        //     trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        // }
    }
    
    public void ListMenu()
    {
        trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        CloseList();
    }

    public void CloseList()
    {
        gameObject.SetActive(false);
        doneButton = true;
    }

    public void Preview(GameObject gameObject)
    {
        if (preview.transform.childCount > 0)
        {
            Destroy(preview.transform.GetChild(0).gameObject);
        }
        Instantiate(gameObject, preview.transform.position, gameObject.transform.rotation, preview.transform);
    }
}
