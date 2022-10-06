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
    [SerializeField] private int _startingIndex = 0;
    public Phase[] PhaseArray { get { return _phases; } }

    private Queue<Phase> phaseQueue = new Queue<Phase>(); //To make the process faster
    private int _index = 0;
    public Phase CurrentPhase { get { return _phases[_index]; } }

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        InitializeSingleton();
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
        int count = _phases.Length;
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
