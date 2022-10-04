using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region GAME EVENTS
        #region GAME IS OVER EVENTS
            #region GAME IS OVER
                public delegate void GameIsOver(WinState winstate);
                public static event GameIsOver OnGameIsOver;
                public static void InvokeGameOver(WinState winstate)
                {
                    OnGameIsOver?.Invoke(winstate);
                }
            #endregion
            #region LOSE EVENT
                    public delegate void Lose();
                    public static event Lose OnLose;
                    public static void InvokeLose()
                    {
                        OnLose?.Invoke();
                    }
            #endregion
            #region WIN EVENT
                public delegate void Win();
                public static event Win OnWin;
                public static void InvokeWin()
                {
                    OnWin?.Invoke();
                }
    #endregion
    #endregion
        #region CHANGE PHASE
        public delegate void PhaseChange(Phase phase);
        public static event PhaseChange OnPhaseChange;
        public static void InvokePhaseChange(Phase phase)
        {
            OnPhaseChange?.Invoke(phase);
        }
        #endregion

    #endregion

    #region RHYTHM EVENTS
    #region NOTE SPAWN EVENT
    public delegate void Beat();
    public static event Beat OnBeat;
    public static void InvokeBeat()
    {
        OnBeat?.Invoke();
    }
    #endregion

    #region NOTE SPAWN EVENT
    public delegate void NoteSpawn(float beat, int index, NoteType noteType);
    public static event NoteSpawn OnNoteSpawn;
    public static void InvokeNoteSpawn(float beat, int index, NoteType noteType) 
    {
        OnNoteSpawn?.Invoke(beat, index, noteType);
    }
    #endregion
    #endregion

    #region ENEMY DEATH EVENT
    public delegate void EnemyDied();
    public static event EnemyDied OnEnemyDied;
    public static void InvokeEnemyDied()
    {
        OnEnemyDied?.Invoke();
    }
    #endregion

    #region PLAYER EVENTS
        #region PLAYER DAMAGED EVENT
        public delegate void PlayerDamaged();
        public static event PlayerDamaged OnPlayerDamaged;
        public static void InvokePlayerDamaged()
        {
            OnPlayerDamaged?.Invoke();
        }
        #endregion
        #region PLAYER ATTACK EVENT
        public delegate void PlayerAttack();
        public static event PlayerAttack OnPlayerAttack;
        public static void InvokePlayerAttack()
        {
        OnPlayerAttack?.Invoke();
        }
        #endregion
    #endregion
}
