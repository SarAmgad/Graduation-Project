using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SteerRotation : MonoBehaviour
{
    // Right Hand 
    public GameObject rightHand;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;
    public float moveSpeed = 15.0f;

    // Left Hand
    public GameObject leftHand;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    public Transform[] snappPositions;

    // Wheels to contol with the wheel
    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;

    public float currentSteeringWheelRotation = 0;

    private float turnDampening = 250;

    public Transform directionalObject;
    // Input Action References
    public InputActionReference rightHandGripAction;
    public InputActionReference leftHandGripAction;

    void Start()
    {
        VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ReleaseHandsFromWheel();
        ConvertHandRotationToSteeringWheelRotation();
        TurnVehicle();
        currentSteeringWheelRotation = -transform.rotation.eulerAngles.z;
    }
    private void ConvertHandRotationToSteeringWheelRotation()
    {
        Vector3 forwardMovement = moveSpeed * Time.deltaTime * Vehicle.transform.forward;
        if (rightHandOnWheel == true && leftHandOnWheel == false)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.localRotation = newRot;
            transform.parent = directionalObject;
            Debug.Log("TurnVehiclelll1");

            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
        }
        else if (rightHandOnWheel == false && leftHandOnWheel == true)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.localRotation = newRot;
            transform.parent = directionalObject;
            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
            Debug.Log("TurnVehiclelll2");
        }
        else if (rightHandOnWheel == true && leftHandOnWheel == true)
        {
            Quaternion newRotRight = Quaternion.Euler(0, 0, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion newRotLeft = Quaternion.Euler(0, 0, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion finalRot = Quaternion.Slerp(newRotLeft, newRotRight, 1.0f / 2.0f);
            directionalObject.localRotation = finalRot;
            transform.parent = directionalObject;
            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
            Debug.Log("TurnVehiclelll1");
        }
    }
    private void TurnVehicle()
    {
        var turn = currentSteeringWheelRotation;
        if (turn < -350)
        {
            turn += 360;
        }
        Debug.Log("Vehicleeee Rotattte" + turn);

        VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, turn, 0), Time.deltaTime * turnDampening));

    }

    private void ReleaseHandsFromWheel()
    {
        // Check if the right hand is on the wheel and the grip button is released
        if (rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() <= 0.1f)
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            rightHandOnWheel = false;
            Debug.Log("Release Handddd");
        }

        // Check if the left hand is on the wheel and the grip button is released
        if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() <= 0.1f)
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHand.transform.rotation = leftHandOriginalParent.rotation;
            leftHandOnWheel = false;
        }

        // Reset steering wheel to not be a parent of directional object if the wheel is released
        if (leftHandOnWheel == false && rightHandOnWheel == false)
        {
            transform.parent = transform.root;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            // Check if right hand is on the wheel and grip button is pressed
            if (!rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() > 0.1f)
            {
                PlaceHandOnWheel(ref rightHand, ref rightHandOriginalParent, ref rightHandOnWheel);

                Debug.Log("On TriggerrrrrrrRight");
            }

            // Check if left hand is on the wheel and grip button is pressed
            if (!leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
            {
                PlaceHandOnWheel(ref leftHand, ref leftHandOriginalParent, ref leftHandOnWheel);

                Debug.Log("On TriggerrrrrrrLeftt");
            }

            Debug.Log("eeeeeee");
        }

    }

    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel)
    {
        var shortestDistance = Vector3.Distance(snappPositions[0].position, hand.transform.position);
        var bestSnapp = snappPositions[0];

        foreach (var snappPosition in snappPositions)
        {
            if (snappPosition.childCount == 0)
            {
                var distance = Vector3.Distance(snappPosition.position, hand.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestSnapp = snappPosition;
                }
            }
        }
        originalParent = hand.transform.parent;
        hand.transform.parent = bestSnapp.transform;
        hand.transform.position = bestSnapp.transform.position;

        handOnWheel = true;
        Debug.Log("Placeeeee onnnn wheeel");
    }





}
