using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private uint startingHP = 3;
    [SerializeField] protected uint maxHP = 3;
    protected uint currentHP;
    public uint GetCurrentHP() { return currentHP; }

    protected bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        InitializeHP();
        isDead = false;
    }

    protected virtual void InitializeHP()
    {
        currentHP = startingHP;
        currentHP = (uint)Mathf.Clamp(currentHP, 0, maxHP);
        //Debug.Log(gameObject.name + " " + startingHP.ToString());
    }

    public virtual void Damage(uint damage)
    {
        currentHP -= damage;
        currentHP = (uint)Mathf.Clamp(currentHP, 0, maxHP);
        //Debug.Log(gameObject.name + ": " + currentHP);
        //If your HP is 0 die
        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    //Adds HP
    public void Heal(uint heal)
    {
        currentHP += heal;
        currentHP = (uint)Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void SetHealth(uint currHealth, uint maxHealth)
    {
        currentHP = currHealth;
        maxHP = maxHealth;
    }
    public bool IsDead() { return isDead; }
}
