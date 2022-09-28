using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static PlayerManager GetInstance() { return instance; }
    #endregion

    private void Awake()
    {
        IntializeSingleton();
    }

    public Player player;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        GameObject playerObj = player.gameObject;
        playerMovement = playerObj.GetComponent<PlayerMovement>();
    }
}
