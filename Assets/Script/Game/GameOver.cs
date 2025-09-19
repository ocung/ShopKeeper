using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] private HagglingSystem hagglingSystem;

    [SerializeField] private int PlayTime;

    void Awake()
    {
        hagglingSystem.OfferingEnded += GameOverCheck;
    }

    private void GameOverCheck()
    {
        PlayTime--;
        Debug.Log("Remaining Play Time: " + PlayTime);

        if (IsOutOfPlayTime())
        {
            Debug.Log("Game Over! Out of play time.");
            GameOverScreen();
        }
        else if (IsBankrupt())
        {
            Debug.Log("Game Over! You are bankrupt.");
            GameOverScreen();
        }
    }

    private bool IsBankrupt()
    { return playerMoney.IsOutOfMoney() && playerInventory.StorageIsEmpty(); }

    private bool IsOutOfPlayTime()
    { return PlayTime <= 0; }

    private void GameOverScreen()
    { 
        Debug.Log ("Game Over Screen Triggered.");
        SceneManager.LoadScene(2);
    }
}
