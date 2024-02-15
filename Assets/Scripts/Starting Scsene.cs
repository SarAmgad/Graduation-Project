using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScsene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
