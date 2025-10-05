using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] private int maxStorage = 5;
    [SerializeField] private PlayerInventoryUI playerInventoryUI;

    [SerializeField] private List<SmartPhone> smartPhones = new List<SmartPhone>();

    private int index;

    private void Start()
    {
        UpdateInventoryUI();
    }

    public SmartPhone GetSmartphone()
    {
        index = Random.Range(0, smartPhones.Count);
        Debug.Log("Selected Player Inventory " + index);
        return smartPhones[index];
    }

    public bool StorageIsFull()
    {
        return smartPhones.Count >= maxStorage;
    }

    public bool StorageIsEmpty()
    {
        return smartPhones == null || smartPhones.Count == 0;
    }
    public void AddSmartPhone(SmartPhone phone)
    {
        if (!StorageIsFull())
        {
            smartPhones.Add(phone);
            UpdateInventoryUI();
            Debug.Log("Smartphone added to inventory: " + phone.ItemName);
            return;
        }
        Debug.Log("Storage is full, cannot add more smartphones.");
    }

    public void RemoveSmartPhone()
    {
        smartPhones.Remove(smartPhones[index]);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        playerInventoryUI.SetValueText(smartPhones.Count, maxStorage);
        Debug.Log($"Inventory Updated: {smartPhones.Count} / {maxStorage}");
    }

}
