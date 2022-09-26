using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongInfo", menuName = "ScriptableObjects/NewSongInfo", order = 1)]
public class SongInfo : ScriptableObject
{
    public float BPM;
    public AudioClip clip;
    public Chart chart;
}
