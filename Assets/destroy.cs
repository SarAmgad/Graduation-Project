using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public Transform Rcontroller; 
    public Transform Lcontroller;
    private bool positionMatched = false;
    private float destructionDelay = 4f;
    private float timer = 0f;
    public GameObject FoundPanel;

    void Update()
    {
        if (positionMatched)

        {
            FoundPanel.SetActive(true);
            timer += Time.deltaTime;
            if (timer >= destructionDelay)
            {
                Destroy(gameObject);
                FoundPanel.SetActive(false);

            }
        }
    }

    void FixedUpdate()
    {
        // Check if the positions match
        if (transform.position == Rcontroller.position || transform.position == Lcontroller.position )
        {
            positionMatched = true;
        }
    }
}
