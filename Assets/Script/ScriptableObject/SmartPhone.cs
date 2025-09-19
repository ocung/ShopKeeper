using UnityEngine;

[CreateAssetMenu(fileName = "SmartPhone", menuName = "Scriptable Objects/SmartPhone")]
public class SmartPhone : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private DisplayedItem.ItemConditiion layar;
    [SerializeField] private DisplayedItem.ItemConditiion koneksi;
    [SerializeField] private DisplayedItem.ItemConditiion kamera;
    [SerializeField] private DisplayedItem.ItemConditiion fisik;

    public string ItemName => itemName;
    public (DisplayedItem.ItemConditiion layar, DisplayedItem.ItemConditiion koneksi, DisplayedItem.ItemConditiion kamera, DisplayedItem.ItemConditiion fisik) GetCondition()
    { 
        return (layar, koneksi, kamera, fisik);
    }
}
