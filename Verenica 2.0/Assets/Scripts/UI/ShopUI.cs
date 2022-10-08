using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    private void OnEnable()
    {
        EventManager.OnIsShopOpenValueChange += OnShopIsOpenValueChange;
    }

    private void OnDisable()
    {
        EventManager.OnIsShopOpenValueChange -= OnShopIsOpenValueChange;
    }

    void OnShopIsOpenValueChange(bool isOpen)
    {
        shopUI.SetActive(isOpen);
    }
}
