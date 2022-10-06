using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyCollider))]
public abstract class Enemy : Being
{
    void Start()
    {
        InitializeHP();
    }

    protected override void Die()
    {
        isDead = true;
        EventManager.InvokeEnemyDied();
    }

    public abstract void Gimmick();
}
