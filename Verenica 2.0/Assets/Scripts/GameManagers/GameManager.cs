using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WinState { win, lose, none }

public class GameManager : MonoBehaviour
{
    private WinState _winState = WinState.none;
    [SerializeField] private Player player;
    private bool gameIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Win();
    }

    public void Win()
    {
        if (player.IsDead())
        {
            _winState = WinState.win;
            gameIsOver = true;
        }
    }

    public WinState GetWinState() { return _winState; }
}
