using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollider : MyCollider
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    protected override void OnCollision()
    {
        //Checks if a collider is intersect a certain poisition
        var point0 = transform.position + point0Offset;
        var point1 = transform.position + point0Offset;
        Collider[] collider = Physics.OverlapCapsule(point0, point1, radius);
        foreach (Collider col in collider)
        {
            Debug.Log(col.name);
            if(col.TryGetComponent<Note>(out Note note))
            {
                note.OnPlayerCollided();
            }
        }
    }
}
