using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Values")]
    [SerializeField] private int startingAttackValue;
    [SerializeField] private int maxAttackValue;
    #region Current Attack Value
    private int currentAttackValue;
    public int CurrentAttackValue { get { return currentAttackValue; } }
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
        currentAttackValue = Mathf.Clamp(currentAttackValue, 0, maxAttackValue);
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
