using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthPotionButton : ShopButton
{
    [SerializeField] private HealthPotion healthPotion;
    
    public override void OnPress()
    {
        if (Shop.GetInstance().WalletOfBuyer.currency >= cost)
        {
            bool wasUsed = healthPotion.UseMe(GameManager.GetInstance().Player.gameObject);
            if (wasUsed)
            {
                Shop.GetInstance().BuyMe(cost);
                SetInteractability(false);
            }
        }
    }
}
