using System;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorButtons : MonoBehaviour
{
    [SerializeField] private ButtonClicked buttonItemPrefab;
    [SerializeField] private ButtonText buttonItemTextPrefab; 
    [SerializeField] private RectTransform buttonPaanel;
    [SerializeField] private List<string> buttonValue = new List<string>(); //set buttonValue di Inspector
    List<ButtonClicked> buttonList = new List<ButtonClicked>();

    public void InitializeButtons(Action<string> onInput)
    {
        for (int i = 0; i < buttonValue.Count; i++)
        {
            ButtonClicked button = Instantiate(buttonItemPrefab, Vector3.zero, Quaternion.identity);
            button.transform.SetParent(buttonPaanel);
            buttonList.Add(button);

            int index = i;

            //buttonItemTextPrefab.SetText(buttonValue[index]);
            ButtonText Number = button.GetComponentInChildren<ButtonText>();
            Number.SetText(buttonValue[index]);
            // if (Number != null)
            // {
            //     Number.SetText(buttonValue[index]);
            // }

            button.OnRightButtonClicked += () => onInput(buttonValue[index]);
            button.OnLeftButtonClicked += () => onInput(buttonValue[index]);
        }
    }

    // private void HandleButtonLeftClicked(int index)
    // {
    //     string value = buttonValue[index];
    //     Debug.Log("calculator touch" + value);
        
    // }

    // private void HandleButtonRightClicked(int index)
    // {
    //     string value = buttonValue[index];
    //     Debug.Log("calculator touch" + value);
    // }
}
