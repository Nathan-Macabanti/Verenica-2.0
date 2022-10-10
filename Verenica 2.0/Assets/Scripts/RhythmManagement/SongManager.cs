using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Radio))]
public class SongManager : MonoBehaviour
{
    #region Singleton
    private static SongManager instance;
    private void InitializeSingleton() 
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static SongManager GetInstance() { return instance; }
    #endregion

    private void Awake()
    {
        InitializeSingleton();
        if (_radio == null) { _radio = this.GetComponent<Radio>(); }
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
    private bool _surival = false;
    private float _beatTimer;
    private float _dspTimeForBeat;
    #endregion

    #region Initializers
    private void InitializeAll()
    {
        InitializeTiming();
        InitializeAudio();
    }
    private void InitializeTiming()
    {
        //Reset song Position
        _songPosition = 0;
        _songPositionInBeats = 0;

        //Calculate songPos variables
        BPM = _songInfo.BPM;
        _secondsPerBeat = 60.0f / BPM;
        _dspTime = (float)AudioSettings.dspTime; //Must be called when the song starts
        _dspTimeForBeat = (float)AudioSettings.dspTime;
        _nextIndex = 0;
    }

    private void InitializeAudio()
    {
        _radio.SetClip(_songInfo.clip);
        _radio.Play();
        float beats = _songInfo.clip.length / _secondsPerBeat;
        Debug.Log($"Beats: {beats}");
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
        //IF THE INDEX IS GREATER THAN THE AMOUNT IN THE CHART or LESS THAN 0
        if (_nextIndex >= _chartCopy.beats.Length) 
        {
            //If the radio is not playing anything
            if (!_radio.audioSource.isPlaying)
            {
                if (_surival)
                {
                    EventManager.InvokeSongFinished();
                }
                else
                {
                    ResetChart();
                }
            }
            return;
        }     
        
        //SPAWN 
        float spawnTime = _songPositionInBeats + BeatOffset;
        float currentBeat = CurrentBeatInfo().beat;
        if (currentBeat < spawnTime)
        {
            CallSpawnEvent(currentBeat);
            _nextIndex++;
        }
    }

    private void ResetChart()
    {
        _nextIndex = 0;
        _dspTime = (float)AudioSettings.dspTime;
        _beatTimer = (float)AudioSettings.dspTime - _dspTimeForBeat;
        _radio.Play();
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
        EventManager.OnGameStateChanged += OnGameStateChange;
    }
    private void OnDisable()
    {
        EventManager.OnPhaseChange -= ChangeSong;
        EventManager.OnGameIsOver -= OnGameOver;
        EventManager.OnGameStateChanged -= OnGameStateChange;
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
        _radio.Stop();
        _songInfo = songInfo;
        _chartCopy = _songInfo.chart;
        InitializeAll();
    }

    public void ChangeSong(Phase phase)
    {
        _radio.Stop();
        _songInfo = phase.songInfo;
        BeatOffset = phase.beatOffset;
        _surival = phase.survivalMode;
        _chartCopy = _songInfo.chart;
        InitializeAll();
    }
    #endregion

    public void OnGameStateChange(GameState state)
    {
        if (state == GameState.playing) 
        {
            //https://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html
            _dspTime = (float)AudioSettings.dspTime - _songPosition; //Resume to the songPosition
            //_dspTimeForBeat = (float)AudioSettings.dspTime;
            _isOn = true;
            _radio.Play();
            return;
        }

        _isOn = false;
        _radio.Pause();
    }

    public float GetSongPositionInBeats() { return _songPositionInBeats; }
    public BeatInfo CurrentBeatInfo() { return _chartCopy.beats[_nextIndex]; }
}
