using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Rotation : MonoBehaviour
{
    public GameObject objectToRotate;
    private InputData _inputData;

    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerInputDetector.triggerClicked)
        {
            if (TriggerInputDetector.rotationSupported )
            {
                objectToRotate.transform.rotation = TriggerInputDetector.controllerRot;
            }
        }
    }
}
