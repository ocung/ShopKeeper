using TMPro;
using UnityEngine;

public class ButtonText : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;

    public void SetText(string text)
    {
        // if (buttonText != null)
        // { 
        //     buttonText.text = text;
        // }
        buttonText.text = text;
        
    }
        
}
