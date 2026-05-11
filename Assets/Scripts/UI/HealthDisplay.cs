using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    void OnEnable()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        Cave.OnHPChanged += UpdateDisplay;
        UpdateDisplay(Cave.HP);
    }

    void OnDisable()
    {
        Cave.OnHPChanged -= UpdateDisplay;
    }

    void UpdateDisplay(int amount)
    {
        healthText.text = $"{amount}";
    }
}