using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackPotion : Potion
{
    [SerializeField] private int attackValue = 2;
    public override bool UseMe(GameObject obj)
    {
        if (obj.TryGetComponent<Player>(out Player player))
        {
            PlayerAttack playerAttack = player.playerAttack;
            if (playerAttack.CurrentAttackValue != attackValue)
            {
                playerAttack.CurrentAttackValue = attackValue;
                Debug.Log("Attack enhanced");
                return true;
            }
            else
            {
                Debug.Log("Attack already enhanced");
                return false;
            }   
        }

        return false;
    }
}
