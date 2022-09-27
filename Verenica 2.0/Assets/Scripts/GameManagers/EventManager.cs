using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static EventManager GetInstance() { return instance; }
    #endregion

    private void Awake()
    {
        IntializeSingleton();
    }

    #region EVENT UNITY ACTION
    public event UnityAction<float, int, NoteType> onNoteSpawn;
    public event UnityAction onLose;
    #endregion

    #region INVOKE FUNCTIONS
    public void NoteSpawn(float beat, int spawner, NoteType note) { onNoteSpawn?.Invoke(beat, spawner, note); }
    public void Lose() { onLose?.Invoke(); }
    #endregion
}
