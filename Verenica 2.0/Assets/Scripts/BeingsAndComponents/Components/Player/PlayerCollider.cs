using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollider : MonoBehaviour
{
    [Header("Overlap Capsule Values")]
    [SerializeField] private Vector3 centerOffset;
    [SerializeField] private float radius = 0.01f;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnPlayerCollision();
    }

    private void OnPlayerCollision()
    {
        //Checks if a collider is intersect a certain poisition
        var center = transform.position + centerOffset;
        Collider[] collider = Physics.OverlapSphere(center, radius);
        foreach (Collider col in collider)
        {
            Debug.Log(col.name);
            if(col.TryGetComponent<Note>(out Note note))
            {
                note.OnPlayerCollided();
            }
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    //Draws area of trigger
    private void OnDrawGizmos()
    {
        var center = transform.position + centerOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
