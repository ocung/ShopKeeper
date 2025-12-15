using System;
using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerSpawner spawner;
    [SerializeField] private HagglingSystem hagglingSystem;
    [SerializeField] private PlayerInventory playerInventory;//TestPurpose
    [SerializeField] private CustomerStateUI stateUI;
    [SerializeField] private DialogueTrigger SellDialogueTrigger;
    [SerializeField] private DialogueTrigger BuyDialogueTrigger;

    [SerializeField] private int patienty = 6;
    [SerializeField] private float highFairnessThreshold = 0.31f;
    [SerializeField] private float lowFairnessThreshold = 0.2f;
    private int customerOffer;//test purpose
    private int offereRate;//test purpose
    
    public event Action OnBuying, OnSelling, OnHaggling, OnRefusing, OnLowFairness, OnHighFairness;
    public event Action OnStartHaggling;

    public CustomerState customerState;
    public CustomerType customerType;
    // public CustomerState TestState;//TestPurpose

    public enum CustomerState
    {
        Buying,
        Selling
    }
    public enum CustomerType
    {
        DosntKnowPrice,
        KnowsPrice
    }

    void OnEnable()
    {
        ResetPatienty();
        spawner.OnCustomerSpawn -= Spawn;
        spawner.OnCustomerDespawn += Despawn;
        hagglingSystem.OfferingEnded += OnHagglingEnd;

        GetState();
        GetTypee();
        //customerType = CustomerType.KnowsPrice;//TestPurpose
        Debug.Log("Customer State: " + customerState);

        ResetOfferRate();

        DisplayStartDialogue();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Buying(int offerPrice, int itemPriced)
    {

        SetCustomerOffer(itemPriced);

        float profitShopkeeper = SellProfit(offerPrice, itemPriced); //test purpose
        float profitCustomer = BuyProfit(offerPrice, itemPriced); //test purpose
        Debug.Log("Profit Shopkeeper: " + profitShopkeeper );
        Debug.Log("Profit Customer: " + profitCustomer );

        if (isFairOrNot(profitShopkeeper, profitCustomer))
        {
            return;
        }

        if (isCustomerAcceptingBuyOffer(offerPrice, itemPriced))
        {
            OnBuying?.Invoke();
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AcceptPrice);
            Debug.Log("Accepting the offer");
        }
        else if (Haggling(offerPrice, itemPriced))
        {
            OnHaggling?.Invoke();
            patienty--;
            if (patienty <= 0)
            {
                ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AcceptPrice);
                Debug.Log("Haggling because patienty 0");
                return;
            }
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AskPrice);
            Debug.Log("Haggling but still patienty left");
            Debug.Log("Customer is haggling, remaining patienty: " + patienty);
        }
        else
        {
            OnRefusing?.Invoke();
            patienty--;
            if (patienty <= 0)
            {
                ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.RefusePrice);
                Debug.Log("Refuse because patienty 0");
                return;
            }
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AskPrice);
            Debug.Log("Refuse but still patienty left");
            Debug.Log("Customer is refusing, remaining patienty: " + patienty);
        }
    }

    public void Selling(int offerPrice, int itemPriced)
    {
        
        SetCustomerOffer(itemPriced);

        float profitShopkeeper = BuyProfit(offerPrice, itemPriced); //test purpose
        float profitCustomer = SellProfit(offerPrice, itemPriced); //test purpose
        Debug.Log("Profit Shopkeeper: " + profitShopkeeper );
        Debug.Log("Profit Customer: " + profitCustomer );

        if (isFairOrNot(profitShopkeeper, profitCustomer))
        {
            return;
        }

        if (isCustomerAcceptingSellOffer(offerPrice, itemPriced))
        {
            OnSelling?.Invoke();
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AcceptPrice);
            Debug.Log("Accepting the offer");
        }
        else if (Haggling(offerPrice, itemPriced))
        {
            OnHaggling?.Invoke();
            patienty--;
            if (patienty <= 0)
            {
                ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AcceptPrice);
                Debug.Log("Haggling because patienty 0");
                return;
            }
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AskPrice);
            Debug.Log("Haggling but still patienty left");
            Debug.Log("Customer is haggling, remaining patienty: " + patienty);
        }
        else
        {
            OnRefusing?.Invoke();
            patienty--;
            if (patienty <= 0)
            {
                ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.RefusePrice);
                Debug.Log("Refuse because patienty 0");
                return;
            }
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AskPrice);
            Debug.Log("Refuse but still patienty left");
            Debug.Log("Customer is refusing, remaining patienty: " + patienty);
        }
    }

    public bool isCustomerAcceptingBuyOffer(int offerPrice, int itemPriced)
    {
        return offerPrice <= customerOffer;
    }

    public bool Haggling(int offerPrice, int itemPriced)
    {
        switch (customerState)
        {
            case CustomerState.Buying:
                return customerOffer < offerPrice && offerPrice < MaxPrice(itemPriced);
            case CustomerState.Selling:
                return MinPrice(itemPriced) < offerPrice && offerPrice < customerOffer;
        }
        return false;
    }

    public bool isCustomerAcceptingSellOffer(int offerPrice, int itemPriced)
    {
        return offerPrice >= customerOffer;
    }

    // public void SetPatienty(int value)
    // {
    //     patienty = value;
    //     Debug.Log("Customer Patiency: " + patienty);
    // }

    public void DisplayStateUI()
    {
        switch (customerState)
        {
            case CustomerState.Buying:
                stateUI.SetBuying();
                Debug.Log("DisplayBuying");
                break;
            case CustomerState.Selling:
                stateUI.SetSelling();
                Debug.Log("DisplaySelling");
                break;
        }
    }

    public void HideStateUI()
    {
        stateUI.Hide();
    }

    private void SetOfferRate()
    {
        offereRate += UnityEngine.Random.Range(2, 6);
        Debug.Log("SetUp Offer Rate: " + offereRate);
    }
    private void ResetOfferRate()
    {
        offereRate = 0;
        Debug.Log("Reset Offer Rate: " + offereRate);
    }

    private void SetCustomerOffer(int itemPrice)
    {
        SetOfferRate();
        switch (customerState)
        {
            case CustomerState.Buying:
                customerOffer = MinPrice(itemPrice) * (100 + offereRate) / 100;
                Debug.Log("Customer Offer for Buying: " + customerOffer);
                break;
            case CustomerState.Selling:
                customerOffer = MaxPrice(itemPrice) * (100 - offereRate) / 100;
                Debug.Log("Customer Offer for Selling: " + customerOffer);
                break;
        }
    }

    private float BuyProfit (int offer, int refPrice) // offer = priice shopkeeper gives to customer, ref_price = price of item
    {
        float profit = (refPrice - offer) / (float)offer;
        Debug.Log("Buy Profit: " + profit);
        return profit;
    }
    
    private float SellProfit (int offer, int refPrice) // offer = price shopkeeper give from customer, ref_price = price of item
    {
        float profit = (offer - refPrice) / (float)refPrice;
        Debug.Log("Sell Profit: " + profit);
        return profit;
    }

    private float CalculateInequality (float profitShopkeeper, float profitCustomer)
    {
        float absProfitCustomer = Math.Abs(profitCustomer);
        float absInequality = Math.Abs(profitShopkeeper - profitCustomer);

        if (absProfitCustomer == 0)
        {
            Debug.Log("Absolute Profit Customer is " + absInequality);
            return absInequality;
        }
        
        float inequality = absInequality / absProfitCustomer;
        Debug.Log("Inequality is " + inequality);
        return inequality;
    }

    private float CalculateFairness (float profitShopkeeper, float profitCustomer)
    {
        float fairness = 1 / (1 + CalculateInequality(profitShopkeeper, profitCustomer));
        Debug.Log("Fairness is " + fairness);
        return fairness;
    }

    private bool isFairOrNot(float profitShopkeeper, float profitCustomer)
    {
        if (CalculateFairness(profitShopkeeper, profitCustomer) >= highFairnessThreshold)
        {
            OnHighFairness?.Invoke();
            Debug.Log("High Fairness - Customer is more likely to accept the offer.");

            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AcceptPrice);
            return true;   
        }
        if (CalculateFairness(profitShopkeeper, profitCustomer) <= lowFairnessThreshold)
        {
            OnLowFairness?.Invoke();
            Debug.Log("Low Fairness - Customer is more likely to refuse the offer.");

            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.RefusePrice);
            return true;   
        }
        return false;
    }

    private int MinPrice(int itemPrice)
    {
        int minPrice;
        if (customerType == CustomerType.KnowsPrice)
        {
            switch (customerState)
            {
                case CustomerState.Buying:
                    minPrice = itemPrice * 70 / 100;
                    Debug.Log("Known Min Price for Buying: " + minPrice);
                    return minPrice;
                case CustomerState.Selling:
                    minPrice = itemPrice * 90 / 100;
                    Debug.Log("Known Min Price for Selling: " + minPrice);
                    return minPrice;
            }
        }
        minPrice = itemPrice * 70 / 100;
        Debug.Log("Min Price: " + minPrice);
        return minPrice;
    }

    private int MaxPrice(int itemPrice)
    {
        int maxPrice;
        if (customerType == CustomerType.KnowsPrice)
        {
            switch (customerState)
            {
                case CustomerState.Buying:
                    maxPrice = itemPrice * 110 / 100;
                    Debug.Log("Known Max Price for Buying: " + maxPrice);
                    return maxPrice;
                case CustomerState.Selling:
                    maxPrice = itemPrice * 130 / 100;
                    Debug.Log("Known Max Price for Selling: " + maxPrice);
                    return maxPrice;
            }
        }
        maxPrice = itemPrice * 130 / 100;
        Debug.Log("Max Price: " + maxPrice);
        return maxPrice;
    }

    private static T GetRandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(randomIndex);
    }

    private void GetState()
    {
        if (playerInventory.StorageIsEmpty())
        {
            customerState = CustomerState.Selling;//TestPurpose
        }
        else if (playerInventory.StorageIsFull())
        {
            customerState = CustomerState.Buying;//TestPurpose
        }
        else
        {
            customerState = GetRandomEnum<CustomerState>();//TestPurpose
        }
    }

    private void GetTypee()
    {
        customerType = GetRandomEnum<CustomerType>();
        Debug.Log("Customer Type: " + customerType);
    }

    private void DisplayStartDialogue()
    {
        switch (customerState)
        {
            case CustomerState.Buying:
                //BuyDialogueTrigger.TriggerStartDialogue(hagglePrice);
                BuyDialogueTrigger.TriggerStartDialogue();
                break;
            case CustomerState.Selling:
                //SellDialogueTrigger.TriggerStartDialogue(hagglePrice);
                SellDialogueTrigger.TriggerStartDialogue();
                break;
        }
    }

    private void ResetPatienty()
    {
        patienty = 6;
    }

    private void Spawn()
    {
        gameObject.SetActive(true);
        OnStartHaggling?.Invoke();
        Debug.Log("Customer Spawned");
    }

    private void Despawn()
    {
        // gameObject.SetActive(false);
        // Debug.Log("Customer Despawned");
        StartCoroutine(DelayDespawn());
        Debug.Log("Despawn Customer Called");
    }

    private void OnHagglingEnd()
    {
        // StartCoroutine(DelayDespawn());
        spawner.OnHagglingEnd();
    }

    IEnumerator DelayDespawn()
    {
        yield return new WaitForSeconds(3f);
        //spawner.OnHagglingEnd();
        gameObject.SetActive(false);
        Debug.Log("Customer Despawned");
    }

    private void ShowDialogueNextFrame(DialogueTrigger.DialogueChoice choice)
    {
        StopCoroutine(DelayDialogue(choice));
        StartCoroutine(DelayDialogue(choice));
    }

    private IEnumerator DelayDialogue(DialogueTrigger.DialogueChoice choice)
    {
        yield return null; // tunggu 1 frame
        switch (customerState)
        {
            case CustomerState.Buying:
                BuyDialogueTrigger.TriggerChangeDialogue(choice, customerOffer);
                break;
            case CustomerState.Selling:
                SellDialogueTrigger.TriggerChangeDialogue(choice, customerOffer);
                break;
        }
    }

    void OnDisable()
    {
        spawner.OnCustomerSpawn += Spawn;
        spawner.OnCustomerDespawn -= Despawn;
    }

}
