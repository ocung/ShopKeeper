using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MarketListSO", menuName = "Scriptable Objects/MarketListSO")]
public class MarketListSO : ScriptableObject
{

    [SerializeField] private List<MarketData> markets = new List<MarketData>();

    private MarketData currentMarket;

    public void SetCurrentMarketFeb()
    {
        currentMarket = markets[0];
    }

    public void SetCurrentMarketAug()
    {
        currentMarket = markets[1];
    }

    public MarketData GetCurrentMarket()
    { 
        return currentMarket;
    }
    
}
