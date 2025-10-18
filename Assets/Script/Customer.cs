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
    
    public event Action OnBuying, OnSelling, OnHaggling, OnRefusing;
    public event Action OnStartHaggling;

    public CustomerState customerState;
    // public CustomerState TestState;//TestPurpose

    public enum CustomerState
    {
        Buying,
        Selling
    }

    void OnEnable()
    {
        ResetPatienty();
        spawner.OnCustomerSpawn -= Spawn;
        spawner.OnCustomerDespawn += Despawn;
        hagglingSystem.OfferingEnded += OnHagglingEnd;

        GetState();
        Debug.Log("Customer State: " + customerState);//TestPurpose

        DisplayStartDialogue();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Buying(int offerPrice, int itemPriced) 
    {
        if (isCustomerAcceptingBuyOffer(offerPrice, itemPriced))
        {
            OnBuying?.Invoke();
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AcceptPrice);
            Debug.Log("Accepting the offer");
            // BuyDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.AcceptPrice);
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
            // BuyDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.AskPrice);
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
            // if (patienty <= 0)
            // {
            //     BuyDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.RefusePrice);
            //     return;
            // }
            // BuyDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.AskPrice);
        }
    }

    public void Selling(int offerPrice, int itemPriced) 
    {
        if (isCustomerAcceptingSellOffer(offerPrice, itemPriced))
        {
            OnSelling?.Invoke();
            ShowDialogueNextFrame(DialogueTrigger.DialogueChoice.AcceptPrice);
            Debug.Log("Accepting the offer");
            // SellDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.AcceptPrice);
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
            // if (patienty <= 0)
            // {
            //     SellDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.AcceptPrice);
            //     return;
            // }
            // SellDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.AskPrice);
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
            // if (patienty <= 0)
            // {
            //     SellDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.RefusePrice);
            //     // StartCoroutine(DelayRefuseDialogue());
            //     return;
            // }
            // SellDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.AskPrice);
        }
    }

    public bool isCustomerAcceptingBuyOffer(int offerPrice, int itemPriced)
    {
        return offerPrice <= MinPrice(itemPriced);
    }

    public bool Haggling(int offerPrice, int itemPriced)
    {
        return MinPrice(itemPriced) < offerPrice && offerPrice < MaxPrice(itemPriced);
    }

    public bool isCustomerAcceptingSellOffer(int offerPrice, int itemPriced)
    {
        return offerPrice >= MaxPrice(itemPriced);
    }

    public void SetPatienty(int value)
    {
        patienty = value;
        Debug.Log("Customer Patiency: " + patienty);
    }

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

    private int MinPrice(int itemPrice)
    {
        int minPrice = itemPrice * 70 / 100;
        Debug.Log("Min Price: " + minPrice);
        return minPrice;
    }

    private int MaxPrice(int itemPrice)
    {
        int maxPrice = itemPrice * 130 / 100;
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

    private void DisplayStartDialogue()
    {
        switch (customerState)
        {
            case CustomerState.Buying:
                BuyDialogueTrigger.TriggerStartDialogue();
                break;
            case CustomerState.Selling:
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
                BuyDialogueTrigger.TriggerChangeDialogue(choice);
                break;
            case CustomerState.Selling:
                SellDialogueTrigger.TriggerChangeDialogue(choice);
                break;
        }
        //SellDialogueTrigger.TriggerChangeDialogue(choice);
    }

    // IEnumerator DelayRefuseDialogue()
    // {
    //     yield return null;
    //     switch (customerState)
    //     {
    //         case CustomerState.Buying:
    //             BuyDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.RefusePrice);
    //             break;
    //         case CustomerState.Selling:
    //             SellDialogueTrigger.TriggerChangeDialogue(DialogueTrigger.DialogueChoice.RefusePrice);
    //             break;
    //     }
    // }

    void OnDisable()
    {
        spawner.OnCustomerSpawn += Spawn;
        spawner.OnCustomerDespawn -= Despawn;
    }

}
