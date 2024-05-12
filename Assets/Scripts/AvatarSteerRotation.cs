using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AvatarSteerRotation : MonoBehaviour
{
    // Reference to the XR Origin (assuming attached to avatar)
    public XRBaseInteractor leftInteractor; // Optional: Store references to interactors (if using controller proxies)
    public XRBaseInteractor rightInteractor;

    public float maxSteeringAngle = 360f; // Maximum steering angle for the avatar
    public float deadZone = 0.01f; // Threshold for controller movement (adjust as needed)

    private float previousPosition = 0f; // Store previous controller X-axis position

    private void Start()
    {
        // Reset initial X-axis rotation (optional)
        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }
    // private void Update()
    // {
    //     // Get current controller X-axis position
    //     //     float currentPosition = leftInteractor.transform.localPosition.x;

    //     //     // Check if position change exceeds dead zone
    //     //     if (Mathf.Abs(currentPosition - previousPosition) > deadZone && Mathf.Abs(currentPosition - previousPosition) <= 0.3)
    //     //     {
    //     //         // Calculate position value for steering (considering dead zone)
    //     //         //float positionValue =  maxSteeringAngle / (currentPosition - previousPosition);
    //     //         //positionValue = Mathf.Clamp(Mathf.Abs(currentPosition - previousPosition) - deadZone, 0f, maxSteeringAngle);

    //     //         float direction = Mathf.Sign(currentPosition - previousPosition);

    //     //         // Rotate the avatar around Z-axis based on direction
    //     //         float rotationAmount = direction * maxSteeringAngle * Mathf.Abs(currentPosition);
    //     //         transform.Rotate(Vector3.forward, rotationAmount);
    //     //         Debug.Log("Rotation z: " + rotationAmount);

    //     //         // positionValue += currentPosition * maxSteeringAngle;
    //     //         // // Update previous position for next comparison
    //     //         // previousPosition = currentPosition;

    //     //         // // Rotate the avatar around Z-axis based on position value
    //     //         // transform.localRotation = Quaternion.Euler(
    //     //         //     transform.localRotation.eulerAngles.x, // Keep current X-axis rotation
    //     //         //     transform.localRotation.eulerAngles.y, // Keep current Y-axis rotation
    //     //         //     -Mathf.Sign(currentPosition - previousPosition) * positionValue);
    //     //         // Debug.Log("Rotation z: " + positionValue);
    //     //     }

    //     //     // Debug.Log("Controller Position (X-axis): " + currentPosition);
    //     //     // Debug.Log("Rotation Value Steer (Z-axis): " + transform.localRotation.z);

    //     // }
    //     float currentPosition = leftInteractor.transform.localPosition.x;

    //     // Check if position change exceeds dead zone
    //     if (Mathf.Abs(currentPosition - previousPosition) > deadZone)
    //     {
    //         // Calculate position change direction
    //         float direction = Mathf.Sign(currentPosition - previousPosition);

    //         // Calculate the rotation amount based on direction
    //         float rotationAmount = direction * maxSteeringAngle;

    //         // Calculate the current rotation around the Z-axis
    //         float currentRotation = transform.localRotation.eulerAngles.z;

    //         // Calculate the target rotation (rotation when the desired angle is reached)
    //         float targetRotation = Mathf.Clamp(currentRotation + rotationAmount, 0, maxSteeringAngle);

    //         // If the target rotation exceeds the maximum allowed, limit it to the maximum angle
    //         if (Mathf.Abs(targetRotation) > maxSteeringAngle)
    //         {
    //             targetRotation = Mathf.Sign(targetRotation) * maxSteeringAngle;
    //         }

    //         // Rotate the avatar around Z-axis to the target rotation
    //         transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, targetRotation );
    //         Debug.Log("Rotation z: " + targetRotation);
    //         Debug.Log("Rotation xy: " + (currentPosition - previousPosition));

    //         // Update previous position for next comparison
    //         previousPosition = currentPosition;
    //     }
    // }

    private void Update()
{
    // Get current controller X-axis position
    float currentPosition = leftInteractor.transform.localPosition.x;

    // Check if position change exceeds dead zone
    if (Mathf.Abs(currentPosition - previousPosition) > deadZone)
    {
        // Calculate position change direction
        float direction = Mathf.Sign(currentPosition - previousPosition);

        // Calculate the rotation amount based on direction
        float rotationAmount = direction * maxSteeringAngle;

        // Calculate the target rotation by adding the rotation amount to the current rotation
        float targetRotation = transform.localRotation.eulerAngles.z + rotationAmount;

        // Rotate the avatar around Z-axis to the target rotation
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, targetRotation);

        // Update previous position for next comparison
        previousPosition = currentPosition;
    }
}

}

