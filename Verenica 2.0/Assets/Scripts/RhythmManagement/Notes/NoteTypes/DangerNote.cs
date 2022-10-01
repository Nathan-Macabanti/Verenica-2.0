using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerNote : Note
{
    private void OnEnable()
    {
        //CollisionPlayerToNote.OnDangerNoteHit += OnPlayerCollided;
    }

    private void OnDisable()
    {
        //CollisionPlayerToNote.OnDangerNoteHit -= OnPlayerCollided;
    }

    public override void OnPlayerCollided()
    {
        if (isDead) return;
        Player player = GameManager.GetInstance().GetPlayer();
        GameManager.GetInstance().GetPlayer().Damage(1);
        ReturnToPool();
    }
}
