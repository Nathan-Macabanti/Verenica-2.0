using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Values")]
    [SerializeField] private uint startingAttackValue;
    [SerializeField] private uint maxAttackValue;
    #region Current Attack Value
    private uint currentAttackValue;
    public uint CurrentAttackValue { get { return currentAttackValue; } }
    #endregion
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        InitializeAttack();
        player = GetComponent<Player>();
    }

    public void InitializeAttack()
    {
        currentAttackValue = startingAttackValue;
        currentAttackValue = (uint)Mathf.Clamp(currentAttackValue, 0, maxAttackValue);
    }

    //Attack the target directly
    public void Attack(Being target)
    {
        //Debug.Log($"Attacking {target.name}");
        target.Damage(currentAttackValue);
    }

    //Hit something
    public void Hit()
    {
    }
}
