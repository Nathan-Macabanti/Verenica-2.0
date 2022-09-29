using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Being
{
#if false
    #region Singleton
    private static Player instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static Player GetInstance() { return instance; }
    #endregion
#endif
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

    protected override void Die()
    {
        isDead = true;
        EventManager.InvokeLose();

        //base.Die();
    }
}
