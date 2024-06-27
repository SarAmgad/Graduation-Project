using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class UIManager : MonoBehaviour
{
    private GameObject FoundPanel;
    private GameObject endCanvas;
    private GameObject list;
    private GameObject menu;
    private int COUNT;
    private TrackedPoseDriver trackedPoseDriver;
    private XRGrabInteractable[] grabInteractables;


    private void Awake()
    {
        // trackedPoseDriver = GameObject.Find("XR Origin (XR Rig)/Camera Offset/Main Camera").GetComponent<TrackedPoseDriver>();
        trackedPoseDriver = GameObject.Find("XR Origin Hands/Camera Offset/Main Camera").GetComponent<TrackedPoseDriver>();
        grabInteractables = FindObjectsOfType<XRGrabInteractable>();
    }

    void Start()
    {
        // FoundPanel = GetCanvas(FoundPanel, "XR Origin (XR Rig)/Camera Offset/Main Camera/Found Canvas");
        FoundPanel = GetCanvas(FoundPanel, "XR Origin Hands/Camera Offset/Main Camera/Found Canvas");

        // endCanvas = GetCanvas(endCanvas, "XR Origin (XR Rig)/Camera Offset/Main Camera/End Canvas");
        endCanvas = GetCanvas(endCanvas, "XR Origin Hands/Camera Offset/Main Camera/End Canvas");

        // list = GetCanvas(list, "XR Origin (XR Rig)/Camera Offset/Main Camera/XRCanvas");
        list = GetCanvas(list, "XR Origin Hands/Camera Offset/Main Camera/XRCanvas");

        // menu = GetCanvas(menu, "XR Origin (XR Rig)/Camera Offset/Main Camera/Menu");
        menu = GetCanvas(menu, "XR Origin Hands/Camera Offset/Main Camera/XRCanvas");


        Tracking();
        SetCount();
    }

    void Update()
    {
        if (FoundObjectDestroy.objectsList.Count != 0 && FoundObjectDestroy.objectsList.Count == COUNT && !FoundPanel.activeSelf)
        {
            endCanvas.SetActive(true);
        }

        Tracking();
    }

    private GameObject GetCanvas(GameObject canvas, string name)
    {
        if (canvas == null)
        {
            return GameObject.Find(name);
        }
        return canvas;
    }

    private void SetCount()
    {
        if(StartingScene.level1){
            COUNT = 6;
        }
        else if(StartingScene.level2){
            COUNT = 5;
        }
    }

    private void Tracking()
    {
        if (list.activeSelf || menu.activeSelf || endCanvas.activeSelf)
        {
            Debug.Log("Tracking");
            // trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
            foreach (XRGrabInteractable obj in grabInteractables)
            {
                if (obj)
                {
                    obj.enabled = false;
                }
            }
        }
        else
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
            foreach (XRGrabInteractable obj in grabInteractables)
            {
                if (obj)
                {
                    obj.enabled = true;
                }
            }
        }
    }
}
