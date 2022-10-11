using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    private void InitializeSingleton()
    {
        if (instance == null) 
        { 
            instance = this; 
            //DontDestroyOnLoad(gameObject); 
        }
        else { Utils.SingletonErrorMessage(this); }
    }

    public static SoundManager GetInstance() { return instance; }
    #endregion

    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        InitializeSingleton();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
