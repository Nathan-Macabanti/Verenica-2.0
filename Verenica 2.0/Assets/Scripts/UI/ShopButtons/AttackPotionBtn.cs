using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPotionBtn : ShopButton
{
    [SerializeField] private AttackPotion attackPotion;
    public override void OnPress()
    {
        if (Shop.GetInstance().WalletOfBuyer.currency >= cost)
        {
            bool wasUsed = attackPotion.UseMe(GameManager.GetInstance().Player.gameObject);
            if (wasUsed)
            {
                Shop.GetInstance().BuyMe(cost);
                SetInteractability(false);
            }
        }
    }
}
