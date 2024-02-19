using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class List : MonoBehaviour
{
    // public GameObject[] listofObjs;
    public GameObject[] listofTicks;
    public GameObject preview;
    private bool found = false; 
    private void Start()
    {
    }

    private void Update()
    {
        if (found)
        {
        }
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

    public void Found(GameObject gameObject)
    {
        foreach (var obj in listofTicks)
        {
            if (obj.gameObject.CompareTag(gameObject.tag))
            {
                obj.SetActive(true);
            }
        }
    }
}
