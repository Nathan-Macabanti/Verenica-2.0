using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WinState { win, lose, none }

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static GameManager GetInstance() { return instance; }
    #endregion


    #region Variables
    [Header("Manually set player")]
    #region Player
    [SerializeField] private Player _player;
    public Player Player { get { return _player; } }
    #endregion

    private WinState _winState = WinState.none;
    //private bool gameIsOver = false;

    #endregion

    private void Awake()
    {
        IntializeSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if(_player == null)
        {
            _player = GameObject.FindObjectOfType<Player>();
        }
        #if false
        GameObject temp;
        if (_player == null)
        {
            temp = GameObject.FindGameObjectWithTag(PlayerName);
            if (temp.TryGetComponent<Player>(out Player p))
            {
                _player = p;
            }
        }
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        //If game is not over, the winState is neither Win or Lose
        if (_winState == WinState.none)
        {
            //If incase of tie, Win takes priority
            Win();
            Lose();
        }
    }

#region Methods
#region WinAndLoseChecker
    public void Win()
    {
        //If there is no more Phases, and the current Enemy is dead
        if (PhaseManager.GetInstance().QueueIsEmpty() && EnemyGroup.GetInstance().CurrentEnemy.IsDead())
        {
            _winState = WinState.win;
            EventManager.InvokeGameOver(_winState);
            Debug.Log("You Win");
            //gameIsOver = true;
        }
    }

    public void Lose()
    {
        if (_player.IsDead())
        {
            _winState = WinState.lose;
            EventManager.InvokeGameOver(_winState);
            Debug.Log("You Lose");
            //gameIsOver = true;
        }
    }
#endregion
#region Setters
#region Player
    public void SetPlayer(Player player)
    {
        _player = player;
    }
#endregion
#endregion
#endregion

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public WinState GetWinState() { return _winState; }
}
