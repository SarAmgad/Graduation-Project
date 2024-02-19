using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace UIListPackage
{
    public class List : MonoBehaviour
    {
        public GameObject preview;
        private float timer = 0f;
    
        private TrackedPoseDriver trackedPoseDriver;
        private bool doneButton = false;

        private void Awake()
        {
            trackedPoseDriver = gameObject.transform.parent.GetComponent<TrackedPoseDriver>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > 2 && !doneButton)
            {
                trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
            }
        }

        public void ListMenu()
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
            gameObject.SetActive(false);
            doneButton = true;
        }

        public void Preview(GameObject gameObject)
        {
            if (preview.transform.childCount > 0)
            {
                Destroy(preview.transform.GetChild(0).gameObject);
            }
            // Instantiate(gameObject, preview.transform);
            Instantiate(gameObject, preview.transform.position, gameObject.transform.rotation, preview.transform);
        }

    
    }
}
