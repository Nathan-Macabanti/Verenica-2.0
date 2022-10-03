using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Tooltip("Children order is source, destination")]
    [SerializeField] private NotePath path;
    [SerializeField] private int key;

    private void Awake()
    {
        InitializePath();
    }

    private void InitializePath()
    {
        if (path.source != null && path.destination != null) return;//If there is already a set source and destination no need to initialize

        Transform[] transform = GetComponentsInChildren<Transform>();
        Debug.Log(transform.Length);
        if(path.source == null)
            path.source = transform[1];

        if(path.destination == null)
            path.destination = transform[2];
    }

    public NotePath GetPath() { return path; }
    public int GetKey() { return key; }
}
