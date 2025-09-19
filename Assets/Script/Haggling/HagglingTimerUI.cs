using TMPro;
using UnityEngine;

public class HagglingTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    public void SetDisplayText(int time)
    {
        timerText.text = time.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    { 
        gameObject.SetActive(true);
    }
}
