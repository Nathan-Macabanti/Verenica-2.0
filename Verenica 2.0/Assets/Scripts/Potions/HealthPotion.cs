using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPotion : Potion
{
    [SerializeField] private int healingValue = 1;

    public override bool UseMe(GameObject obj)
    {
        if(obj.TryGetComponent<Being>(out Being being))
        {
            return being.Heal(healingValue);
        }

        return false;
    }
}
