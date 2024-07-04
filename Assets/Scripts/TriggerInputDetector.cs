using UnityEngine;
using UnityEngine.XR;

public class TriggerInputDetector : MonoBehaviour
{
    private InputData _inputData;
    public GameObject menu;
    public GameObject list;
    
    public static bool triggerClicked = false;
    public static bool rotationSupported = false;
    public static Quaternion controllerRot;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    void Update()
    {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            triggerClicked = true;
        }
        else
        {
            triggerClicked = false;
        }
        
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton) && Abutton && !list.activeSelf)
        {
            menu.SetActive(true);
        }
        
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool Bbutton) && Bbutton && !menu.activeSelf)
        {
            if (StartingScene.level1)
            {
                list.SetActive(true);
            }
        }

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.deviceRotation,
                out Quaternion controllerRotation))
        {
            rotationSupported = true;
            controllerRot = controllerRotation;
        }
        else
        {
            rotationSupported = false;
        }
    }
}
