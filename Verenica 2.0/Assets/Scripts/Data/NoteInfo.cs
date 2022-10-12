using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NoteInfo
{
    public NoteType type;
}

public enum NoteType { None = 0, Danger = 1, Safe = 2, Jump = 3, Gimmick1 = 4, Gimmick2 = 5 }