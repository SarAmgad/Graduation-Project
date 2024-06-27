using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.UI;


public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;
    public TMP_InputField nameField;
    public TMP_InputField emailField;
    public GameObject registerCanvas, instructionsCanvs, keyboard;
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    public void OpenKeyboard(){
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
    }

    public void SaveEmail()
    {
        if (nameField.text != "" || emailField.text != "")
        {
            registerCanvas.SetActive(false);
            instructionsCanvs.SetActive(true);
            keyboard.SetActive(false);
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("name", nameField.text);
            PlayerPrefs.SetString("email", emailField.text);
        }
    }
}
