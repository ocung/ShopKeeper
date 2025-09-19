using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    private bool isMonthSelected = false;
    public void StartGame()
    {
        if (!isMonthSelected) return;
        SceneManager.LoadScene(1);
    }

    public void SetMonthFebruary()
    {
        isMonthSelected = true;
        Debug.Log("Its February");
    }

    public void SetMonthAugust()
    { 
        isMonthSelected = true;
        Debug.Log("Its August");
    }
}
