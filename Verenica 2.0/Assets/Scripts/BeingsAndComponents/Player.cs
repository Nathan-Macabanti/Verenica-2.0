using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Being
{
    #region Player Attack
    public PlayerAttack playerAttack { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public Wallet playerWallet { get; private set; }
    #endregion

    //private float 
    private void Awake()
    {
        InitializeHP();
        TryToSetPlayerComponents();
    }

    private void TryToSetPlayerComponents()
    {
        if(TryGetComponent<PlayerAttack>(out PlayerAttack pAttack))
        {
            playerAttack = pAttack;
        }
        if(TryGetComponent<PlayerMovement>(out PlayerMovement pMovement))
        {
            playerMovement = pMovement;
        }
        if(TryGetComponent<Wallet>(out Wallet wallet))
        {
            playerWallet = wallet;
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        SoundManager.GetInstance().PlaySound(GameSFX.GetInstance().playerHitSFX);
        EventManager.InvokePlayerDamaged();
    }
}
