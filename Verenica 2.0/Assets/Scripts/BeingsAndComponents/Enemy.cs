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

    public override void Damage(int damage)
    {
        base.Damage(damage);
        SoundManager.GetInstance().PlaySound(GameSFX.GetInstance().enemyHitSFX);
    }
    protected override void Die()
    {
        isDead = true;
        EventManager.InvokeEnemyDied();
    }

    public abstract void Gimmick();
}
