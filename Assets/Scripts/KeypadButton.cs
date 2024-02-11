using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using TMPro;

public class KeypadButton : MonoBehaviour
{
    Keypad keypad;
    TextMeshProUGUI buttonText;
    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text.Length == 1){
            NameToButtonText();
        }
    }
     public void NameToButtonText()
     {
        buttonText.text = gameObject.name;
     }
}

