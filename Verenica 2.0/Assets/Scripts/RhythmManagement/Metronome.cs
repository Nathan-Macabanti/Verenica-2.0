using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SongManager))]
public class Metronome : MonoBehaviour
{
    #region Singleton
    private static Metronome instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static Metronome GetInstance() { return instance; }
    #endregion

    private void Awake()
    {
        IntializeSingleton();
    }

    #region Variables
    private float _bpm;
    private float _beatInterval, _beatTimer;

    #region BeatFull
    private static bool _beatFull;
    public static bool GetBeatFull() { return _beatFull; }
    #endregion

    #region BeatCountFull
    private static int _beatCountFull;
    public static int GetBeatCountFull() { return _beatCountFull; }
    #endregion
    #endregion

    // Update is called once per frame
    void Update()
    {
    }

    

}
