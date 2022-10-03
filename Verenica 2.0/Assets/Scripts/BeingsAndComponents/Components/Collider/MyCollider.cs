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

    //Cache for optimization
    protected Transform _transform;

    

    // Update is called once per frame
    void Update()
    {
        OnOverlap();
    }

    protected abstract void OnOverlap();

    //Draws area of trigger
    private void OnDrawGizmos()
    {
        var point0 = transform.TransformPoint(point0Offset); //positionVector + pointOffsetVector
        var point1 = transform.TransformPoint(point1Offset);

        Gizmos.color = colliderColor;
        Gizmos.DrawWireSphere(point0, radius);
        Gizmos.DrawWireSphere(point1, radius);
    }
}
