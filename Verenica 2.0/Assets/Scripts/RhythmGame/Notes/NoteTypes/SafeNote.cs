using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNote : Note
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    //For movement
    enum MovementState { move, rebound }
    MovementState movementState;

    Enemy target;
    private void OnEnable()
    {
        movementState = MovementState.move;
        target = GameManager.GetInstance().GetEnemy();
        //CollisionPlayerToNote.OnSafeNoteHit += OnPlayerCollided;
    }

    private void OnDisable()
    {
        target = null;
        //CollisionPlayerToNote.OnSafeNoteHit -= OnPlayerCollided;
    }

    private void FixedUpdate()
    {
        if (movementState == MovementState.move)
            Move();
        else if(movementState == MovementState.rebound)
            Rebound();
    }

    private void Rebound()
    {
        //Make it hit the enemy on beat
        if (target == null) return;

        var step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    public override void OnPlayerCollided()
    {
        if (isDead) return;
        movementState = MovementState.rebound;
        //GameManager.GetInstance().GetPlayer().GetPlayerAttack().Attack(target);
    }

    public void OnEnemyCollided()
    {
        target.Damage(damage);
        ReturnToPool();
    }
}
