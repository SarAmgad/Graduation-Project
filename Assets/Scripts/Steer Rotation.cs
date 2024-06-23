using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SteerRotation : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject wheel;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;
    public float maxMoveSpeed = 220f;
    public float minMoveSpeed = 0f;

    public GameObject Needle;

    public GameObject leftHand;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    public Transform[] snappPositions;

    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;

    public float currentSteeringWheelRotation = -180;

    private float turnDampening = 250;

    public Transform directionalObject;

    public InputActionReference rightHandGripAction;
    public InputActionReference leftHandGripAction;

    public InputActionReference rightHandThumb;
    public InputActionReference leftHandThumb;

    public float move;

    public bool RightHandOnWheel
    {
        get { return rightHandOnWheel; }
        private set { rightHandOnWheel = value; }
    }

    public bool LeftHandOnWheel
    {
        get { return leftHandOnWheel; }
        private set { leftHandOnWheel = value; }
    }

    void Start()
    {
        VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
        wheel.transform.parent = Vehicle.transform;
    }

    void OnEnable()
    {
        rightHandThumb.action.Enable();
        leftHandThumb.action.Enable();
    }

    void OnDisable()
    {
        rightHandThumb.action.Disable();
        leftHandThumb.action.Disable();
    }

    void Update()
    {
        Vector2 thumbstickValueLeft = leftHandThumb.action.ReadValue<Vector2>();
        Vector2 thumbstickValueRight = rightHandThumb.action.ReadValue<Vector2>();
        float moveDirectionLeft = thumbstickValueLeft.y;
        // float moveDirectionLeft = move;
        float moveDirectionRight = thumbstickValueRight.y;

        ReleaseHandsFromWheel();
        ConvertHandRotationToSteeringWheelRotation(moveDirectionLeft);
        TurnVehicle();

        currentSteeringWheelRotation = -wheel.transform.localEulerAngles.z;
        UpdateSpeedometer(moveDirectionLeft);
        //   Vector3 forwardMovement = moveDirectionLeft * Time.deltaTime * Vehicle.transform.forward;

    }

    private void ConvertHandRotationToSteeringWheelRotation(float moveSpeed)
    {
        Debug.Log("Move Direction Function: " + moveSpeed);
        Vector3 currentRotation = wheel.transform.localEulerAngles;
        Vector3 forwardMovement = 20f * moveSpeed * Time.deltaTime * Vehicle.transform.forward;

        if (RightHandOnWheel && !LeftHandOnWheel)
        {
            float newRotationZ = rightHandOriginalParent.localEulerAngles.z;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, newRotationZ);
            Debug.Log("TurnVehicle Right Hand");
            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
        }
        else if (!RightHandOnWheel && LeftHandOnWheel)
        {
            float newRotationZ = leftHandOriginalParent.localEulerAngles.z;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, newRotationZ);
            Debug.Log("TurnVehicle Left Hand");
            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
        }
        else if (RightHandOnWheel && LeftHandOnWheel)
        {
            float newRotationRightZ = rightHandOriginalParent.localEulerAngles.z;
            float newRotationLeftZ = leftHandOriginalParent.localEulerAngles.z;
            float finalRotationZ = (newRotationRightZ + newRotationLeftZ) / 2.0f;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, finalRotationZ);
            Debug.Log("TurnVehicle Both Hands");
            VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
        }

        Debug.Log("Move Direction: " + forwardMovement);
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
        if (RightHandOnWheel && rightHandGripAction.action.ReadValue<float>() <= 0.1f)
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            RightHandOnWheel = false;
            Debug.Log("Release Right Hand");
        }

        if (LeftHandOnWheel && leftHandGripAction.action.ReadValue<float>() <= 0.1f)
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHand.transform.rotation = leftHandOriginalParent.rotation;
            LeftHandOnWheel = false;
        }

        if (!LeftHandOnWheel && !RightHandOnWheel)
        {
            wheel.transform.parent = Vehicle.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if (!RightHandOnWheel && rightHandGripAction.action.ReadValue<float>() > 0.1f)
            {
                PlaceHandOnWheel(rightHand, ref rightHandOriginalParent);
                RightHandOnWheel = true;
                Debug.Log("On Trigger Right Hand");
            }

            if (!LeftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
            {
                PlaceHandOnWheel(leftHand, ref leftHandOriginalParent);
                LeftHandOnWheel = true;
                Debug.Log("On Trigger Left Hand");
            }
        }
    }

    private void PlaceHandOnWheel(GameObject hand, ref Transform originalParent)
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

        Debug.Log("Place on Wheel");
    }
    // void UpdateSpeedometer(float moveSpeed)
    // {
    //     // Calculate the target angle based on moveSpeed 
    //     float targetAngle;
    //     float speed = moveSpeed * maxMoveSpeed;
    //     if (speed == 0f)
    //     {
    //         //     targetAngle = Mathf.Lerp(270f, 360f, speed);
    //         Debug.Log("hhhhhhhhhhhhhhhhhhh");
    //         targetAngle = 180;
    //         //Debug.Log("Target Angle" + targetAngle);
    //         Debug.Log("Speed" + speed);

    //     }
    //     else if (speed > 61)
    //     {
    //         //   targetAngle = Mathf.Lerp(0f, 60f, speedPercentage);
    //         //    targetAngle = Mathf.Lerp(360f, 90f, speed);
    //         //  Debug.Log("Target Angle" + targetAngle);
    //         Debug.Log("Speed" + speed);
    //         targetAngle = speed ;
    //     }
    //     else if (speed >= 120)
    //     {
    //         //   targetAngle = Mathf.Lerp(90f, 180f, speed);
    //         //   Debug.Log("Target Angle" + targetAngle);
    //         Debug.Log("Speed" + speed);
    //         targetAngle = speed;
    //     }
    //     else
    //     {
    //         //   targetAngle = Mathf.Lerp(180f, 270f, speed);
    //         //   Debug.Log("Target Angle" + targetAngle);
    //         Debug.Log("Speed" + speed);
    //         targetAngle = speed;
    //     }
    //     //float targetAngle = Mathf.Lerp(-180f, 180f, mappedPercentage);  // Interpolate between -180 and 180 degrees

    //     // Get the current rotation and the target rotation
    //     Quaternion currentRotation = Needle.transform.localRotation;
    //     Quaternion targetRotation = Quaternion.Euler(new Vector3(0, targetAngle, 0));

    //     // Smoothly interpolate the rotation
    //     Needle.transform.localRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 5f);
    // }

    void UpdateSpeedometer(float moveSpeed)
    {
        // Ensure moveSpeed is within the range of 0 to 1
        //  moveSpeed = Mathf.Clamp01(moveSpeed);
        float targetAngle;
        // Map moveSpeed to targetAngle within the range of 0 to 160
        if (moveSpeed >= 0 && moveSpeed <= 0.5f)
        {
            targetAngle = 360f * moveSpeed - 180f;
        }
        else
        {
            targetAngle = 360f * moveSpeed + 160f;
        }
        // Debug.Log to verify values (optional)
        Debug.Log("MoveSpeed: " + moveSpeed);
        Debug.Log("Target Angle: " + targetAngle);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, targetAngle, 0f));
        float rotationSpeed = 180f;
        float maxRotationDegreesPerSecond = rotationSpeed * Time.deltaTime;
        Needle.transform.localRotation = Quaternion.RotateTowards(Needle.transform.localRotation, targetRotation, maxRotationDegreesPerSecond);
    }

}

