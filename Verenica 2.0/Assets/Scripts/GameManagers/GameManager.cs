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
    private WinState _winState = WinState.none;
    private bool gameIsOver = false;
    #region Player
    [SerializeField] private Player _player;
    public Player GetPlayer() { return _player; }
    #endregion
    #region Enemy
    [SerializeField] private Enemy _enemy;
    public Enemy GetEnemy() { return _enemy; }
    #endregion
    #endregion

    private void Awake()
    {
        IntializeSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(_player == null)
            _player = FindObjectOfType<Player>();
        if (_enemy == null) //Remove once PhaseManager exists
            _enemy = FindObjectOfType<Enemy>();

        //_enemy = will be called in the PhaseManager
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsOver)
        {
            Win();
            Lose();
        }
    }

    public void Win()
    {
        if (_enemy.IsDead())
        {
            _winState = WinState.win;
            gameIsOver = true;
            EventManager.InvokeWin();
        }
    }

    public void Lose()
    {
        if (_player.IsDead())
        {
            _winState = WinState.lose;
            gameIsOver = true;
            EventManager.InvokeLose();
        }
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }
    public WinState GetWinState() { return _winState; }
}
