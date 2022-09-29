using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionPlayerToNote
{
    public delegate void NoteHit(NoteType note);
    public static event NoteHit OnNoteHit;
    public static void InvokeOnNoteHit(NoteType note)
    {
        OnNoteHit?.Invoke(note);
    }

    public delegate void DangerNoteHit();
    public static event DangerNoteHit OnDangerNoteHit;
    public static void InvokeOnDangerNoteHit()
    {
        OnDangerNoteHit?.Invoke();
    }

    public delegate void SafeNoteHit();
    public static event SafeNoteHit OnSafeNoteHit;
    public static void InvokeOnSafeNoteHit()
    {
        OnSafeNoteHit?.Invoke();
    }
}
