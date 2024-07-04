using UnityEngine;
using UnityEngine.InputSystem.XR;

public class List : MonoBehaviour
{
//_____________ Inspector Vars _____________
    [SerializeField] private GameObject preview;

//_____________ Private Vars _____________
    private TrackedPoseDriver trackedPoseDriver;

    private void Awake()
    {
        trackedPoseDriver = gameObject.transform.parent.GetComponent<TrackedPoseDriver>();
    }

    public void ListMenu()
    {
        trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
    }

    public void Preview(GameObject gameObject)
    {
        if (preview.transform.childCount > 0)
        {
            foreach (Transform child in preview.GetComponentInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
        Instantiate(gameObject, preview.transform.position, gameObject.transform.rotation, preview.transform);
    }
}
