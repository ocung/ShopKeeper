using System.Globalization;
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
            UpdateFormattedDisplay();
            //displayText.text = currentInput;
        }

    }

     private void UpdateFormattedDisplay()
    {
        if (long.TryParse(currentInput, out long rawNumber))
        {
            // format Indonesia ribuan: 1.200.000
            string formatted = rawNumber.ToString("N0", new CultureInfo("id-ID"));
            displayText.text = formatted;
        }
        else
        {
            // fallback (harusnya sih gak terjadi)
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
