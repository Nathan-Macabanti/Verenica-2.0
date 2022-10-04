using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    #region Singleton
    private static PhaseManager instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static PhaseManager GetInstance() { return instance; }
    #endregion

    [SerializeField] private Phase[] _phases;
    [SerializeField] private int _startingIndex = 0;

    private Queue<Phase> phaseQueue = new Queue<Phase>(); //To make the process faster
    private int _index = 0;
    public Phase CurrentPhase { get { return _phases[_index]; } }

    private SongManager songManager;
    private GameManager gameManager; 

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        songManager = SongManager.GetInstance();
        gameManager = GameManager.GetInstance();
    }

    #region Initialization
    void Initialize()
    {
        InitializePhases();
    }

    private void InitializePhases()
    {
        _index = _startingIndex;
        int count = _phases.Length;
        for (int i = _startingIndex; i < count; i++)
        {
            phaseQueue.Enqueue(_phases[i]);
        }

        NextPhase();
    }
    #endregion

    #region CHANGING PHASES
    public void NextPhase()
    {
        if (phaseQueue.Count == 0) return;

        Phase phase = phaseQueue.Dequeue();
        EventManager.InvokePhaseChange(phase);
    }

    public void NextPhase(int index)
    {
        int _index = index;
        Phase phase = CurrentPhase;
        EventManager.InvokePhaseChange(phase);
    }
    #endregion
}
