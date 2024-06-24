using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.UI;


public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;
    public TMP_InputField nameField;
    public TMP_InputField emailField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenKeyboard(){
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
    }

    public void SaveEmail()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("name", nameField.text);
        PlayerPrefs.SetString("email", emailField.text);
    }
}
