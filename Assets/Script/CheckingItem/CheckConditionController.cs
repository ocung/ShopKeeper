using System;
using UnityEngine;

public class CheckConditionController : MonoBehaviour
{

    [SerializeField] private ConditionCheckButtons conditionCheckButtons;
    [SerializeField] private DisplayedItem displayedItem;
    [SerializeField] private MarketListSO marketList;
    //[SerializeField] private MarketData smartphoneMarket;
    [SerializeField] private ConditionDisplayItem display;
    private MarketData smartphoneMarket;

    private string itemName; // Example item name, should match an entry in MarketData

    private DisplayedItem.ItemConditiion? layar;
    private DisplayedItem.ItemConditiion? koneksi;
    private DisplayedItem.ItemConditiion? kamera;
    private DisplayedItem.ItemConditiion? fisik;

    void Awake()
    { 
        smartphoneMarket = marketList.GetCurrentMarket();
    }

    void OnEnable()
    {
        ResetConditions();
        itemName = displayedItem.GetItemName(); 
        Debug.Log("Item Name: " + itemName);
    }

    void Start()
    {
        PerpareConditionCheckButtons();
        
        // Debug.Log("CheckCon..." + layar + " " + koneksi + " " + kamera + " " + fisik);
    }

    // void FixedUpdate()
    // {
    //     Debug.Log("Aku update");
    // }

    private void PerpareConditionCheckButtons()
    {
        conditionCheckButtons.InitializeButtons(HandleInput);
    }

    private void HandleInput(string inputButton)
    {
        Debug.Log("Condition button clicked: " + inputButton);
        switch (inputButton)
        {
            case "cekLayar":
                UpdateLayar();
                UpdatePrice();
                // layar = displayedItem.GetCondition().layar;
                Debug.Log("Checking layar condition..." + layar);
                break;
            case "cekKoneksi":
                UpdateKoneksi();
                UpdatePrice();
                //koneksi = displayedItem.GetCondition().koneksi;
                Debug.Log("Checking koneksi condition..." + koneksi);
                break;
            case "cekKamera":
                UpdateKamera();
                UpdatePrice();
                //kamera = displayedItem.GetCondition().kamera;
                Debug.Log("Checking kamera condition..." + kamera);
                break;
            case "cekFisik":
                UpdateFisik();
                UpdatePrice();
                //fisik = displayedItem.GetCondition().fisik;
                Debug.Log("Checking fisik condition..." + fisik);
                break;
            default:
                Debug.LogWarning("Unknown condition button: " + inputButton);
                return;
        }

    }

    private void UpdatePrice()
    {
        if (!IsConditionChecked())
        {
            Debug.LogWarning("Not all conditions are checked. Cannot update price.");
            return;
        }
        display.SetPrice(GetItemPrice());
    }

    private void UpdateLayar()
    {
        layar = displayedItem.GetCondition().layar;
        if (layar == DisplayedItem.ItemConditiion.Good)
        {
            display.SetGoodLayar();
            Debug.Log("layarDisplayGood");
        }
        else if (layar == DisplayedItem.ItemConditiion.Broken)
        {
            display.SetBrokeLayar();
            Debug.Log("layarDisplayBroke");
        }

    }

    private void UpdateKoneksi()
    {
        koneksi = displayedItem.GetCondition().koneksi;
        if (koneksi == DisplayedItem.ItemConditiion.Good)
        {
            display.SetGoodKoneksi();
            Debug.Log("koneksiDisplayGood");
        }
        else if (koneksi == DisplayedItem.ItemConditiion.Broken)
        {
            display.SetBrokeKoneksi();
            Debug.Log("koneksiDisplayBroke");
        }
    }

    public SmartPhone GetDisplayedPhone()
    { 
        return displayedItem.GetSmartPhone();
    }

    private void UpdateKamera()
    {
        kamera = displayedItem.GetCondition().kamera;
        if (kamera == DisplayedItem.ItemConditiion.Good)
        {
            display.SetGoodKamera();
            Debug.Log("kameraDisplayGood");
        }
        else if (kamera == DisplayedItem.ItemConditiion.Broken)
        {
            display.SetBrokeKamera();
            Debug.Log("kameraDisplayBroke");
        }
    }

    private void UpdateFisik()
    {
        fisik = displayedItem.GetCondition().fisik;
        if (fisik == DisplayedItem.ItemConditiion.Good)
        {
            display.SetGoodFisik();
            Debug.Log("fisikDisplayGood");
        }
        else if (fisik == DisplayedItem.ItemConditiion.Broken)
        {
            display.SetBrokeFisik();
            Debug.Log("fisikDisplayBroke");
        }
    }

    private bool IsConditionChecked()
    {
        return layar.HasValue && koneksi.HasValue && kamera.HasValue && fisik.HasValue;
    }

    public void ResetConditions()
    {
        layar = null;
        koneksi = null;
        kamera = null;
        fisik = null;

        display.ResetDisplay();
    }

    public int GetConditionScore()
    {
        int score = 0;
        score += (int)layar;
        score += (int)koneksi;
        score += (int)kamera;
        score += (int)fisik;

        return score;
    }

    public string GetConditionCategory()
    {
        int score = GetConditionScore();

        if (score == 100)
        { return "Perfect"; }
        else if (score >= 75)
        { return "Good"; }
        else if (score >= 50)
        { return "Fair"; }
        else if (score >= 25)
        { return "Damaged"; }
        else
        { return "Poor"; }
    }

    public int GetItemPrice()
    {
        int priced;
        int score = GetConditionScore();

        if (score == 100)
        { return priced = smartphoneMarket.GetPrice(itemName); }
        else if (score >= 75)
        { return priced = smartphoneMarket.GetPrice(itemName) * 90 / 100; }
        else if (score >= 50)
        { return priced = smartphoneMarket.GetPrice(itemName) * 75 / 100; }
        else if (score >= 25)
        { return priced = smartphoneMarket.GetPrice(itemName) * 50 / 100; }
        else
        { return priced = smartphoneMarket.GetPrice(itemName) * 20 / 100; }
    }

    public void ShowConditionDisplay()
    { 
        display.Show();
    }
    public void ShowConditionCheckButtons()
    { 
        gameObject.SetActive(true);
    }

    public void HideConditionDisplay()
    {
        display.Hide();
    }
    public void HideConditionCheckButtons()
    { 
        gameObject.SetActive(false);
    }

    //test Purpose
    // public void TestPrintScore()
    // {
    //     Debug.Log("Condition Score: " + GetConditionScore());
    //     Debug.Log("Priced: " + GetItemPrice());
    // }

    // public void TestPrintCategory()
    // {
    //     Debug.Log("Condition Category: " + GetConditionCategory());
    // }
}
