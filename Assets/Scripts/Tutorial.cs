using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class Tutorial : MonoBehaviour
{
    private InputData _inputData;

    public GameObject menu;

    void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool Bbutton) && Bbutton)
        {
            menu.SetActive(true);
        }
    }

    public void StartGame()
    {
        StartingScene.BackToStart();
    }
}
