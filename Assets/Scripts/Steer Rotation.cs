using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SteerRotation : MonoBehaviour
{
    // Right Hand 
    public GameObject rightHand;
    public GameObject wheel;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;
    public float moveSpeed = 15.0f;

    // Left Hand
    public GameObject leftHand;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    public Transform[] snappPositions;

    // Wheels to control with the wheel
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
        // Make sure the wheel is parented to the vehicle at the start
        wheel.transform.parent = Vehicle.transform;
    }

    void Update()
    {
        ReleaseHandsFromWheel();
        ConvertHandRotationToSteeringWheelRotation();
        TurnVehicle();
        currentSteeringWheelRotation = -wheel.transform.localEulerAngles.z;
    }

    private void ConvertHandRotationToSteeringWheelRotation()
    {
        Vector3 forwardMovement = moveSpeed * Time.deltaTime * Vehicle.transform.forward;

        // Capture the current rotation
        Vector3 currentRotation = wheel.transform.localEulerAngles;

        if (rightHandOnWheel && !leftHandOnWheel)
        {
            float newRotationZ = rightHandOriginalParent.localEulerAngles.z;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, newRotationZ);
            Debug.Log("TurnVehicle Right Hand");

            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
        }
        else if (!rightHandOnWheel && leftHandOnWheel)
        {
            float newRotationZ = leftHandOriginalParent.localEulerAngles.z;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, newRotationZ);
            Debug.Log("TurnVehicle Left Hand");

            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
        }
        else if (rightHandOnWheel && leftHandOnWheel)
        {
            float newRotationRightZ = rightHandOriginalParent.localEulerAngles.z;
            float newRotationLeftZ = leftHandOriginalParent.localEulerAngles.z;
            float finalRotationZ = (newRotationRightZ + newRotationLeftZ) / 2.0f;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, finalRotationZ);
            Debug.Log("TurnVehicle Both Hands");

            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
        }
    }

    private void TurnVehicle()
    {
        var turn = currentSteeringWheelRotation;
        if (turn < -350)
        {
            turn += 360;
        }
        Debug.Log("Vehicle Rotate: " + turn);

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
            Debug.Log("Release Right Hand");
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
        if (!leftHandOnWheel && !rightHandOnWheel)
        {
            wheel.transform.parent = Vehicle.transform;
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
                Debug.Log("On Trigger Right Hand");
            }

            // Check if left hand is on the wheel and grip button is pressed
            if (!leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
            {
                PlaceHandOnWheel(ref leftHand, ref leftHandOriginalParent, ref leftHandOnWheel);
                Debug.Log("On Trigger Left Hand");
            }
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
        Debug.Log("Place on Wheel");
    }
}
