// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using Unity.XR.CoreUtils;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.XR;
// using UnityEngine.XR.Interaction.Toolkit;


// public class SteerRotation : MonoBehaviour
// {
//     private float initialRotationHand;
//     private Quaternion initialWheelRotation;
//     private Quaternion initialVehicleRotation;
//     private InputData _inputData;
//     public GameObject rightHand;
//     public GameObject wheel;
//     private Transform rightHandOriginalParent;
//     private bool rightHandOnWheel = false;
//     public float maxMoveSpeed = 220f;
//     public float minMoveSpeed = 0f;
//     float lastRotationWheel;
//     float lastRotationCar;
//     bool isColliding;

//     float currentRotation;
//     public GameObject Needle;

//     public GameObject leftHand;
//     private Transform leftHandOriginalParent;
//     private bool leftHandOnWheel = false;

//     public Transform[] snappPositions;

//     public GameObject Vehicle;
//     private Rigidbody VehicleRigidBody;

//     public float currentSteeringWheelRotation = 0;

//     private float turnDampening = 300;

//     public Transform directionalObject;

//     public InputActionReference rightHandGripAction;
//     public InputActionReference leftHandGripAction;

//     public InputActionReference rightHandThumb;
//     public InputActionReference leftHandThumb;
//     public float move;



//     public bool RightHandOnWheel
//     {
//         get { return rightHandOnWheel; }
//         private set { rightHandOnWheel = value; }
//     }

//     public bool LeftHandOnWheel
//     {
//         get { return leftHandOnWheel; }
//         private set { leftHandOnWheel = value; }
//     }

//     void Start()
//     {
//         _inputData = GetComponent<InputData>();
//         VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
//         wheel.transform.parent = Vehicle.transform;
//         initialVehicleRotation = Vehicle.transform.localRotation;
//         initialWheelRotation = wheel.transform.localRotation;
//     }

//     void OnEnable()
//     {
//         rightHandThumb.action.Enable();
//         leftHandThumb.action.Enable();
//     }

//     void OnDisable()
//     {
//         rightHandThumb.action.Disable();
//         leftHandThumb.action.Disable();
//     }

//     void Update()
//     {
//         Vector2 thumbstickValueLeft = leftHandThumb.action.ReadValue<Vector2>();
//         Vector2 thumbstickValueRight = rightHandThumb.action.ReadValue<Vector2>();
//         float moveDirectionLeft = thumbstickValueLeft.y;

//         // float moveDirectionLeft = move;
//         float moveDirectionRight = thumbstickValueRight.y;
//         currentRotation = leftHand.transform.localEulerAngles.z;
//         /// Debug.Log("iffffffffffffff" + currentRotation + "  initial" + initialRotationHand);
//         if (!RightHandOnWheel && !LeftHandOnWheel)
//         {
//             wheel.transform.localRotation = Quaternion.RotateTowards(wheel.transform.localRotation, initialWheelRotation, Time.deltaTime * turnDampening);
//             Vehicle.transform.localRotation = Quaternion.RotateTowards(Vehicle.transform.localRotation, initialVehicleRotation, Time.deltaTime * turnDampening);
//         }
//         if (LeftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f && Math.Abs(currentRotation - initialRotationHand) > 0.4f)
//         {

//             isColliding = _inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 joyStickValueL);
//             ConvertHandRotationToSteeringWheelRotation(joyStickValueL.y);
//             Debug.Log("iffffffffffffff" + currentRotation + "  initial" + initialRotationHand);
//             currentSteeringWheelRotation = leftHand.transform.localEulerAngles.z;
//         }


//         // if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 joyStickValueL))
//         // {
//         //     UpdateSpeedometer(joyStickValueL.y);
//         // }



//         if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 joyStickValueR))
//         {
//             // Debug.Log("Value Righttttttttttt  " + joyStickValueR.y);
//             UpdateSpeedometer(joyStickValueR.y);
//         }

//         ReleaseHandsFromWheel();
//         //   ConvertHandRotationToSteeringWheelRotation(joyStickValueR.y);
//         //  TurnVehicle();
//     }

//     private void ConvertHandRotationToSteeringWheelRotation(float moveSpeed)
//     {
//         // Debug.Log("Move Direction Function: " + moveSpeed);
//         Vector3 currentRotation = wheel.transform.localEulerAngles;
//         Vector3 forwardMovement = 220f * moveSpeed * Vehicle.transform.forward;

//         if (RightHandOnWheel && !LeftHandOnWheel)
//         {
//             float newRotationZ = rightHand.transform.localEulerAngles.z;
//             wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, -newRotationZ);
//             //  Debug.Log("TurnVehicle Right Hand" + newRotationZ + "  " + currentRotation.z);
//             VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);

//         }
//         else if (!RightHandOnWheel && LeftHandOnWheel)
//         {
//             float newRotationZ = leftHand.transform.localEulerAngles.z;
//             wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, -newRotationZ);
//             // Debug.Log("TurnVehicle Left Hand" + newRotationZ + "  " + currentRotation.z);
//             VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
//             currentSteeringWheelRotation = wheel.transform.localEulerAngles.z;
//             TurnVehicle();
//         }
//         else if (RightHandOnWheel && LeftHandOnWheel)
//         {
//             float newRotationRightZ = rightHand.transform.localEulerAngles.z;
//             float newRotationLeftZ = leftHand.transform.localEulerAngles.z;
//             float finalRotationZ = (newRotationRightZ + newRotationLeftZ) / 2.0f;
//             wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, -finalRotationZ);
//             // Debug.Log("TurnVehicle Both Hands");
//             VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
//             // TurnVehicle();
//         }

//         // Debug.Log("Move Direction: " + forwardMovement);
//     }

//     private void TurnVehicle()
//     {
//         var turn = currentSteeringWheelRotation;
//         if (turn < -350)
//         {
//             turn += 360;
//         }
//         // Debug.Log("Vehicle Rotate: " + turn);

//         VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, turn, 0), Time.deltaTime * turnDampening));
//     }

//     private void ReleaseHandsFromWheel()
//     {

//         if (RightHandOnWheel && rightHandGripAction.action.ReadValue<float>() <= 0.1f)
//         {
//             rightHand.transform.parent = rightHandOriginalParent;
//             rightHand.transform.position = rightHandOriginalParent.position;
//             rightHand.transform.rotation = rightHandOriginalParent.rotation;
//             RightHandOnWheel = false;

//             // Debug.Log("Release Right Hand");
//         }

//         if (LeftHandOnWheel && leftHandGripAction.action.ReadValue<float>() <= 0.1f)
//         {
//             leftHand.transform.parent = leftHandOriginalParent;
//             leftHand.transform.position = leftHandOriginalParent.position;
//             leftHand.transform.rotation = leftHandOriginalParent.rotation;
//             LeftHandOnWheel = false;
//             initialRotationHand = leftHand.transform.localEulerAngles.z;
//             // currentSteeringWheelRotation = 0;

//             ConvertHandRotationToSteeringWheelRotation(0);
//             //   Debug.Log("Release Left Hand" + LeftHandOnWheel);
//         }

//         if (!LeftHandOnWheel && !RightHandOnWheel)
//         {
//             wheel.transform.parent = Vehicle.transform;
//         }
//     }

//     private void OnTriggerStay(Collider other)
//     {
//         if (other.CompareTag("PlayerHandR"))
//         {

//             if (!RightHandOnWheel && rightHandGripAction.action.ReadValue<float>() > 0.1f)
//             {
//                 PlaceHandOnWheel(rightHand, ref rightHandOriginalParent);
//                 RightHandOnWheel = true;
//                 //TurnVehicle();
//                 // Debug.Log("On Trigger Right Hand");
//             }
//         }
//         if (other.CompareTag("PlayerHandL"))
//         {


//             if (!LeftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
//             {
//                 PlaceHandOnWheel(leftHand, ref leftHandOriginalParent);
//                 LeftHandOnWheel = true;
//                 // Debug.Log("On Trigger Left Hand");
//                 //initialRotationHand = leftHand.transform.eulerAngles.z;
//                 //   Debug.Log("On Trigger Left Hand" + initialRotationHand);
//                 //isColliding = _inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joyStickValueR);
//                 // ConvertHandRotationToSteeringWheelRotation(joyStickValueR.y);
//                 // TurnVehicle();
//                 //  currentSteeringWheelRotation = wheel.transform.localEulerAngles.z;
//                 initialRotationHand = leftHand.transform.localEulerAngles.z;
//             }
//             else if (LeftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
//             {

//                 // isColliding = _inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joyStickValueR);
//                 // ConvertHandRotationToSteeringWheelRotation(joyStickValueR.y);
//                 // Debug.Log("iffffffffffffff" + (leftHand.transform.localEulerAngles.z));
//                 // currentSteeringWheelRotation = leftHand.transform.localEulerAngles.z;
//                 initialRotationHand = leftHand.transform.localEulerAngles.z;
//             }




//         }
//     }

//     private void PlaceHandOnWheel(GameObject hand, ref Transform originalParent)
//     {
//         var shortestDistance = Vector3.Distance(snappPositions[0].position, hand.transform.position);
//         var bestSnapp = snappPositions[0];

//         foreach (var snappPosition in snappPositions)
//         {
//             if (snappPosition.childCount == 0)
//             {
//                 var distance = Vector3.Distance(snappPosition.position, hand.transform.position);
//                 if (distance < shortestDistance)
//                 {
//                     shortestDistance = distance;
//                     bestSnapp = snappPosition;
//                 }
//             }
//         }
//         originalParent = hand.transform.parent;
//         hand.transform.parent = bestSnapp.transform;
//         hand.transform.position = bestSnapp.transform.position;

//         // Debug.Log("Place on Wheel");
//     }
//     // void UpdateSpeedometer(float moveSpeed)
//     // {
//     //     // Calculate the target angle based on moveSpeed 
//     //     float targetAngle;
//     //     float speed = moveSpeed * maxMoveSpeed;
//     //     if (speed == 0f)
//     //     {
//     //         //     targetAngle = Mathf.Lerp(270f, 360f, speed);
//     //         Debug.Log("hhhhhhhhhhhhhhhhhhh");
//     //         targetAngle = 180;
//     //         //Debug.Log("Target Angle" + targetAngle);
//     //         Debug.Log("Speed" + speed);

//     //     }
//     //     else if (speed > 61)
//     //     {
//     //         //   targetAngle = Mathf.Lerp(0f, 60f, speedPercentage);
//     //         //    targetAngle = Mathf.Lerp(360f, 90f, speed);
//     //         //  Debug.Log("Target Angle" + targetAngle);
//     //         Debug.Log("Speed" + speed);
//     //         targetAngle = speed ;
//     //     }
//     //     else if (speed >= 120)
//     //     {
//     //         //   targetAngle = Mathf.Lerp(90f, 180f, speed);
//     //         //   Debug.Log("Target Angle" + targetAngle);
//     //         Debug.Log("Speed" + speed);
//     //         targetAngle = speed;
//     //     }
//     //     else
//     //     {
//     //         //   targetAngle = Mathf.Lerp(180f, 270f, speed);
//     //         //   Debug.Log("Target Angle" + targetAngle);
//     //         Debug.Log("Speed" + speed);
//     //         targetAngle = speed;
//     //     }
//     //     //float targetAngle = Mathf.Lerp(-180f, 180f, mappedPercentage);  // Interpolate between -180 and 180 degrees

//     //     // Get the current rotation and the target rotation
//     //     Quaternion currentRotation = Needle.transform.localRotation;
//     //     Quaternion targetRotation = Quaternion.Euler(new Vector3(0, targetAngle, 0));

//     //     // Smoothly interpolate the rotation
//     //     Needle.transform.localRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 5f);
//     // }

//     void UpdateSpeedometer(float moveSpeed)
//     {
//         // Ensure moveSpeed is within the range of 0 to 1
//         //  moveSpeed = Mathf.Clamp01(moveSpeed);
//         float targetAngle;
//         // Map moveSpeed to targetAngle within the range of 0 to 160
//         if (moveSpeed >= 0 && moveSpeed <= 0.5f)
//         {
//             targetAngle = 360f * moveSpeed - 180f;
//         }
//         else if (moveSpeed > 0.5f && moveSpeed <= 1)
//         {
//             targetAngle = 360f * moveSpeed + 160f;
//         }
//         else
//         {
//             targetAngle = Needle.transform.localRotation.y;
//         }
//         // Debug.Log to verify values (optional)
//         // Debug.Log("MoveSpeed: " + moveSpeed);
//         // Debug.Log("Target Angle: " + targetAngle);

//         Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, targetAngle, 0f));
//         // float rotationSpeed = 180f;
//         // float maxRotationDegreesPerSecond = rotationSpeed * Time.deltaTime;
//         // Quaternion currentRotation = Needle.transform.localRotation;
//         // Needle.transform.localRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 5f);
//         Needle.transform.localRotation = targetRotation;
//         // Needle.transform.localRotation = Quaternion.RotateTowards(Needle.transform.localRotation, targetRotation, maxRotationDegreesPerSecond);
//     }
//     private float NormalizeAngle(float angle)
//     {
//         while (angle > 360)
//         {
//             angle -= 360;
//         }
//         while (angle < 0)
//         {
//             angle += 360;
//         }
//         return angle;
//     }

// }

// using System;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.XR.Interaction.Toolkit;

// public class SteerRotation : MonoBehaviour
// {
//     private float initialLeftHandRotation;
//     private Quaternion initialWheelRotation;
//     private Quaternion initialVehicleRotation;
//     private InputData _inputData;
//     public GameObject rightHand;
//     public GameObject Needle;
//     public GameObject wheel;
//     private bool rightHandOnWheel = false;
//     public float maxMoveSpeed = 220f;
//     private float currentRotation;
//     public GameObject leftHand;
//     private bool leftHandOnWheel = false;
//     public GameObject Vehicle;
//     private Rigidbody VehicleRigidBody;
//     private float currentSteeringWheelRotation = 0;
//     private float turnDampening = 300;
//     public InputActionReference rightHandGripAction;
//     public InputActionReference leftHandGripAction;
//     public InputActionReference rightHandThumb;
//     public InputActionReference leftHandThumb;

//     private bool leftHandInitialRotationCaptured = false;
//     private bool rotationExceeded = false;
//     private float maxWheelRotationZ = 180f; // Maximum allowed rotation for the wheel in degrees
//     private float maxVehicleRotationY = 180f; // Maximum allowed rotation for the vehicle in degrees

//     float lastControllerRot;

//     void Start()
//     {
//         _inputData = GetComponent<InputData>();
//         VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
//         initialVehicleRotation = Vehicle.transform.localRotation;
//         initialWheelRotation = wheel.transform.localRotation;
//     }

// void Update()
// {
//     float currentLeftHandRotation = leftHand.transform.localRotation.eulerAngles.z;

//     if (!leftHandOnWheel && !rightHandOnWheel)
//     {
//         // Reset wheel and vehicle rotations when not gripped
//         wheel.transform.localRotation = Quaternion.RotateTowards(wheel.transform.localRotation, initialWheelRotation, Time.deltaTime * turnDampening);
//         Vehicle.transform.localRotation = Quaternion.RotateTowards(Vehicle.transform.localRotation, initialVehicleRotation, Time.deltaTime * turnDampening);
//     }

//     if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
//     {
//         // Calculate rotation difference
//         float rotationDifference = Mathf.DeltaAngle(currentLeftHandRotation, initialLeftHandRotation);
//         Debug.Log("currentLeftHandRotation: " + currentLeftHandRotation + " initialLeftHandRotation: " + initialLeftHandRotation + " rotationDifference: " + rotationDifference);

//         if (Math.Abs(rotationDifference) > 0.1f)
//         {
//             if (!rotationExceeded)
//             {
//                 // Apply rotation to wheel and vehicle
//                 ConvertHandRotationToSteeringWheelRotation(rotationDifference);
//                 currentRotation += rotationDifference;
//                 Debug.Log("currentRotation: " + currentRotation);
//             }

//             // Check if the controller begins to rotate in the opposite direction
//             if (rotationDifference * (currentLeftHandRotation - lastControllerRot) < 0)
//             {
//                 rotationExceeded = true;
//                 lastControllerRot = currentLeftHandRotation;
//             }
//             else
//             {
//                 lastControllerRot = currentLeftHandRotation;
//             }

//             // Apply rotation limits
//             if (Math.Abs(currentRotation) > maxWheelRotationZ || Math.Abs(currentRotation) > maxVehicleRotationY)
//             {
//                 rotationExceeded = true;
//                 //currentRotation -= rotationDifference;
//             }
//             else if (Math.Abs(currentLeftHandRotation - lastControllerRot) < currentRotation)
//             {
//                 rotationExceeded = false;
//             }
//         }
//     }
//     else
//     {
//         // Reset flags and values when not gripping
//         leftHandInitialRotationCaptured = false;
//     }

//     if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 joyStickValueR))
//     {
//         UpdateSpeedometer(joyStickValueR.y);
//     }

//     // Update other logic as needed
// }

//     private void ConvertHandRotationToSteeringWheelRotation(float rotationDelta)
//     {
//         Vector3 currentRotation = wheel.transform.localRotation.eulerAngles;
//         wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + rotationDelta);
//         Debug.Log("currentRotation: " + currentRotation.z + " rotationDelta: " + rotationDelta);
//         VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, currentRotation.z + rotationDelta, 0), Time.deltaTime * turnDampening));
//     }

//     private void ReleaseHandsFromWheel()
//     {
//         if (rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() <= 0.1f)
//         {
//             rightHandOnWheel = false;
//         }

//         if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() <= 0.1f)
//         {
//             leftHandOnWheel = false;
//             //leftHandInitialRotationCaptured = false;
//         }
//     }

//     private void OnTriggerStay(Collider other)
//     {
//         if (other.CompareTag("PlayerHandL"))
//         {
//             if (!leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f && !leftHandInitialRotationCaptured)
//             {
//                 leftHandOnWheel = true;
//                 initialLeftHandRotation = leftHand.transform.localRotation.eulerAngles.z;
//                 leftHandInitialRotationCaptured = true;
//                 Debug.Log("Left Hand On Wheel Triggered" + initialLeftHandRotation);
//             }
//             else if (!leftHandOnWheel)
//             {
//                 leftHandInitialRotationCaptured = false;
//             }
//         }
//     }

//     void UpdateSpeedometer(float moveSpeed)
//     {
//         float targetAngle;
//         if (moveSpeed >= 0 && moveSpeed <= 0.5f)
//         {
//             targetAngle = 360f * moveSpeed - 180f;
//         }
//         else if (moveSpeed > 0.5f && moveSpeed <= 1)
//         {
//             targetAngle = 360f * moveSpeed + 160f;
//         }
//         else
//         {
//             targetAngle = Needle.transform.localRotation.y;
//         }

//         Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, targetAngle, 0f));
//         Needle.transform.localRotation = targetRotation;
//     }
// }






using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SteerRotation : MonoBehaviour
{
    private float initialLeftHandRotation;
    private Quaternion initialWheelRotation;
    private Quaternion initialVehicleRotation;
    private InputData _inputData;
    public GameObject rightHand;
    public GameObject Needle;
    public GameObject wheel;
    private bool rightHandOnWheel = false;
    public float maxMoveSpeed = 220f;
    private float currentRotation;
    public GameObject leftHand;
    private bool leftHandOnWheel = false;
    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;
    private float currentSteeringWheelRotation = 0;
    private float turnDampening = 300;
    public InputActionReference rightHandGripAction;
    public InputActionReference leftHandGripAction;
    public InputActionReference rightHandThumb;
    public InputActionReference leftHandThumb;

    private bool leftHandInitialRotationCaptured = false;

    void Start()
    {
        _inputData = GetComponent<InputData>();
        VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
        initialVehicleRotation = Vehicle.transform.localRotation;
        initialWheelRotation = wheel.transform.localRotation;
    }

    void Update()
    {
        float currentLeftHandRotation = leftHand.transform.localRotation.z;
        // if (currentLeftHandRotation < 0)
        // {
        //     currentLeftHandRotation += 360;
        // }

        if (!leftHandOnWheel && !rightHandOnWheel)
        {
            wheel.transform.localRotation = Quaternion.RotateTowards(wheel.transform.localRotation, initialWheelRotation, Time.deltaTime * turnDampening);
            Vehicle.transform.localRotation = Quaternion.RotateTowards(Vehicle.transform.localRotation, initialVehicleRotation, Time.deltaTime * turnDampening);
        }

        if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
        {
            if (!leftHandInitialRotationCaptured)
            {
                initialLeftHandRotation = leftHand.transform.localRotation.z;
                leftHandInitialRotationCaptured = true;
            }
            else
            {
                // float currentLeftHandRotation = leftHand.transform.localRotation.z;
                float rotationDifference = currentLeftHandRotation - initialLeftHandRotation;

                Debug.Log("Rotation Difference" + rotationDifference);

                float angle;
                Vector3 axis;
                //rotationDifference.ToAngleAxis(out angle, out axis);

                if (Math.Abs(rotationDifference) > 0.1f)
                {
                    // if (currentLeftHandRotation > 0)
                    //     currentLeftHandRotation = -currentLeftHandRotation;
                    // else if (rotationDifference < 0)
                    //     currentSteeringWheelRotation = -wheel.transform.localRotation.eulerAngles.z;
                    ConvertHandRotationToSteeringWheelRotation(currentLeftHandRotation );

                }
            }
        }
        else
        {
            leftHandInitialRotationCaptured = false;
        }

        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 joyStickValueR))
        {
            UpdateSpeedometer(joyStickValueR.y);
        }

        ReleaseHandsFromWheel();
    }

    private void ConvertHandRotationToSteeringWheelRotation(float rotationDelta)
    {
        Vector3 currentRotation = wheel.transform.localEulerAngles;
        wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + rotationDelta);

        TurnVehicle(rotationDelta);
    }

    private void ReleaseHandsFromWheel()
    {
        if (rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() <= 0.1f)
        {
            rightHandOnWheel = false;
        }

        if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() <= 0.1f)
        {
            leftHandOnWheel = false;
            leftHandInitialRotationCaptured = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHandL"))
        {
            if (!leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
            {
                leftHandOnWheel = true;
                initialLeftHandRotation = leftHand.transform.localRotation.z;
                if (initialLeftHandRotation < 0)
                {
                    initialLeftHandRotation += 360;
                }
                leftHandInitialRotationCaptured = true;
            }
        }
    }

    void UpdateSpeedometer(float moveSpeed)
    {
        float targetAngle;
        if (moveSpeed >= 0 && moveSpeed <= 0.5f)
        {
            targetAngle = 360f * moveSpeed - 180f;
        }
        else if (moveSpeed > 0.5f && moveSpeed <= 1)
        {
            targetAngle = 360f * moveSpeed + 160f;
        }
        else
        {
            targetAngle = Needle.transform.localRotation.y;
        }

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, targetAngle, 0f));
        Needle.transform.localRotation = targetRotation;
    }

    private void TurnVehicle(float rotationDelta)
    {
        var turn = Vehicle.transform.localEulerAngles.y;
        // if (turn < 0)
        // {
        //     turn = 1 - turn;
        // }
        Debug.Log("Vehicle Rotate: " + (rotationDelta));
        Vehicle.transform.localEulerAngles = new Vector3(Vehicle.transform.localEulerAngles.x, turn + rotationDelta, Vehicle.transform.localEulerAngles.z);
        // VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, Math.Abs(Vehicle.transform.rotation.y + rotationDelta), 0), Time.deltaTime * turnDampening));
    }
}