// using System.Collections;
// using System.Collections.Generic;
// using UnityEditorInternal;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class KeypadButton : MonoBehaviour
{
    // Keypad keypad;
    TextMeshProUGUI buttonText;
    public Transform target;
    private Vector3 offset;
    private Transform pokeAttachTransform;
    private XRBaseInteractable interactable;
    private bool isFollowing = false;
    private bool freeze = false;
    public Vector3 localAxis;
    private Vector3 intialPosition;
    public float resetSpeed = 5;
    public float followAngleTresh = 45;

    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetPosition);
        interactable.selectEntered.AddListener(Freeze);
        intialPosition = target.localPosition;

        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text.Length == 1){
            NameToButtonText();
        }
    }
    void Update()
    {
        if (freeze){
            return;
        }
        if (isFollowing){
            Vector3 localTargetPosition = target.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            target.position = target.TransformPoint(constrainedLocalTargetPosition); 
            if (target.position.z < 0.002){
                target.localPosition = new Vector3(target.position.x, target.position.y, 0.002f);
            }
        }
        else{
            target.localPosition = Vector3.Lerp(target.localPosition, intialPosition, Time.deltaTime * resetSpeed);
        }
    }

    // Vector3(9.93410776e-09,0.00238075107,-3.7252903e-08)
    public void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }

    public void Follow(BaseInteractionEventArgs select)
    {
        if (select.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)select.interactorObject;
            pokeAttachTransform = interactor.attachTransform;
            offset = target.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, target.TransformDirection(localAxis));
            if (pokeAngle < followAngleTresh){
                isFollowing = true;
                freeze = false;
            }
        }
    }

    public void ResetPosition(BaseInteractionEventArgs select) 
    {
        if(select.interactorObject is XRPokeInteractor){
            isFollowing = false;
            freeze = false;
        }    
    }

   public void Freeze(BaseInteractionEventArgs select) 
    {
        if(select.interactorObject is XRPokeInteractor){
            freeze = true;
        }    
    }
}

