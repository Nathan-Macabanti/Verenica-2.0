using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    [Tooltip("If not working just directly plug the audioSource")]
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetClip(AudioClip clip) { audioSource.clip = clip; }
    public void Play() 
    { 
        if (!audioSource.isPlaying) 
        { 
            audioSource.Play();
        } 
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}
