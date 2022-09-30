using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyCollider : MyCollider
{
    private Enemy enemy;

    private void Start()
    {
        if(enemy == null)
            enemy = GetComponent<Enemy>();
    }

    protected override void OnCollision()
    {
        //Checks if a collider is intersect a certain poisition
        var point0 = transform.position + point0Offset;
        var point1 = transform.position + point0Offset;
        Collider[] collider = Physics.OverlapCapsule(point0, point1, radius);
        foreach (Collider col in collider)
        {
            if (col.TryGetComponent<SafeNote>(out SafeNote note))
            {
                note.OnEnemyCollided();
            }
        }
    }
}
