using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class CalculatorDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;

    private string currentInput = "0";

    public void SetDisplayText(string inputButton)
    {
        if (currentInput.Length < 8)
        {
            if (currentInput.Length == 1 && currentInput == "0" )
            { 
                currentInput = ""; 
            }
            
            currentInput += inputButton;
            displayText.text = currentInput;
        }

    }
    public void ClearDisplay()
    {
        currentInput = "0";
        displayText.text = currentInput;
    }

    public string GetCurrentInput()
    {
        return currentInput;
    }

}
