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
    [SerializeField] private SongInfo songInfo;
    private float songPosition;
    private float songPositionInBeats;
    private float secondsPerBeat;
    private float dspTime;
    private Radio radio;

    private int nextIndex;
    private Chart chartCopy;
    
    public float beatOffset;
    public float BPM;
    private bool _beatFull;
    private float _beatTimer;
    private int _beatCountFull;

    private bool isOn = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitializeTiming();
        InitializeAudio();
    }

    private void InitializeTiming()
    {
        this.BPM = songInfo.BPM;
        //this.chartCopy = PhaseManager.GetPhase().SongInfo.chart;
        this.chartCopy = songInfo.chart; //erase if PhaseManager exists
        secondsPerBeat = 60.0f / BPM;
        this.dspTime = (float)AudioSettings.dspTime; //Must be called when the song starts
        nextIndex = 0;
    }

    private void InitializeAudio()
    {
        if(radio == null) { radio = this.GetComponent<Radio>(); }
        radio.SetClip(songInfo.clip);
        radio.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isOn) return;

        CalculateBeat();
        CheckIfCanSpawn();
        //BeatDetection();
    }

    private void CalculateBeat()
    {
        songPosition = (float)AudioSettings.dspTime - this.dspTime;
        songPositionInBeats = (float)songPosition / (float)secondsPerBeat;
    }

    private void CheckIfCanSpawn()
    {
        if (nextIndex >= chartCopy.beats.Length) return; //IF THE INDEX IS GREATER THAN THE AMOUNT IN THE CHART or LESS THAN 0
        
        //SPAWN 
        float spawnTime = songPositionInBeats + beatOffset;
        float currentBeat = CurrentBeatInfo().beat;
        if (currentBeat < spawnTime)
        {
            CallSpawnEvent(currentBeat);
            nextIndex++;
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
        //full beat count
        _beatFull = false;
        _beatTimer += Time.deltaTime;
        if (_beatTimer >= secondsPerBeat)
        {
            _beatTimer -= secondsPerBeat;
            _beatFull = true;
            _beatCountFull++;
            Debug.Log("doogs");
        }
    }

    private void OnEnable()
    {
        EventManager.OnGameIsOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.OnGameIsOver -= OnGameOver;
    }

    public void OnGameOver(WinState winState)
    {
        radio.Stop();
        isOn = false;
    }

    public float GetSongPositionInBeats() { return songPositionInBeats; }
    public BeatInfo CurrentBeatInfo() { return chartCopy.beats[nextIndex]; }
}
