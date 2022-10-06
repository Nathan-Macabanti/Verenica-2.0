using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPhase", menuName = "ScriptableObjects/Phase", order = 1)]
public class Phase : ScriptableObject
{
    [Header("Enemy Info")]
    public Enemy enemy;

    [Header("SongInfo")]
    public SongInfo songInfo;

    [Header("Notes Prefab")]
    public DangerNote dangerNote;
    public SafeNote safeNote;
    public Note GimmickNote1;
    public Note GimmickNote2;
}
