using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chart", menuName = "ScriptableObjects/NewChart", order = 1)]
public class Chart : ScriptableObject
{
    public BeatInfo[] beats;
}

[System.Serializable]
public struct BeatInfo
{
    public float beat;
    public NoteType lane_1;
    public NoteType lane_2;
    public NoteType lane_3;
}


