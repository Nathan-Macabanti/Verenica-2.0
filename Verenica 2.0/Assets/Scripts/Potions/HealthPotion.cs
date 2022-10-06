using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    [SerializeField] private int healingValue = 1;

    public override void UseMe(GameObject obj)
    {
        if(obj.TryGetComponent<Being>(out Being being))
        {
            being.Heal(healingValue);
        }
    }
}
