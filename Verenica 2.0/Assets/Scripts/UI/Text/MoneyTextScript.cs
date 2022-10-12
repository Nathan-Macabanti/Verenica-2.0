using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyTextScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        EventManager.OnMoneyUpdated += OnMoneyUpdated;
    }

    private void OnDisable()
    {
        EventManager.OnMoneyUpdated -= OnMoneyUpdated;
    }

    void OnMoneyUpdated()
    {
        moneyText.text = "Cost: " + Shop.GetInstance().WalletOfBuyer.currency.ToString();
    }
}
