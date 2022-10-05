using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Being
{
    #region Player Attack
    private PlayerAttack playerAttack;
    public PlayerAttack GetPlayerAttack() { return playerAttack; }
    #endregion
    #region Player Movement
    private PlayerMovement playerMovement;
    public PlayerMovement GetPlayerMovement() { return playerMovement; }
    #endregion

    //private float 
    private void Start()
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
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        EventManager.InvokePlayerDamaged();
    }

    protected override void Die()
    {
        isDead = true;
        //base.Die();
    }
}
