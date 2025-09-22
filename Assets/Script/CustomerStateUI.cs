
using UnityEngine;
using UnityEngine.UI;

public class CustomerStateUI : MonoBehaviour
{

    [SerializeField] private Image buyingImage;
    [SerializeField] private Image sellingImage;

    void Start()
    {

    }

    public void SetBuying()
    {
        buyingImage.gameObject.SetActive(true);
        sellingImage.gameObject.SetActive(false);
    }

    public void SetSelling()
    {
        sellingImage.gameObject.SetActive(true);
        buyingImage.gameObject.SetActive(false);
    }
    
    public void Hide()
    {
        buyingImage.gameObject.SetActive(false);
        sellingImage.gameObject.SetActive(false);
    }

}
