// using System;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.XR.Interaction.Toolkit;

// public class SteerRotation : MonoBehaviour
// {
//     private float initialLeftHandRotation;
//     private float currentRightHandRotation;
//     private bool leftHandInitialRotationCaptured = false;
//     private float initialRightHandRotation;
//     private bool rightHandInitialRotationCaptured = false;
//     private Quaternion initialWheelRotation;
//     private Quaternion initialVehicleRotation;
//     private InputData _inputData;
//     public GameObject rightHand;
//     public GameObject Needle;
//     public GameObject wheel;
//     private bool rightHandOnWheel = false;
//     public float maxMoveSpeed = 220f;
//     public float moveSpeed = 20f;
//     public GameObject leftHand;
//     private bool leftHandOnWheel = false;
//     public GameObject Vehicle;
//     private Rigidbody VehicleRigidBody;
//     public float turnDampening = 60;
//     public InputActionReference rightHandGripAction;
//     public InputActionReference leftHandGripAction;
//     float currentLeftHandRotation;

//     void Start()
//     {
//         _inputData = GetComponent<InputData>();
//         VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
//         initialVehicleRotation = Vehicle.transform.localRotation;
//         initialWheelRotation = wheel.transform.localRotation;

//     }

//     void Update()
//     {
//         currentLeftHandRotation = leftHand.transform.localEulerAngles.z;
//         currentRightHandRotation = rightHand.transform.localEulerAngles.z;
//         CheckLeftHandOnWheel();
//         CheckRightHandOnWheel();
//         if (!leftHandOnWheel && !rightHandOnWheel)
//         {
//             wheel.transform.localRotation = Quaternion.RotateTowards(wheel.transform.localRotation, initialWheelRotation, Time.deltaTime * turnDampening);
//             Vehicle.transform.localRotation = Quaternion.RotateTowards(Vehicle.transform.localRotation, initialVehicleRotation, Time.deltaTime * turnDampening);
//         }
//         ReleaseHandsFromWheel();
//     }

//     // private void CheckLeftHandOnWheel()
//     // {
//     //     Vector2 joyStickValueL = new Vector2(0, 0);
//     //     if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
//     //     {
//     //         if (!leftHandInitialRotationCaptured)
//     //         {
//     //             initialLeftHandRotation = leftHand.transform.localRotation.z;
//     //             leftHandInitialRotationCaptured = true;
//     //         }
//     //         else
//     //         {
//     //             float rotationDifference = (currentLeftHandRotation - initialLeftHandRotation) - 360;
//     //             Debug.Log("currentLeftHandRotation" + currentLeftHandRotation + " initialLeftHandRotation  " + initialLeftHandRotation + " rotationDifference  " + rotationDifference);
//     //             Debug.Log("rotationdifference " + Math.Abs(rotationDifference % 360));
//     //             if ((Math.Abs(rotationDifference % 360) > 20) && (Math.Abs(rotationDifference % 360) < 340))
//     //             {
//     //                 if (rotationDifference < 0)
//     //                 {
//     //                     ConvertHandRotationToSteeringWheelRotation(-rotationDifference);
//     //                     TurnVehicle(-rotationDifference + 360);
//     //                 }
//     //                 else
//     //                 {
//     //                     ConvertHandRotationToSteeringWheelRotation(rotationDifference);
//     //                     TurnVehicle(rotationDifference);
//     //                 }
//     //             }
//     //             // if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joyStickValueL))
//     //             // {
//     //             //     UpdateSpeedometer(joyStickValueL.y);
//     //             // }

//     //         }
//     //         // Vector3 forwardMovement = joyStickValueL.y * Vehicle.transform.forward;
//     //         // VehicleRigidBody.velocity = forwardMovement * moveSpeed;
//     //     }
//     //     else
//     //     {
//     //         leftHandInitialRotationCaptured = false;
//     //     }
//     // }
//     private void CheckLeftHandOnWheel()
//     {
//         Vector2 joyStickValueL = new Vector2(0, 0);
//         if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
//         {
//             if (!leftHandInitialRotationCaptured)
//             {
//                 initialLeftHandRotation = leftHand.transform.localRotation.z;
//                 leftHandInitialRotationCaptured = true;
//             }
//             else
//             {
//                 float rotationDifference = (currentLeftHandRotation - initialLeftHandRotation);
//                 Debug.Log("currentLeftHandRotation" + currentLeftHandRotation + " initialLeftHandRotation  " + initialLeftHandRotation + " rotationDifference  " + rotationDifference);
//                 Debug.Log("rotationdifference " + Math.Abs(rotationDifference));

//                 if (Math.Abs(rotationDifference) > 0.5f)
//                 {
//                     if (rotationDifference < 0)
//                     {
//                         rotationDifference = Math.Abs(rotationDifference);
//                         ConvertHandRotationToSteeringWheelRotation(rotationDifference);
//                         TurnVehicle(rotationDifference);
//                     }
//                     else
//                     {
//                         ConvertHandRotationToSteeringWheelRotation(-rotationDifference);
//                         TurnVehicle(-rotationDifference);
//                     }
//                 }
//                 // if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joyStickValueL))
//                 // {
//                 //     UpdateSpeedometer(joyStickValueL.y);
//                 // }

//             }
//             // Vector3 forwardMovement = joyStickValueL.y * Vehicle.transform.forward;
//             // VehicleRigidBody.velocity = forwardMovement * moveSpeed;
//         }
//         else
//         {
//             leftHandInitialRotationCaptured = false;
//         }
//     }
//     private void CheckRightHandOnWheel()
//     {
//         Vector2 joyStickValueR = new Vector2(0, 0);
//         float speed;
//         if (rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() > 0.1f)
//         {
//             if (!rightHandInitialRotationCaptured)
//             {
//                 initialRightHandRotation = rightHand.transform.localRotation.z;
//                 rightHandInitialRotationCaptured = true;
//             }
//             else
//             {
//                 float rotationDifference = (currentRightHandRotation - initialRightHandRotation);
//                 if (Math.Abs(rotationDifference) > 0.5f)
//                 {
//                     if (rotationDifference < 0)
//                     {
//                         rotationDifference = Math.Abs(rotationDifference);
//                         // ConvertHandRotationToSteeringWheelRotation(-rotationDifference);
//                         // TurnVehicle(rotationDifference);
//                     }
//                     Debug.Log("currentRightHandRotation" + currentRightHandRotation + " initialRightHandRotation  " + initialRightHandRotation + " rotationDifference  " + rotationDifference);
//                     Debug.Log("rotationdifference   " + Math.Abs(rotationDifference));
//                     ConvertHandRotationToSteeringWheelRotation(-rotationDifference);
//                     TurnVehicle(-rotationDifference);
//                 }
//                 // if (rotationDifference < 0)
//                 // {
//                 //     Math.Abs(rotationDifference);
//                 // }
//                 // Debug.Log("currentRightHandRotation" + currentRightHandRotation + " initialRightHandRotation  " + initialRightHandRotation + " rotationDifference  " + rotationDifference);
//                 // Debug.Log("rotationdifference   " + Math.Abs(rotationDifference));
//                 // ConvertHandRotationToSteeringWheelRotation(-rotationDifference);
//                 // TurnVehicle(-rotationDifference);
//             }
//             if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joyStickValueR))
//             {
//                 UpdateSpeedometer(joyStickValueR.y);
//                 Vector3 forwardMovement = joyStickValueR.y * (Vehicle.transform.forward);
//                 VehicleRigidBody.MovePosition(VehicleRigidBody.position + forwardMovement);
//                 // 
//                 // VehicleRigidBody.velocity = new Vector3(0,0,forwardMovement.z * moveSpeed);
//                 // Debug.Log("Velcity "+VehicleRigidBody.velocity);
//             }


//         }
//         else
//         {
//             rightHandInitialRotationCaptured = false;
//         }

//     }

//     private void ConvertHandRotationToSteeringWheelRotation(float rotationDelta)
//     {
//         Vector3 currentRotation = wheel.transform.localEulerAngles;
//         float targetRotationZ = currentRotation.z + rotationDelta;

//         if (wheel.transform.localEulerAngles.z >= 270)
//         {
//             targetRotationZ = 270 + rotationDelta;
//             wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, 270);
//         }
//         else if (wheel.transform.localEulerAngles.z <= 90)
//         {
//             targetRotationZ = 90 + rotationDelta;
//             wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, 90);
//         }
//         Quaternion targetRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, targetRotationZ);
//         wheel.transform.localRotation = Quaternion.RotateTowards(wheel.transform.localRotation, targetRotation, turnDampening * 0.7f * Time.deltaTime);

//         Debug.Log("Rotateeeeee: " + rotationDelta);
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
//             leftHandInitialRotationCaptured = false;
//         }
//     }

//     private void OnTriggerStay(Collider other)
//     {
//         if (other.CompareTag("PlayerHandL"))
//         {
//             if (!leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
//             {
//                 leftHandOnWheel = true;
//                 initialLeftHandRotation = leftHand.transform.localEulerAngles.z;
//                 leftHandInitialRotationCaptured = true;
//             }
//         }
//         if (other.CompareTag("PlayerHandR"))
//         {
//             if (!rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() > 0.1f)
//             {
//                 rightHandOnWheel = true;
//                 initialRightHandRotation = rightHand.transform.localEulerAngles.z;
//                 rightHandInitialRotationCaptured = true;
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

//     private void TurnVehicle(float rotationDelta)
//     {
//         var turn = Vehicle.transform.localEulerAngles.y;
//         if (Vehicle.transform.localEulerAngles.y > 90 && Vehicle.transform.localEulerAngles.y < 180)
//         {
//             turn = 90;
//         }
//         else if (Vehicle.transform.localEulerAngles.y < 270 && Vehicle.transform.localEulerAngles.y > 180)
//         {
//             turn = -90;
//         }
//         Debug.Log("Vehicle Rotate: " + (rotationDelta));
//         VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, turn + rotationDelta, 0), Time.deltaTime * turnDampening));
//     }
// }



using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit;

public class SteerRotation : MonoBehaviour
{
    private float initialLeftHandRotation;
    private bool leftHandInitialRotationCaptured = false;
    private float initialRightHandRotation;
    private bool rightHandInitialRotationCaptured = false;
    private Quaternion initialWheelRotation;
    private Quaternion initialVehicleRotation;
    private InputData _inputData;
    public GameObject rightHand;
    public GameObject Needle;
    public GameObject wheel;
    private bool rightHandOnWheel = false;
    public float maxMoveSpeed = 220f;
    public float moveSpeed = 100;
    public GameObject leftHand;
    private bool leftHandOnWheel = false;
    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;
    public float turnDampening = 60;
    public InputActionReference rightHandGripAction;
    public InputActionReference leftHandGripAction;
    float currentLeftHandRotation;
    Vector3 forwardMovement;
    public Movement movement;
    Vector2 joyStickValueR = new Vector2(0, 0);


    void Start()
    {
        _inputData = GetComponent<InputData>();
        VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
        initialVehicleRotation = Vehicle.transform.localRotation;
        initialWheelRotation = wheel.transform.localRotation;
        //forwardMovement = moveSpeed * Time.deltaTime * Vehicle.transform.forward;

    }

    void Update()
    {
        currentLeftHandRotation = leftHand.transform.localEulerAngles.z;
        CheckLeftHandOnWheel();
        CheckRightHandOnWheel();
        VehicleRigidBody.velocity = forwardMovement;
        if(movement.isEdgeDetected){
            moveSpeed = 0;
        }
        // if (!leftHandOnWheel && !rightHandOnWheel)
        // {
        //     wheel.transform.localRotation = Quaternion.RotateTowards(wheel.transform.localRotation, initialWheelRotation, Time.deltaTime * turnDampening);
        //     Vehicle.transform.localRotation = Quaternion.RotateTowards(Vehicle.transform.localRotation, initialVehicleRotation, Time.deltaTime * turnDampening);
        // }

        // if (!movement.isEdgeDetected && joyStickValueR.y > 0)
        // {
        //     VehicleRigidBody.velocity = moveSpeed * Time.deltaTime * Vehicle.transform.forward;
        // }


        //        Debug.Log("VehicleMovementtt " + VehicleRigidBody.velocity);
        ReleaseHandsFromWheel();


    }

    private void CheckLeftHandOnWheel()
    {

        Vector2 joyStickValueL = new Vector2(0, 0);
        if (leftHandOnWheel && leftHandGripAction.action.ReadValue<float>() > 0.1f)
        {
            if (!leftHandInitialRotationCaptured)
            {
                initialLeftHandRotation = leftHand.transform.localRotation.z;
                leftHandInitialRotationCaptured = true;
            }
            else
            {
                float rotationDifference = (currentLeftHandRotation - initialLeftHandRotation) - 360;
                Debug.Log("currentLeftHandRotation" + currentLeftHandRotation + " initialLeftHandRotation  " + initialLeftHandRotation + " rotationDifference  " + rotationDifference);
                Debug.Log("rotationdifference " + Math.Abs(rotationDifference % 360));
                if ((Math.Abs(rotationDifference % 360) > 10) && (Math.Abs(rotationDifference % 360) < 350))
                {
                    if (rotationDifference < 0)
                    {
                        ConvertHandRotationToSteeringWheelRotation(-rotationDifference + 360);
                        TurnVehicle(-rotationDifference + 360);
                    }
                    else
                    {
                        ConvertHandRotationToSteeringWheelRotation(rotationDifference);
                        TurnVehicle(rotationDifference);
                    }
                }
                // if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joyStickValueL))
                // {
                //     UpdateSpeedometer(joyStickValueL.y);
                // }

            }
            // Vector3 forwardMovement = joyStickValueL.y * Vehicle.transform.forward;
            // VehicleRigidBody.velocity = forwardMovement * moveSpeed;
        }
        else
        {
            leftHandInitialRotationCaptured = false;
        }
    }

    private void CheckRightHandOnWheel()
    {

        float speed;
        // if (rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() > 0.1f)
        // {
        // if (!rightHandInitialRotationCaptured)
        // {
        //     initialRightHandRotation = rightHand.transform.localRotation.z;
        //     rightHandInitialRotationCaptured = true;
        // }
        // else
        // {
        //     float rotationDifference = (rightHand.transform.localRotation.z - initialRightHandRotation) - 360;
        //     Debug.Log("currentRightHandRotation" + rightHand.transform.localRotation.z + " initialRightHandRotation  " + initialRightHandRotation + " rotationDifference  " + rotationDifference);
        //     Debug.Log("rotationdifference " + Math.Abs(rotationDifference % 360));
        //     if ((Math.Abs(rotationDifference % 360) > 20) && (Math.Abs(rotationDifference % 360) < 340))
        //     {
        //         if (rotationDifference > 0)
        //         {
        //             ConvertHandRotationToSteeringWheelRotation(-rotationDifference + 360);
        //             TurnVehicle(-rotationDifference + 360);
        //         }
        //         else
        //         {
        //             ConvertHandRotationToSteeringWheelRotation(rotationDifference);
        //             TurnVehicle(rotationDifference);
        //         }
        //     }
        // }
        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joyStickValueR))
        {
            UpdateSpeedometer(joyStickValueR.y);
            forwardMovement = 10 * joyStickValueR.y * moveSpeed * Time.deltaTime * Vehicle.transform.forward;
            if (joyStickValueR.y < 0)
            {
                //VehicleRigidBody.velocity = Vector3.zero;
                joyStickValueR = -joyStickValueR;
                moveSpeed = 10;
                //movement.isAccelerating = false;
                movement.isDecelerating = true;
                

            }
            else if(joyStickValueR.y == 0){
                moveSpeed = 0;
                movement.isDecelerating = true;
            }
            else
            {
                moveSpeed = 60;
                VehicleRigidBody.velocity = forwardMovement;
                movement.isAccelerating = true;
                //movement.isDecelerating = false;

            }
            //VehicleRigidBody.velocity = forwardMovement * 2;
            // 
            // VehicleRigidBody.velocity = new Vector3(0,0,forwardMovement.z * moveSpeed);
            // Debug.Log("Velcity "+VehicleRigidBody.velocity);
        }


        // }
        // else
        // {
        //     rightHandInitialRotationCaptured = false;
        // }

    }

    private void ConvertHandRotationToSteeringWheelRotation(float rotationDelta)
    {
        Vector3 currentRotation = wheel.transform.localEulerAngles;
        float targetRotationZ = currentRotation.z + rotationDelta;

        if (wheel.transform.localEulerAngles.z >= 270)
        {
            targetRotationZ = 270 + rotationDelta;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, 270);
        }
        else if (wheel.transform.localEulerAngles.z <= 90)
        {
            targetRotationZ = 90 + rotationDelta;
            wheel.transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, 90);
        }
        Quaternion targetRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, targetRotationZ);
        wheel.transform.localRotation = Quaternion.RotateTowards(wheel.transform.localRotation, targetRotation, turnDampening * 0.7f * Time.deltaTime);

        Debug.Log("Rotateeeeee: " + rotationDelta);
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
                initialLeftHandRotation = leftHand.transform.localEulerAngles.z;
                leftHandInitialRotationCaptured = true;
            }
        }
        if (other.CompareTag("PlayerHandR"))
        {
            if (!rightHandOnWheel && rightHandGripAction.action.ReadValue<float>() > 0.1f)
            {
                rightHandOnWheel = true;
                initialRightHandRotation = rightHand.transform.localEulerAngles.z;
                rightHandInitialRotationCaptured = true;
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
        if (Vehicle.transform.localEulerAngles.y > 90 && Vehicle.transform.localEulerAngles.y < 180)
        {
            turn = 90;
        }
        else if (Vehicle.transform.localEulerAngles.y < 270 && Vehicle.transform.localEulerAngles.y > 180)
        {
            turn = -90;
        }
        Debug.Log("Vehicle Rotate: " + rotationDelta);
        VehicleRigidBody.MoveRotation(Quaternion.RotateTowards(Vehicle.transform.rotation, Quaternion.Euler(0, Math.Abs(turn + rotationDelta), 0), Time.deltaTime * turnDampening));
    }
}