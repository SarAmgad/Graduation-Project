using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnding : MonoBehaviour
{
    public GameObject endCanvas;

    public GameObject foundCanvas;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        if(StartingScene.level1){
            count = 6;
        }
        else if(StartingScene.level2){
            count = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FoundObjectDestroy.objectsList.Count >= count && !foundCanvas.activeSelf)
        {
            endCanvas.SetActive(true);
        }
    }
}
