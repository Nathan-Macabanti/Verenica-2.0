using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNote : Note
{
    private void OnEnable()
    {
        //CollisionPlayerToNote.OnSafeNoteHit += OnPlayerCollided;
    }

    private void OnDisable()
    {
        //CollisionPlayerToNote.OnSafeNoteHit -= OnPlayerCollided;
    }

    public override void OnPlayerCollided()
    {
        if (isDead) return;
        Being target = GameManager.GetInstance().GetEnemy();
        GameManager.GetInstance().GetPlayer().GetPlayerAttack().Attack(target);
        ReturnToPool();
    }
}
