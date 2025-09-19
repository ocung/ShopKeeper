using UnityEngine;
using UnityEngine.UI;

public class ConditionLabel : MonoBehaviour
{
    [SerializeField] private Image goodLabel;
    [SerializeField] private Image brokeLabel;

    void Start()
    {
        NotChecked();
    }

    public void SetGood()
    {
        goodLabel.gameObject.SetActive(true);
        brokeLabel.gameObject.SetActive(false);
    }
    
    public void SetBroke()
    {
        brokeLabel.gameObject.SetActive(true);
        goodLabel.gameObject.SetActive(false);
    }

    public void NotChecked()
    {
        goodLabel.gameObject.SetActive(false);
        brokeLabel.gameObject.SetActive(false);
    }
}
