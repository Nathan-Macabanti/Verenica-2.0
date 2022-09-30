using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyCollider : MonoBehaviour
{
    [Header("Overlap Capsule Values")]
    [SerializeField] protected Vector3 point0Offset;
    [SerializeField] protected Vector3 point1Offset;
    [SerializeField] protected float radius = 0.01f;

    [Header("Gizmos")]
    [SerializeField] Color colliderColor;

    // Update is called once per frame
    void FixedUpdate()
    {
        OnCollision();
    }

    protected abstract void OnCollision();

    //Draws area of trigger
    private void OnDrawGizmos()
    {
        var point0 = transform.position + point0Offset;
        var point1 = transform.position + point1Offset;

        Gizmos.color = colliderColor;
        Gizmos.DrawWireSphere(point0, radius);
        Gizmos.DrawWireSphere(point1, radius);
    }
}
