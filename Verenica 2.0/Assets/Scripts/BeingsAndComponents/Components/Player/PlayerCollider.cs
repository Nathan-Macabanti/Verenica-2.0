using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollider : MonoBehaviour
{
    [Header("Overlap Capsule Values")]
    [SerializeField] private Vector3 point0Offset;
    [SerializeField] private Vector3 point1Offset;
    [SerializeField] private float radius;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        player.GetPlayerMovement().OnPlayerMove += OnMove;
    }

    private void OnDisable()
    {
        player.GetPlayerMovement().OnPlayerMove -= OnMove;
    }

    public void OnMove()
    {
        //Checks if a collider is intersect a certain poisition
        var point0 = transform.position + point0Offset;
        var point1 = transform.position + point1Offset;
        Collider[] collider = Physics.OverlapCapsule(point0, point1, radius);
    }

    //Draws area of trigger
    private void OnDrawGizmos()
    {
        var point0 = transform.position + point0Offset;
        var point1 = transform.position + point1Offset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point0, radius);
        Gizmos.DrawWireSphere(point1, radius);
    }
}
