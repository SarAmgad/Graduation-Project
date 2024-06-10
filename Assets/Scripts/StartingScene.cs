using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    public static bool level1 = false;
    public static bool level2 = false;
    public void StartLevel1()
    {
        SceneManager.LoadScene(1);
        level2 = false;
        level1 = true;
    }
    
    public void StartLevel2()
    {
        SceneManager.LoadScene(2);
        FoundObjectDestroy.objectsList = new List<GameObject>();
        level1 = false;
        level2 = true;
    }


    public void BackToStart()
    {
        SceneManager.LoadScene(0);
        level1 = false;
        level2 = false;
    }

    public void Exit()
    {
            Application.Quit();
    }
}
