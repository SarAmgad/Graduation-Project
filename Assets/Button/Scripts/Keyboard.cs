using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public TMP_InputField inputField;
    // public GameObject normalButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InsertChar(string c)
    {
        inputField.text += c;
    }

    public void DeleteChar()
    {
        if (inputField.text.Length > 0){
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void ChosenNumber()
    {
        Debug.Log("Chosennnn");
    }

       
}
