using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyDisplay;
    //[SerializeField] private Sprite moneyIcon;

    private int money = 90000000;

    void Awake()
    {
        // money = 0;
        SetMoney(money);
    }

    private void SetMoney(int amount)
    {
        moneyDisplay.text = amount.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        SetMoney(money);
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
        SetMoney(money);
    }

    public bool IsOutOfMoney()
    { return money <= 0; }
        

}
