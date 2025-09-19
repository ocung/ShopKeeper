using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerSpawner spawner;
    [SerializeField] private HagglingSystem hagglingSystem;
    [SerializeField] private PlayerInventory playerInventory;//TestPurpose

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
        spawner.OnCustomerSpawn -= Spawn;
        spawner.OnCustomerDespawn += Despawn;
        hagglingSystem.OfferingEnded += OnHagglingEnd;

        GetState();
        Debug.Log("Customer State: " + customerState);//TestPurpose
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
        }
        else if (Haggling(offerPrice, itemPriced))
        {
            OnHaggling?.Invoke();
        }
        else
        {
            OnRefusing?.Invoke();
        }
    }

    public void Selling(int offerPrice, int itemPriced) 
    {
        if (isCustomerAcceptingSellOffer(offerPrice, itemPriced))
        {
            OnSelling?.Invoke();
        }
        else if (Haggling(offerPrice, itemPriced))
        {
            OnHaggling?.Invoke();
        }
        else
        {
            OnRefusing?.Invoke();
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

    private void Spawn()
    {
        gameObject.SetActive(true);
        OnStartHaggling?.Invoke();
        Debug.Log("Customer Spawned");
    }

    private void Despawn()
    {
        gameObject.SetActive(false);
        Debug.Log("Customer Despawned");
    }
    
    private void OnHagglingEnd()
    {
        spawner.OnHagglingEnd();
    }

    void OnDisable()
    {
        spawner.OnCustomerSpawn += Spawn;
        spawner.OnCustomerDespawn -= Despawn;
    }

}
