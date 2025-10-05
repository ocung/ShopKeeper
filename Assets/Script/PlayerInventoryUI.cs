using TMPro;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ValueText;

    public void SetValueText(int currentValue, int maxValue)
    {
        ValueText.text = $"{currentValue} / {maxValue}";
    }
}
