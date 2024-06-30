using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.UI;


public class ShowKeyboard : MonoBehaviour
{
    // private TMP_InputField inputField;
    public TMP_InputField emailField;
    public TMP_InputField nameField;
    public GameObject registerCanvas, instructionsCanvs, keyboard;
    void Start()
    {
        // inputField = GetComponent<TMP_InputField>();
        // inputField.onSelect.AddListener(x => OpenKeyboard());
        nameField.onSelect.AddListener(delegate {OpenKeyboard(nameField);});
        emailField.onSelect.AddListener(delegate {OpenKeyboard(emailField);});
    }

    public void OpenKeyboard(TMP_InputField inputField){
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
    }

    public void SaveEmail()
    {
        Debug.Log(emailField.text);
        if (emailField.text != "")
        {
            PlayerPrefs.DeleteKey("name");
            PlayerPrefs.DeleteKey("email");
            PlayerPrefs.SetString("name", nameField.text);
            PlayerPrefs.SetString("email", emailField.text);
            Debug.Log(emailField.text);
            registerCanvas.SetActive(false);
            instructionsCanvs.SetActive(true);
            keyboard.SetActive(false);
        }
    }
}
