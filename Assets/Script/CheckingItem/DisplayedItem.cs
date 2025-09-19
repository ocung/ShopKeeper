using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DisplayedItem : MonoBehaviour
{

    [SerializeField] private Customer customer;//TestPurpose
    [SerializeField] private PlayerInventory playerInventory;//TestPurpose

    [SerializeField] private SmartPhone[] CustomerSmartphone;

    private SmartPhone DisplayedSmartphone;

    private string itemName;

    private ItemConditiion layar;
    private ItemConditiion koneksi;
    private ItemConditiion kamera;
    private ItemConditiion Fisik;

    private int displayedIndex;

    public enum ItemConditiion
    {
        Good = 25,
        Broken = 0
    }

    void OnEnable()
    {
        SetDisplayData();
        Debug.Log("DisplayListArray:" + displayedIndex);
    }

    private void SetDisplayData()
    {
        switch (customer.customerState)
        {
            case Customer.CustomerState.Buying:
                DisplayedSmartphone = playerInventory.GetSmartphone();
                SetPhonesData();
                break;
            case Customer.CustomerState.Selling:
                displayedIndex = Random.Range(0, CustomerSmartphone.Length);
                Debug.Log("Selected Customer Inventory " + displayedIndex);
                DisplayedSmartphone = CustomerSmartphone[displayedIndex];
                SetPhonesData();
            break;
        }
        // displayedIndex = Random.Range(0, DisplayedSmartphone.Length);

        // itemName = DisplayedSmartphone[displayedIndex].ItemName;
        // layar = DisplayedSmartphone[displayedIndex].GetCondition().layar;
        // koneksi = DisplayedSmartphone[displayedIndex].GetCondition().koneksi;
        // kamera = DisplayedSmartphone[displayedIndex].GetCondition().kamera;
        // Fisik = DisplayedSmartphone[displayedIndex].GetCondition().fisik;
    }

    private void SetPhonesData()
    {
        itemName = DisplayedSmartphone.ItemName;
        layar = DisplayedSmartphone.GetCondition().layar;
        koneksi = DisplayedSmartphone.GetCondition().koneksi;
        kamera = DisplayedSmartphone.GetCondition().kamera;
        Fisik = DisplayedSmartphone.GetCondition().fisik;
        
    }

    public string GetItemName()
    { return itemName; }

    public (ItemConditiion layar, ItemConditiion koneksi, ItemConditiion kamera, ItemConditiion fisik) GetCondition()
    {
        return (layar, koneksi, kamera, Fisik);
    }

    public SmartPhone GetSmartPhone()
    { 
        return DisplayedSmartphone;
    }

    public void Show()
    {
        gameObject.SetActive(true);

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
