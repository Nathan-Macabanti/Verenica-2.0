using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Being
{
    protected override void Die()
    {
        EventManager.InvokeEnemyDied();
    }

    public abstract void Gimmick();
}
