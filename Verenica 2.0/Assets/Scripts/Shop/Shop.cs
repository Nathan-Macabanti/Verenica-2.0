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

    private Wallet _walletOfBuyer;
    public Wallet WalletOfBuyer { get { return _walletOfBuyer; } }
    [SerializeField] private bool shopIsOpen;
    //[SerializeField] private Stock[] _Shopstocks;

    private void Start()
    {
        _walletOfBuyer = GameManager.GetInstance().Player.playerWallet;
        shopIsOpen = false;
    }

    public bool BuyMe(int cost)
    {
        if (!shopIsOpen) { Debug.Log("SHOP IS CLOSED"); return false; } //Shop is not open
        if (_walletOfBuyer == null) { Debug.Log("NO WALLET"); return false; } //No wallet seen

        return _walletOfBuyer.RemoveCurrency((int)cost);

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
