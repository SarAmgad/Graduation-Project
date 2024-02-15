using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class List : MonoBehaviour
{
    // public GameObject[] listofObjs;
    // public GameObject[] listofTicks;
    public GameObject preview;
    // public GameObject list;
    private bool found = false; 
    private float timer = 0f;
    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 20)
        {
            gameObject.SetActive(false);
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

    
}
