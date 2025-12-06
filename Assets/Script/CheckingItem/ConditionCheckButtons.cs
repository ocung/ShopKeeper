using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionCheckButtons : MonoBehaviour
{

    [SerializeField] private Sprite checkIcon; //test purpose
    [SerializeField] private ButtonClicked buttonItemPrefab;
    [SerializeField] private RectTransform CheckCondition;

    [SerializeField] private List<string> buttonValue = new List<string>(); //set buttonValue di Inspector
    List<ButtonClicked> buttonList = new List<ButtonClicked>();

    public void InitializeButtons(Action<string> onInput)
    {
        for (int i = 0; i < buttonValue.Count; i++)
        {
            ButtonClicked button = Instantiate(buttonItemPrefab, Vector3.zero, Quaternion.identity);
            button.transform.SetParent(CheckCondition);
            buttonList.Add(button);

            int index = i;

            ButtonText Number = button.GetComponentInChildren<ButtonText>();
            //Number.SetText(buttonValue[index]);

            Image image = button.GetComponent<Image>();
            image.sprite = checkIcon;

            button.OnRightButtonClicked += () => onInput(buttonValue[index]);
            button.OnLeftButtonClicked += () => onInput(buttonValue[index]);
        }
    }

    private void HandleCheckButton()
    {
        Debug.Log("Check button clicked!");
    }

}
