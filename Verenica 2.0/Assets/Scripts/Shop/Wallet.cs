using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public uint currency { get; private set; }
    private uint MaxCurrency = 5;

    public void AddCurrency(uint addition)
    {
        currency += addition;
        currency = (uint)Mathf.Clamp(currency, 0, MaxCurrency);
    }

    public bool RemoveCurrency(uint subtract)
    {
        if (currency - subtract < 0) return false;

        currency -= subtract;
        return true;
    }

    public void ResetWallet()
    {
        currency = 0;
    }
}
