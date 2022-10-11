using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFX : MonoBehaviour
{
    #region Singleton
    private static GameSFX instance;
    private void InitializeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static GameSFX GetInstance() { return instance; }
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        InitializeSingleton();
    }

    public AudioClip playerHitSFX;
    public AudioClip enemyHitSFX;
    public AudioClip noteHitSFX;
}
