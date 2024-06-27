using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoundObjectDestroy : MonoBehaviour
{

//_____________ Inspector Vars _____________
    [SerializeField] private List<GameObject> tick;

//_____________ Private Vars _____________
    private GameObject FoundPanel;
    public static List<GameObject> objectsList = new List<GameObject>();
    XRGrabInteractable grabbedObject;
    private List<string> objects = new List<string>() {"Statue", "Statue2", "Book", "Cup", "Bottle", "Lens", "Headphones", "Hammer", "Alarm", "Trophy", "Can"};
    private void Start()
    {
        // FoundPanel = GetCanvas(FoundPanel, "XR Origin (XR Rig)/Camera Offset/Main Camera/Found Canvas");
        FoundPanel = GetCanvas(FoundPanel, "XR Origin Hands/Camera Offset/Main Camera/Found Canvas");
        objectsList = new List<GameObject>();
    }

    private void Update()
    {
        grabbedObject = Grabbing.grabbedObject;
        if (grabbedObject != null && objects.Contains(grabbedObject.tag))
        {
            FoundPanel.SetActive(true);
            
            if (StartingScene.level1)
            {
                Found();
            }
            
            if (!objectsList.Contains(grabbedObject.gameObject))
            {
                objectsList.Add(grabbedObject.gameObject);
            }
            
            StartCoroutine(Delay(1f));
        }
    }

    private GameObject GetCanvas(GameObject canvas, string name)
    {
        if (canvas == null)
        {
            return GameObject.Find(name);
        }
        return canvas;
    }

    public void Found()
    {
        // tick.SetActive(true);
        foreach (GameObject t in tick)
        {
            if (t.tag == grabbedObject.tag)
            {
                t.SetActive(true);
            }
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FoundPanel.SetActive(false);
        if(grabbedObject)
        {
            Destroy(grabbedObject.gameObject);
        }
    }
}
