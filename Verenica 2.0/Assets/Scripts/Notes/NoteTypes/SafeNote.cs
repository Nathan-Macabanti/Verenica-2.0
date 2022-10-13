using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNote : Note
{
    private float speed = 60.0f;
    
    //For movement
    protected enum MovementState { move, rebound }
    protected MovementState movementState;

    protected PlayerAttack _pAttack;

    protected Enemy _target;

    private void Start()
    {
        _transform = transform;
        _songManager = SongManager.GetInstance();
        _comboSystem = ComboSystem.GetInstance();
        _gameManger = GameManager.GetInstance();
    }

    private void OnEnable()
    {
        movementState = MovementState.move;
        EnemyGroup _enemyGroup = EnemyGroup.GetInstance();
        _target = _enemyGroup.CurrentEnemy;
        EventManager.OnGameIsOver += OnGameOver;
        EventManager.OnEnemyDied += ReturnToPool;
    }

    private void OnDisable()
    {
        _target = null;
        if(movementState == MovementState.move)
        {
            _comboSystem.LoseNote();
        }
        EventManager.OnGameIsOver -= OnGameOver;
        EventManager.OnEnemyDied -= ReturnToPool;
    }

    private void FixedUpdate()
    {
        CheckMoveState();
    }

    protected void CheckMoveState()
    {
        if (movementState == MovementState.move)
            Move();
        else if (movementState == MovementState.rebound)
            Rebound();
    }

    protected void Rebound()
    {
        //Make it hit the enemy on beat later
        if (_target == null) return;

        EnemyGroup _enemyGroup = EnemyGroup.GetInstance();
        var step = speed * Time.fixedDeltaTime;
        _transform.position = Vector3.MoveTowards(transform.position, _enemyGroup.transform.position, step);
    }

    #region OnCollided
    public override void OnPlayerCollided()
    {
        if (isDead) return;
        movementState = MovementState.rebound;
        EventManager.InvokePlayerAttack();
        SoundManager.GetInstance().PlaySound(GameSFX.GetInstance().noteHitSFX);
        //GameManager.GetInstance().GetPlayer().GetPlayerAttack().Attack(target);
    }

    public void OnEnemyCollided()
    {
        PlayerAttack pAttack = _gameManger.Player.playerAttack;
        _target.Damage(pAttack.CurrentAttackValue * (int)ComboSystem.GetInstance().Multiplier);
        ReturnToPool();
    }
    #endregion
}
