using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private NotePath path;
    [SerializeField] private int key;

    public NotePath GetPath() { return path; }
    public int GetKey() { return key; }
}
