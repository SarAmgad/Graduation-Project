using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnding : MonoBehaviour
{
    public GameObject endCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FoundObjectDestroy.objectsList.Count >= 5)
        {
            endCanvas.SetActive(true);
        }
    }
}
