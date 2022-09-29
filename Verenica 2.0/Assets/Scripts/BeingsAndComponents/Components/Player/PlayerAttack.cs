using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Values")]
    [SerializeField] private int startingAttackValue;
    [SerializeField] private int maxAttackValue;
    private int currentAttackValue;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        InitializeAttack();
        player = GetComponent<Player>();
    }

    public void InitializeAttack()
    {
        startingAttackValue = Mathf.Clamp(startingAttackValue, 0, maxAttackValue);
        currentAttackValue = startingAttackValue;
        currentAttackValue = Mathf.Clamp(currentAttackValue, 0, maxAttackValue);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(Being target)
    {
        Debug.Log($"Attacking {target.name}");
        target.Damage(currentAttackValue);
    }
}
