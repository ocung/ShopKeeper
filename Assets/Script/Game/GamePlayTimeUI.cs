using UnityEngine;
using UnityEngine.UI;

public class GamePlayTimeUI : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public void SetMaxPlayTime(int time)
    {
        slider.maxValue = time;
        slider.value = time;
    }

    public void SetPlayTime(int time)
    {
        slider.value = time;
    }

    public void PrintValue()
    {
        Debug.Log("Current Play Time: " + slider.value);
    }

}
