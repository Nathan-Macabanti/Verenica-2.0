using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Enemy))]
public class EnemyCollider : MonoBehaviour //MyCollider just incase this also does 2 collisions
{
    #region MyCollider
#if false
    //private Enemy enemy;

    private void Start()
    {
        _transform = transform;
        //if (enemy == null)
        //    enemy = GetComponent<Enemy>();
    }


    protected override void OnOverlap()
    {
        //Checks if a collider is intersect a certain poisition
        var point0 = _transform.TransformPoint(point0Offset); //positionVector + pointOffsetVector 
        var point1 = _transform.TransformPoint(point1Offset);
        Collider[] collider = Physics.OverlapCapsule(point0, point1, radius);
        foreach (Collider col in collider)
        {
            if (col.TryGetComponent<SafeNote>(out SafeNote note))
            {
                note.OnEnemyCollided();
            }
        }
    }
#endif
    #endregion

    private void OnEnable()
    {
        InitalizeRequiredComponents();
    }

    private void InitalizeRequiredComponents()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    //Uses rigidbody but this makes sure that only one collision is happening
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SafeNote>(out SafeNote note))
        {
            note.OnEnemyCollided();
        }
    }
}
