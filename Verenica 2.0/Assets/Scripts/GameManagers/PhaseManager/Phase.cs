using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPhase", menuName = "ScriptableObjects/Phase", order = 1)]
public class Phase : ScriptableObject
{
    public Enemy enemy;
    public SongInfo songInfo;
}
