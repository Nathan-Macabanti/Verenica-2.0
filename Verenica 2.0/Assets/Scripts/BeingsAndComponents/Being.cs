using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being : MonoBehaviour
{
    [SerializeField] protected int startingHP = 3;
    [SerializeField] protected int maxHP = 3;
    protected int currentHP;
    protected bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        InitializeHP();
        isDead = false;
    }

    public void InitializeHP()
    {
        if (startingHP <= currentHP)
        {
            currentHP = startingHP;
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        }
    }

    public virtual void Damage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        //If your HP is 0 die
        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    //Adds HP
    public void Heal(int heal)
    {
        currentHP += heal;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void SetHealth(int currHealth, int maxHealth)
    {
        currentHP = currHealth;
        maxHP = maxHealth;
    }
    public bool IsDead() { return isDead; }
}
