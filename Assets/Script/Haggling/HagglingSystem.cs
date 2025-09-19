using System;
using System.Collections;
using UnityEngine;

public class HagglingSystem : MonoBehaviour
{
    [SerializeField] private CalculatorController calculator;
    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] private Customer customer;
    [SerializeField] private CheckConditionController checkCondition;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private HagglingTimerUI hagglingTimerUI;

    [SerializeField] private int hagglingTimer = 6;

    private string offeredPrice;

    public event Action OfferingEnded;

    void OnEnable()
    {
        hagglingTimerUI.SetDisplayText(hagglingTimer);
        customer.OnBuying += HandleBuying;
        customer.OnSelling += HandleSelling; 
        customer.OnHaggling += HandleHaggling;
        customer.OnRefusing += HandleRefusing;
        calculator.OfferingPrice += PriceOffered;
    }

    private void PriceOffered()
    {
        Debug.Log("Price has been offered!");
        offeredPrice = calculator.GetCalculatorCurrentValue();

        switch (customer.customerState)
        {
            case Customer.CustomerState.Buying:
                CustomerAcceptedForBuy(offeredPrice);
                break;
            case Customer.CustomerState.Selling:
                CustomerAcceptedForSell(offeredPrice);
                break;
        }
    }

    private void CustomerAcceptedForBuy(string offeredPrice)
    {
        Debug.Log("Customer Considered the buy offer.");

        customer.Buying(int.Parse(offeredPrice), checkCondition.GetItemPrice());
    }
    private void CustomerAcceptedForSell(string offeredPrice)
    {
        Debug.Log("Customer Considered the sell offer.");
        customer.Selling(int.Parse(offeredPrice), checkCondition.GetItemPrice());
    }

    private void HandleBuying()
    {
        AddMoney(int.Parse(offeredPrice));
        playerInventory.RemoveSmartPhone();//test
        OfferingEnded?.Invoke();
        Debug.Log("Customer accepted the offer: " + offeredPrice);
        Debug.Log("Haggling ended, despawning customer.");
    }

    private void HandleSelling()
    {
        SubtractMoney(int.Parse(offeredPrice));
        playerInventory.AddSmartPhone(checkCondition.GetDisplayedPhone());
        OfferingEnded?.Invoke();
        Debug.Log("Customer accepted the offer: " + offeredPrice);
        Debug.Log("Haggling ended, despawning customer.");
    }
    
    private void HandleHaggling()
    {
        hagglingTimer--;
        hagglingTimerUI.SetDisplayText(hagglingTimer);
        Debug.Log("Customer is haggling, remaining time: " + hagglingTimer);

        if (hagglingTimer <= 0)
        {
            switch (customer.customerState)
            {
                case Customer.CustomerState.Buying:
                    AddMoney(int.Parse(offeredPrice));
                    playerInventory.RemoveSmartPhone();//test
                    Debug.Log("Customer accepted the offer: " + offeredPrice);
                    break;
                case Customer.CustomerState.Selling:
                    SubtractMoney(int.Parse(offeredPrice));
                    playerInventory.AddSmartPhone(checkCondition.GetDisplayedPhone());
                    Debug.Log("Customer accepted the offer: " + offeredPrice);
                    break;
            }
            ResetTimer();
            OfferingEnded?.Invoke();
            Debug.Log("Haggling ended, despawning customer.");
            return;
        }
        Debug.Log("try offer again");
    }
    
    private void HandleRefusing()
    {
        hagglingTimer--;
        hagglingTimerUI.SetDisplayText(hagglingTimer);
        Debug.Log("Customer is haggling, remaining time: " + hagglingTimer);

        if (hagglingTimer <= 0) // Haggling Time Out
        {
            ResetTimer();
            OfferingEnded?.Invoke();
            Debug.Log("Customer rejected the offer: " + offeredPrice);
            Debug.Log("Haggling ended, despawning customer.");
            return;
        }
        Debug.Log("try offer again");
    }

    private void ResetTimer()
    { 
        hagglingTimer = 6; 
    }
    private void AddMoney(int amount)
    {
        playerMoney.AddMoney(amount);
    }

    private void SubtractMoney(int amount)
    { 
        playerMoney.SubtractMoney(amount);
    }

    public void OfferingEnd()
    { 
        OfferingEnded?.Invoke();
    }

    public void HideHagglingTimer()
    { 
        hagglingTimerUI.Hide();
    }

    public void ShowHagglingTimer()
    {
        hagglingTimerUI.SetDisplayText(hagglingTimer);
        hagglingTimerUI.Show();
    }

    void OnDisable()
    {
        customer.OnBuying -= HandleBuying;
        customer.OnSelling -= HandleSelling;
        customer.OnHaggling -= HandleHaggling;
        customer.OnRefusing -= HandleRefusing;
        calculator.OfferingPrice -= PriceOffered;
    }
}
