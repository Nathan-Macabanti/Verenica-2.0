using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
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

    #region NOTE SPAWN EVENT
    public delegate void NoteSpawn(float beat, int index, NoteType noteType);
    public static event NoteSpawn OnNoteSpawn;
    public static void InvokeNoteSpawn(float beat, int index, NoteType noteType) 
    {
        OnNoteSpawn?.Invoke(beat, index, noteType);
    }
    #endregion

    #region ENEMY DEATH EVENT
    public delegate void EnemyDied();
    public static event EnemyDied OnEnemyDied;
    public static void InvokeEnemyDied()
    {
        OnEnemyDied?.Invoke();
    }
    #endregion

    #region UNITY EVENTS
#if false
    #region EVENT UNITY ACTION
    public static event UnityAction<float, int, NoteType> onNoteSpawn;
    public static event UnityAction onLose;
    #endregion

    #region INVOKE FUNCTIONS
    public static void NoteSpawn(float beat, int spawner, NoteType note) { onNoteSpawn?.Invoke(beat, spawner, note); }
    public static void Lose() { onLose?.Invoke(); }
    #endregion
#endif
    #endregion
}
