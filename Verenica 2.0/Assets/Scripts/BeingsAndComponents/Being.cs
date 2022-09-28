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
        currentHP = startingHP;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        isDead = false;
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

    public bool IsDead() { return isDead; }
}
