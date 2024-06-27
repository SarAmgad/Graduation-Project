using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grabbing : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor interactor;
    private XRGrabInteractable currentGrabbedObject = null;
    public static Grabbing instance = new();

    public static XRGrabInteractable grabbedObject;
    
    void OnEnable()
    {
        interactor.selectEntered.AddListener(OnSelectEntered);
        interactor.selectExited.AddListener(OnSelectExited);
    }

    void OnDisable()
    {
        interactor.selectEntered.RemoveListener(OnSelectEntered);
        interactor.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject is XRGrabInteractable grabInteractable)
        {
            currentGrabbedObject = grabInteractable;
            grabbedObject = grabInteractable;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if ((Object)args.interactableObject == currentGrabbedObject)
        {
            currentGrabbedObject = null;
        }
    }

    public XRGrabInteractable GetCurrentGrabbedObject()
    {
        return currentGrabbedObject;
    }
}
