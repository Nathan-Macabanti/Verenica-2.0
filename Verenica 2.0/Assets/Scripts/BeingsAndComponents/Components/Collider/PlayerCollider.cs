using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Player))]
public class PlayerCollider : MonoBehaviour //MyCollider
{
    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
    }

    //Uses rigidbody but this makes sure that only one collision is happening
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Note>(out Note note))
        {
            note.OnPlayerCollided();
            if(note.TryGetComponent<SafeNote>(out SafeNote safeNote))
            {
                player.playerAttack.Hit();
            }
        }
    }
}
