using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerNote : Note
{
    public override void OnPlayerCollided()
    {
        if (isDead) return;
        Player player = _gameManger.Player;
        player.Damage(1);
        ReturnToPool();
    }
}
