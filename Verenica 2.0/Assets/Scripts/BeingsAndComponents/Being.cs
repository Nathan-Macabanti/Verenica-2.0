using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private int startingHP = 3;
    [SerializeField] protected int maxHP = 3;
    protected int currentHP;
    public int GetCurrentHP() { return currentHP; }

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
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        //Debug.Log(gameObject.name + " " + startingHP.ToString());
    }

    public virtual void Damage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
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
