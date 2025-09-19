using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MarketDataSO", menuName = "Data/Market Data")]
public class MarketData : ScriptableObject
{
    [System.Serializable]
    public class MarketEntry
    {
        public string itemName;
        public int basePrice;
    }

    public List<MarketEntry> itemList = new List<MarketEntry>();

    private Dictionary<string, int> itemPriceDict;

    public void Initialize()
    {
        itemPriceDict = new Dictionary<string, int>();
        foreach (var item in itemList)
        {
            itemPriceDict[item.itemName] = item.basePrice;
        }
    }

    public int GetPrice(string itemName)
    {
        if (itemPriceDict == null) Initialize();

        if (itemPriceDict.TryGetValue(itemName, out int price))
            return price;

        Debug.LogWarning("Item not found in MarketData: " + itemName);
        return 0;
    }
}