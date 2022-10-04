using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeNote : Note
{
    [SerializeField] private float speed;
    private bool _hasReachedLine;
    
    //For movement
    enum MovementState { move, rebound }
    MovementState movementState;

    private PlayerAttack _pAttack;
    
    Enemy target;
    private void OnEnable()
    {
        _hasReachedLine = false;
        movementState = MovementState.move;
        target = GameManager.GetInstance().GetEnemy();
        EventManager.OnGameIsOver += OnGameOver;
        //CollisionPlayerToNote.OnSafeNoteHit += OnPlayerCollided;
    }

    private void OnDisable()
    {
        target = null;
        EventManager.OnGameIsOver -= OnGameOver;
        //CollisionPlayerToNote.OnSafeNoteHit -= OnPlayerCollided;
    }

    private void FixedUpdate()
    {
        CheckMoveState();
    }

    private void CheckMoveState()
    {
        if (movementState == MovementState.move)
            Move();
        else if (movementState == MovementState.rebound)
            Rebound();
    }

    public override void Move()
    {
        if (path.source == null) return;
        if (path.destination == null) return;

        //Caluclate the distance and time you need to travel to the destination so that you will land on beat
        float songPosInBeats = _songManager.GetSongPositionInBeats();
        float beatOffset = _songManager.BeatOffset;
        float timeToDestinationInBeats = (beat - songPosInBeats);
        float distancePercent = (beatOffset - timeToDestinationInBeats) / beatOffset;

        //If off-screen
        if (distancePercent >= 1.5f)
        {
            ReturnToPool();
        }

        //Interpolate the Note
        Vector3 source = this.path.source.position;
        Vector3 destination = this.path.destination.position;

        if (distancePercent <= 1.0f)
        {
            //Interpolate to the beat line
            _transform.position = Vector3.Lerp(source, destination, distancePercent);
        }
        else
        {
            //Keep moving offscreen at the same velocity as going to the beat line
            float dDistanceX = destination.x + (destination.x - source.x);
            float dDistanceY = destination.y + (destination.y - source.y);
            float dDistanceZ = destination.z + (destination.z - source.z);
            Vector3 doubleDestination = new Vector3(dDistanceX, dDistanceY, dDistanceZ);
            _transform.position = Vector3.Lerp(destination, doubleDestination, distancePercent - 1.0f); //-1 to reset distancePercent to 0.0f

            //When the note touches the line once
            OnDestinationReached();
        }
    }

    private void OnDestinationReached()
    {
        //If the line has already entered dont continue
        if (_hasReachedLine) return;

        //Reset the Combo
        _comboSystem.ResetCollectedNote();

        //Declare you already reached the line
        _hasReachedLine = true;
    }

    private void Rebound()
    {
        //Make it hit the enemy on beat later
        if (target == null) return;

        var step = speed * Time.fixedDeltaTime;
        _transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    #region OnCollided
    public override void OnPlayerCollided()
    {
        if (isDead) return;
        movementState = MovementState.rebound;
        EventManager.InvokePlayerAttack();
        //GameManager.GetInstance().GetPlayer().GetPlayerAttack().Attack(target);
    }

    public void OnEnemyCollided()
    {
        PlayerAttack pAttack = GameManager.GetInstance().GetPlayer().GetPlayerAttack();
        target.Damage(pAttack.CurrentAttackValue * (int)ComboSystem.GetInstance().Multiplier);
        ReturnToPool();
    }
    #endregion
}
