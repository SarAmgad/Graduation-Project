using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundObjectDestroy : MonoBehaviour
{
    public Transform Rcontroller; 
    public Transform Lcontroller;
    private bool positionMatched = false;
    private float destructionDelay = 2f;
    private float timer = 0f;
    public GameObject FoundPanel;
    
    public float positionTolerance = 0.1f;
    public static List<GameObject> objectsList = new List<GameObject>();

    public GameObject tick;


    // Update is called once per frame
    private void Update()
    {
        
        if (positionMatched)
        {
            FoundPanel.SetActive(true);
            Debug.Log("Found.");
            
            if (StartingScene.level1)
            {
                Found();
            }
            
            if (!objectsList.Contains(gameObject))
            {
                objectsList.Add(gameObject);
            }
            
            timer += Time.deltaTime;
            if (timer >= destructionDelay)
            {
                Destroy(gameObject);
                FoundPanel.SetActive(false);
                Debug.Log("Destroyed.");
            }
        }
    }

    private void FixedUpdate()
    {
        // Check if the positions match
        if (ComparePositions(transform.position, Rcontroller.position, positionTolerance) || ComparePositions(transform.position, Lcontroller.position, positionTolerance) )
        {
            positionMatched = true;
        }
        
        
    }
    
    bool ComparePositions(Vector3 pos1, Vector3 pos2, float tolerance)
    {
        // Compare each component of the vectors with the specified tolerance
        return Mathf.Abs(pos1.x - pos2.x) <= tolerance &&
               Mathf.Abs(pos1.y - pos2.y) <= tolerance &&
               Mathf.Abs(pos1.z - pos2.z) <= tolerance;
    }

    public void Found()
    {
        tick.SetActive(true);
    }
}
