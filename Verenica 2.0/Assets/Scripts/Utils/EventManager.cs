using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{

    #region EVENT UNITY ACTION
    public static event UnityAction<float, int, NoteType> onNoteSpawn;
    public static event UnityAction onLose;
    #endregion

    #region INVOKE FUNCTIONS
    public static void NoteSpawn(float beat, int spawner, NoteType note) { onNoteSpawn?.Invoke(beat, spawner, note); }
    public static void Lose() { onLose?.Invoke(); }
    #endregion
}
