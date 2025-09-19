using TMPro;
using UnityEngine;

public class ConditionDisplayItem : MonoBehaviour
{
    [SerializeField] private ConditionLabel labelLayar;
    [SerializeField] private ConditionLabel labelKoneksi;
    [SerializeField] private ConditionLabel labelKamera;
    [SerializeField] private ConditionLabel labelFisik;

    [SerializeField] private TMP_Text Priced;

    public void SetPrice(int price)
    {
        Priced.text = price.ToString();
    }

    public void SetGoodLayar()
    {
        labelLayar.SetGood();
    }
    public void SetBrokeLayar()
    {
        labelLayar.SetBroke();
    }

    public void SetGoodKoneksi()
    {
        labelKoneksi.SetGood();
    }
    public void SetBrokeKoneksi()
    {
        labelKoneksi.SetBroke();
    }

    public void SetGoodKamera()
    {
        labelKamera.SetGood();
    }
    public void SetBrokeKamera()
    {
        labelKamera.SetBroke();
    }

    public void SetGoodFisik()
    {
        labelFisik.SetGood();
    }
    public void SetBrokeFisik()
    {
        labelFisik.SetBroke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ResetDisplay()
    {
        labelLayar.NotChecked();
        labelKoneksi.NotChecked();
        labelKamera.NotChecked();
        labelFisik.NotChecked();
        Priced.text = "";
    }
}
