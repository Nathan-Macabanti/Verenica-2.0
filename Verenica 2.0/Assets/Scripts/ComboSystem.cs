using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private static ComboSystem instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static ComboSystem GetInstance() { return instance; }

    private void Awake()
    {
        IntializeSingleton();
    }

    [Range(1, 100)] [SerializeField] private uint DcollectedNoteReq = 10;
    [Range(1, 100)] [SerializeField] private uint CcollectedNoteReq = 20;
    [Range(1, 100)] [SerializeField] private uint BcollectedNoteReq = 30;
    [Range(1, 100)] [SerializeField] private uint AcollectedNoteReq = 50;
    [Range(1, 100)] [SerializeField] private uint ScollectedNoteReq = 75;
    [Range(1, 100)] [SerializeField] private uint SScollectedNoteReq = 100;

    private uint collectedNotes = 0;
    private uint maxCollectionThreshold = 60;
    private uint currentMultiplier;
    public uint Multiplier { get{ return currentMultiplier; } }
    private uint prevCollectedNotes;

    private void Start()
    {
        ResetCollectedNote();
    }

    public void OnEnable()
    {
        EventManager.OnPlayerDamaged += ResetCollectedNote;
        EventManager.OnPlayerAttack += CollectNote;
    }

    public void OnDisable()
    {
        EventManager.OnPlayerDamaged += ResetCollectedNote;
        EventManager.OnPlayerAttack += CollectNote;
    }

    public void Update()
    {
        OnValueChanged();
    }

    public void UpdateMultiplier()
    {
        if(collectedNotes <= 0)
        {
            currentMultiplier = 1;
        }  
        else if(collectedNotes > 0 && collectedNotes <= DcollectedNoteReq)
        {
            currentMultiplier = 2;
        }
        else if (collectedNotes > DcollectedNoteReq && collectedNotes <= CcollectedNoteReq)
        {
            currentMultiplier = 3;
        }
        else if (collectedNotes > CcollectedNoteReq && collectedNotes <= BcollectedNoteReq)
        {
            currentMultiplier = 5;
        }
        else if (collectedNotes > BcollectedNoteReq && collectedNotes <= AcollectedNoteReq)
        {
            currentMultiplier = 8;
        }
        else if (collectedNotes > AcollectedNoteReq && collectedNotes <= ScollectedNoteReq)
        {
            currentMultiplier = 13;
        }
        else if (collectedNotes > ScollectedNoteReq && collectedNotes <= SScollectedNoteReq)
        {
            currentMultiplier = 20;
        }
        else
        {
            currentMultiplier = 30;
        }
    }

    private void OnValueChanged()
    {
        if (prevCollectedNotes == collectedNotes) return;

        UpdateMultiplier();

        prevCollectedNotes = collectedNotes;
    }

    public void CollectNote()
    {
        collectedNotes++;
        collectedNotes = (uint)Mathf.Clamp(collectedNotes, 0, maxCollectionThreshold);
    }

    public void ResetCollectedNote()
    {
        collectedNotes = 0;
    }
}
