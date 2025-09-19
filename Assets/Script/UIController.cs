using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] private DisplayedItem displayedItem;
    [SerializeField] private CalculatorController calculatorController;
    [SerializeField] private CheckConditionController checkConditionController;
    [SerializeField] private HagglingSystem hagglingSystem;
    [SerializeField] private Customer customer;

    void OnEnable()
    {
        customer.OnStartHaggling += StartHagglingUI;
        hagglingSystem.OfferingEnded += HideAllUI;
    }

    void Start()
    {
        HideAllUI();
    }

    private void HideAllUI()
    {

        calculatorController.HideCalculator();
        checkConditionController.HideConditionDisplay();
        checkConditionController.HideConditionCheckButtons();
        hagglingSystem.HideHagglingTimer();
        displayedItem.Hide();

    }

    private void StartHagglingUI()
    {
        calculatorController.ShowCalculator();
        checkConditionController.ShowConditionDisplay();
        checkConditionController.ShowConditionCheckButtons();
        hagglingSystem.ShowHagglingTimer();
        displayedItem.Show();
    }

}
