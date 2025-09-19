using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CalculatorController : MonoBehaviour
{
    [SerializeField] private CalculatorButtons calculatorButtons;
    [SerializeField] private CalculatorDisplay calculatorDisplay;
    [SerializeField] private CalculatorUI calculatorUI;

    public event Action OfferingPrice;

    void Start()
    {
        PrepareCalculatorButtons();
        // calculatorUI.HideCalculator();
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         if (calculatorUI.isActiveAndEnabled == false)
    //         {
    //             calculatorDisplay.ClearDisplay();
    //             calculatorUI.ShowCalculator();
    //         }
    //         else
    //         {
    //             calculatorUI.HideCalculator();
    //         }
    //     }
    // }

    private void PrepareCalculatorButtons()
    {
        calculatorButtons.InitializeButtons(HandleInput);
    }

    private void HandleInput(string inputButton)
    {
        if (inputButton == "C")
        {
            calculatorDisplay.ClearDisplay();
            return;
        }
        else if (inputButton == "=")
        {
            Debug.Log("Sendiing offer price");
            OfferingPrice?.Invoke();
            return;
        }
        calculatorDisplay.SetDisplayText(inputButton);
    }

    public string GetCalculatorCurrentValue()
    {
        return calculatorDisplay.GetCurrentInput();
    }

    public void HideCalculator()
    {
        calculatorUI.HideCalculator();
    }
    public void ShowCalculator()
    { 
        calculatorDisplay.ClearDisplay();
        calculatorUI.ShowCalculator();
    }
}
