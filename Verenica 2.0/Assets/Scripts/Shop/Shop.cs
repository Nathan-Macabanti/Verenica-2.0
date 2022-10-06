using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Singleton
    private static Shop instance;
    private void InitializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static Shop GetInstance() { return instance; }
    #endregion

    private void Awake()
    {
        InitializeSingleton();
    }

    private Wallet walletOfBuyer;

    [SerializeField] private bool shopIsOpen;
    [SerializeField] private Stock[] _Shopstocks;

    private void Start()
    {
        walletOfBuyer = GameManager.GetInstance().Player.playerWallet;
        shopIsOpen = false;
    }

    public void BuyPotion(uint inventoryIndex)
    {
        if (!shopIsOpen) return; //Shop is not open
        if (walletOfBuyer == null) return; //No wallet seen

        //GetMoney from wallet if you cannot then do not
        Stock stock = _Shopstocks[inventoryIndex];
        if (!walletOfBuyer.RemoveCurrency(stock.cost)) return;

        //Use Immediately on player
        Potion potion = _Shopstocks[inventoryIndex].potion;
        potion.UseMe(GameManager.GetInstance().Player.gameObject);
    }

    #region Opening and Closing Shop
    public void OpenOrCloseShop(bool isOpen)
    {
        shopIsOpen = isOpen;
        EventManager.InvokeOnIsShopOpenValueChange(shopIsOpen);
        ChangeGameState();
    }

    public void OpenOrCloseShop(Phase phase)
    {
        if(PhaseManager.GetInstance().isOnTheFirstPhase)
        {
            shopIsOpen = false;
        }
        else
        {
            shopIsOpen = true;
        }

        EventManager.InvokeOnIsShopOpenValueChange(shopIsOpen);
        ChangeGameState();
    }
    #endregion

    public void ChangeGameState()
    {
        if (shopIsOpen)
        {
            EventManager.InvokeGameStateChanged(GameState.paused);
        }
        else
        {
            EventManager.InvokeGameStateChanged(GameState.playing);
        }
    }
    private void OnEnable()
    {
        EventManager.OnPhaseChange += OpenOrCloseShop;
    }

    private void OnDisable()
    {
        EventManager.OnPhaseChange -= OpenOrCloseShop;
    }
}
