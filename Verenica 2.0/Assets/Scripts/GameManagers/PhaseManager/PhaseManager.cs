using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    #region Singleton
    private static PhaseManager instance;
    private void InitializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static PhaseManager GetInstance() { return instance; }
    #endregion

    [SerializeField] private Phase[] _phases;
    public int _startingIndex = 0;
    public Phase[] PhaseArray { get { return _phases; } }
    public int _index { get; private set; }
    private Queue<Phase> phaseQueue = new Queue<Phase>(); //To make the process faster
    public Phase CurrentPhase { get { return _phases[_index]; } }

    public bool isOnTheFirstPhase { get; private set; } = true;

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        InitializeSingleton();
        isOnTheFirstPhase = true;
    }

    private void Start()
    {
        NextPhase();
    }

    #region Initialization
    void Initialize()
    {
        InitializePhases();
    }

    private void InitializePhases()
    {
        _index = _startingIndex;
        int count = _phases.Length - _index;
        for (int i = _startingIndex; i < count; i++)
        {
            phaseQueue.Enqueue(_phases[i]);
        }
    }
    #endregion

    #region CHANGING PHASES
    public void NextPhase()
    {
        if (phaseQueue.Count == 0) return;

        _index++;
        Phase phase = phaseQueue.Dequeue();
        EventManager.InvokePhaseChange(phase);
        isOnTheFirstPhase = false;
    }

    public void NextPhase(int index)
    {
        _index = index;
        int count = _phases.Length - _index;
        for (int i = _startingIndex; i < count; i++)
        {
            phaseQueue.Enqueue(_phases[i]);
        }
        Phase phase = phaseQueue.Dequeue();
        
        EventManager.InvokePhaseChange(phase);
        isOnTheFirstPhase = false;
    }
    #endregion

    public bool QueueIsEmpty()
    {
        if (phaseQueue.Count == 0)
        {
            return true;
        }

        return false;
    }

    private void OnEnable()
    {
        EventManager.OnEnemyDied += NextPhase;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDied -= NextPhase;
    }
}
