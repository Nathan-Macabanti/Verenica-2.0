using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Values")]
    [SerializeField] private int startingAttackValue;
    private int maxAttackValue;
    private int currentAttackValue;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(Being being)
    {
        being.Damage(currentAttackValue);
    }
}
