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

    private int index;
    private Chart chartCopy;
    
    public float beatOffset;
    public float BPM;
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
        index = 0;
    }

    private void InitializeAudio()
    {
        if(radio == null) { radio = this.GetComponent<Radio>(); }
        radio.SetClip(songInfo.clip);
    }
    
    // Update is called once per frame
    void Update()
    {
        radio.Play();
        CalculateBeat();
        CheckIfCanSpawn();
    }

    private void CalculateBeat()
    {
        songPosition = (float)AudioSettings.dspTime - this.dspTime;
        songPositionInBeats = (float)songPosition / (float)secondsPerBeat;
    }

    private void CheckIfCanSpawn()
    {
        if (index >= chartCopy.beats.Length) return; //IF THE INDEX IS LESS THAN THE AMOUNT IN THE CHART
        
        //SPAWN 
        float spawnTime = songPositionInBeats + beatOffset;
        float currentBeat = CurrentBeatInfo().beat;
        if (currentBeat < spawnTime)
        {
            CallSpawnEvent(currentBeat);
            index++;
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

    public float GetSongPositionInBeats() { return songPositionInBeats; }
    public BeatInfo CurrentBeatInfo() { return chartCopy.beats[index]; }
}
