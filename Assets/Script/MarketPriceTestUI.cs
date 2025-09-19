using UnityEngine;
using TMPro;

public class MarketPriceTestUI : MonoBehaviour
{

    [SerializeField] MarketListSO marketList;

    [SerializeField] TMP_Text GalaxyS23U1TB;
    [SerializeField] TMP_Text GalaxyS23U512GB;
    [SerializeField] TMP_Text GalaxyS23U256GB;

    private MarketData currentMarket;

    void Awake()
    {
        currentMarket = marketList.GetCurrentMarket();
    }

    void Start()
    {
        UpdatePriceUI();
    }

    private void UpdatePriceUI()
    {
        GalaxyS23U1TB.text = currentMarket.GetPrice("Galaxy S23U 1TB").ToString();
        GalaxyS23U512GB.text = currentMarket.GetPrice("Galaxy S23U 512GB").ToString();
        GalaxyS23U256GB.text = currentMarket.GetPrice("Galaxy S23U 256GB").ToString();
    }

}
