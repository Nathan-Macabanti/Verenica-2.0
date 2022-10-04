using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Radio))]
public class SongManager : MonoBehaviour
{
    #region Singleton
    private static SongManager instance;
    private void IntializeSingleton() 
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static SongManager GetInstance() { return instance; }
    #endregion

    private void Awake()
    {
        IntializeSingleton();
    }

    #region Variables
    [SerializeField] private SongInfo _songInfo;
    private float _songPosition;
    private float _songPositionInBeats;
    private float _secondsPerBeat;
    private float _dspTime;
    private Radio _radio;

    private int _nextIndex;
    private Chart _chartCopy;
    
    public float BeatOffset;
    [HideInInspector] public float BPM;

    private bool _isOn = true;

    private float _beatTimer;
    private float _dspTimeForBeat;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitializeAll();
    }

    #region Initializers
    private void InitializeAll()
    {
        InitializeTiming();
        InitializeAudio();
    }
    private void InitializeTiming()
    {
        BPM = _songInfo.BPM;
        //this.chartCopy = PhaseManager.GetPhase().SongInfo.chart;
        _chartCopy = _songInfo.chart; //erase if PhaseManager exists
        _secondsPerBeat = 60.0f / BPM;
        _dspTime = (float)AudioSettings.dspTime; //Must be called when the song starts
        _dspTimeForBeat = (float)AudioSettings.dspTime;
        _nextIndex = 0;
    }

    private void InitializeAudio()
    {
        if(_radio == null) { _radio = this.GetComponent<Radio>(); }
        _radio.SetClip(_songInfo.clip);
        _radio.Play();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (!_isOn) return;

        CalculateBeat();
        CheckIfCanSpawn();
        BeatDetection();
    }

    #region Rhythm Calculation Methods
    private void CalculateBeat()
    {
        _songPosition = (float)AudioSettings.dspTime - _dspTime;
        _songPositionInBeats = _songPosition / _secondsPerBeat;
        //full beat count
        _beatTimer = (float)AudioSettings.dspTime - _dspTimeForBeat;
    }

    private void CheckIfCanSpawn()
    {
        if (_nextIndex >= _chartCopy.beats.Length) return; //IF THE INDEX IS GREATER THAN THE AMOUNT IN THE CHART or LESS THAN 0
        
        //SPAWN 
        float spawnTime = _songPositionInBeats + BeatOffset;
        float currentBeat = CurrentBeatInfo().beat;
        if (currentBeat < spawnTime)
        {
            CallSpawnEvent(currentBeat);
            _nextIndex++;
        }
    }

    private void CallSpawnEvent(float beat)
    {
        if (CurrentBeatInfo().lane_1 != NoteType.None)
        {
            EventManager.InvokeNoteSpawn(beat, 0, CurrentBeatInfo().lane_1);
        }
        if (CurrentBeatInfo().lane_2 != NoteType.None)
        {
            EventManager.InvokeNoteSpawn(beat, 1, CurrentBeatInfo().lane_2);
        }
        if (CurrentBeatInfo().lane_3 != NoteType.None)
        {
            EventManager.InvokeNoteSpawn(beat, 2, CurrentBeatInfo().lane_3);
        }
    }

    private void BeatDetection()
    {
        if (_beatTimer >= _secondsPerBeat)
        {
            _dspTimeForBeat = (float)AudioSettings.dspTime;
            EventManager.InvokeBeat();
        }
    }
    #endregion

    private void OnEnable()
    {
        EventManager.OnPhaseChange += ChangeSong;
        EventManager.OnGameIsOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.OnPhaseChange -= ChangeSong;
        EventManager.OnGameIsOver -= OnGameOver;
    }

    #region Events
    public void OnGameOver(WinState winState)
    {
        _radio.Stop();
        _isOn = false;
    }
    #endregion

    #region ChangeSong
    public void ChangeSong(SongInfo songInfo)
    {
        _songInfo = songInfo;
        InitializeAll();
    }

    public void ChangeSong(Phase phase)
    {
        _songInfo = phase.songInfo;
        InitializeAll();
    }
    #endregion

    public float GetSongPositionInBeats() { return _songPositionInBeats; }
    public BeatInfo CurrentBeatInfo() { return _chartCopy.beats[_nextIndex]; }
}
