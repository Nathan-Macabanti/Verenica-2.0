using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Wallet : MonoBehaviour
{
    private Player owner;
    public int currency { get; private set; } = 0;

    private int MaxCurrency = 5;

    private void Start()
    {
        owner = GetComponent<Player>();
    }
    public void AddCurrency(int addition)
    {
        currency += addition;
        currency = Mathf.Clamp(currency, 0, MaxCurrency);
        EventManager.InvokeOnMoneyUpdated();
    }

    public bool RemoveCurrency(int subtract)
    {
        if (currency - subtract < 0) return false;

        currency -= subtract;
        EventManager.InvokeOnMoneyUpdated();
        return true;
    }

    public void ResetWallet()
    {
        currency = 0;
        EventManager.InvokeOnMoneyUpdated();
    }

    private void OnEnable()
    {
        EventManager.OnEnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDied -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        //Debug.Log(owner.CurrentHP);
        AddCurrency(owner.CurrentHP);
    }
}
