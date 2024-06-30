using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    public static bool level1 = false;
    public static bool level2 = false;
    public void StartLevel1()
    {
        SceneManager.LoadScene(1);
        level1 = true;
    }
    
    public void StartLevel2()
    {
        SceneManager.LoadScene(2);
        level2 = true;
    }


    public void BackToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
            Application.Quit();
    }
}
