using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private NotePath path;
    [SerializeField] private int key;
    private void Start()
    {
        
    }

    public void SpawnNote(float beat, Note note)
    {
        note.initialize(path, beat, key);
    }
}
