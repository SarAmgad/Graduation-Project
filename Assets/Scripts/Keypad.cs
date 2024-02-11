using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TMP_InputField inputField;
    private Numbers numbers;

    void Start()
    {
        numbers = FindObjectOfType<Numbers>();
        if (numbers == null)
        {
            Debug.LogError("Numbers component not found in the scene!");
        }
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

    public void CheckNumber()
    {
        int.TryParse(inputField.text, out int input);
        Debug.Log($"Number Checked = {input}");

        numbers.CheckMissingNumber(input);
        inputField.text = "";

    }

}
