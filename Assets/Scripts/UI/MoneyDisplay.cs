using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    void OnEnable()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        Cave.OnMoneyChanged += UpdateDisplay;
        UpdateDisplay(Cave.Money);
    }

    void OnDisable()
    {
        Cave.OnMoneyChanged -= UpdateDisplay;
    }

    void UpdateDisplay(int amount)
    {
        moneyText.text = $"{amount}";
    }
}