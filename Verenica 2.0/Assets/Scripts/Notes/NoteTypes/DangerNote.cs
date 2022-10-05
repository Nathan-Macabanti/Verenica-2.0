using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerNote : Note
{
    public override void OnPlayerCollided()
    {
        if (isDead) return;
        Player player = GameManager.GetInstance().GetPlayer();
        GameManager.GetInstance().GetPlayer().Damage(1);
        ReturnToPool();
    }
}
