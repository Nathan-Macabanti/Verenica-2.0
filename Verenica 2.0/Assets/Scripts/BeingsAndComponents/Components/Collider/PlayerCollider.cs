using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Player))]
public class PlayerCollider : MonoBehaviour //MyCollider
{
    //This isn't working for some reason
    #region MyCollider
    #if false
    //Non-alloc collisions to lessen garbage, it is slower though
    protected Collider[] _colliders = new Collider[1];
    //private Player player;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        //player = GetComponent<Player>();
    }

    //For some reason sometimes it would overlap twice so I'll fix this later
    protected override void OnOverlap()
    {
        #if false
        //Checks if a collider is intersect a certain poisition
        var point0 = _transform.TransformPoint(point0Offset); //positionVector + pointOffsetVector
        var point1 = _transform.TransformPoint(point1Offset);
        Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius);

        if (colliders.Length <= 0) return;

        foreach (Collider col in colliders)
        {
            //Debug.Log(col.name + " " +Time.time.ToString());
            if(col.TryGetComponent<Note>(out Note note))
            {
                note.OnPlayerCollided();
            }
        }

        //Debug.Log(collider.Length);
        #endif
    }
    #endif
    #endregion

    //Uses rigidbody but this makes sure that only one collision is happening
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Note>(out Note note))
        {
            note.OnPlayerCollided();
        }
    }
}
