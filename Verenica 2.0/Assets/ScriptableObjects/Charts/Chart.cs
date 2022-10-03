using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chart", menuName = "ScriptableObjects/NewChart", order = 1)]
public class Chart : ScriptableObject
{
    [Tooltip("You have to include beat 0 as the first element and set all lanes to none")]
    public BeatInfo[] beats;
}

[System.Serializable]
public struct BeatInfo
{
    public float beat;
    [Space(10)]
    public NoteType lane_1;
    public NoteType lane_2;
    public NoteType lane_3;
}


