using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClicked : MonoBehaviour, IPointerClickHandler
{
    public event Action OnRightButtonClicked, OnLeftButtonClicked;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Button right clicked!");
            OnRightButtonClicked?.Invoke();
        }
        else
        {
            Debug.Log("Button left clicked!");
            OnLeftButtonClicked?.Invoke();
        }
    }
}
