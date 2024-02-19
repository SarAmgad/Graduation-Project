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

    public static List<GameObject> objectsList = new List<GameObject>();


    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Not Found.");
        if (positionMatched)
        {
            FoundPanel.SetActive(true);
            Debug.Log("Found.");
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
        if (transform.position == Rcontroller.position || transform.position == Lcontroller.position )
        {
            positionMatched = true;
        }
    }
}
