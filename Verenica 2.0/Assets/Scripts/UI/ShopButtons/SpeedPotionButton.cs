using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotionButton : ShopButton
{
    [SerializeField] private SpeedPotion speedPotion;
    public override void OnPress()
    {
        if (Shop.GetInstance().WalletOfBuyer.currency >= cost)
        {
            bool wasUsed = speedPotion.UseMe(GameManager.GetInstance().Player.gameObject);
            if (wasUsed)
            {
                Shop.GetInstance().BuyMe(cost);
                SetInteractability(false);
            }
        }
    }
}
