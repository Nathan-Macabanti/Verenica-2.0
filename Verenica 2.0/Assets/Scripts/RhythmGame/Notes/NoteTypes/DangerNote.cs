using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerNote : Note
{
    private void OnEnable()
    {
        CollisionPlayerToNote.OnDangerNoteHit += OnPlayerCollided;
    }

    private void OnDisable()
    {
        CollisionPlayerToNote.OnDangerNoteHit -= OnPlayerCollided;
    }

    public override void OnPlayerCollided()
    {
    }
}
