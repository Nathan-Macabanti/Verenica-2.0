using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private static ComboSystem instance;
    private void InitializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static ComboSystem GetInstance() { return instance; }

    private void Awake()
    {
        InitializeSingleton();
    }

    #region Collected Notes

    private uint collectedNotes = 0;
    public uint CollectedNotes { get { return collectedNotes; } }
    #endregion
    private uint maxCollectionThreshold;
    private uint currentMultiplier;

    private uint DcollectedNoteReq = 5, Dmultipler = 2;
    private uint CcollectedNoteReq = 10, Cmultipler = 3;
    private uint BcollectedNoteReq = 20, Bmultipler = 5;
    private uint AcollectedNoteReq = 35, Amultipler = 8;
    private uint ScollectedNoteReq = 55, Smultipler = 13;
    private uint SScollectedNoteReq = 80, SSmultipler = 20;
    public string LetterRank
    {
        get
        {
            if (currentMultiplier == Dmultipler) return "D";
            else if (currentMultiplier == Cmultipler) return "C";
            else if (currentMultiplier == Bmultipler) return "B";
            else if (currentMultiplier == Amultipler) return "A";
            else if (currentMultiplier == Smultipler) return "S";
            else if (currentMultiplier == SSmultipler) return "SS";

            return "F";
        }
    }
    public uint Multiplier { get{ return currentMultiplier; } }
    private uint prevCollectedNotes;

    private void Start()
    {
        ResetCollectedNote();
        maxCollectionThreshold = SScollectedNoteReq;
    }

    public void OnEnable()
    {
        EventManager.OnEnemyDied += ResetCollectedNote;
        EventManager.OnPlayerDamaged += ResetCollectedNote;
        EventManager.OnPlayerAttack += CollectNote;
    }

    public void OnDisable()
    {
        EventManager.OnEnemyDied -= ResetCollectedNote;
        EventManager.OnPlayerDamaged -= ResetCollectedNote;
        EventManager.OnPlayerAttack -= CollectNote;
    }

    public void Update()
    {
        OnValueChanged();
    }

    public void UpdateMultiplier()
    {
        if(collectedNotes >= 0 && collectedNotes < DcollectedNoteReq) //F
        {
            currentMultiplier = 1;
        }
        else if (collectedNotes >= DcollectedNoteReq && collectedNotes < CcollectedNoteReq) //D
        {
            currentMultiplier = Dmultipler;
        }
        else if (collectedNotes >= CcollectedNoteReq && collectedNotes < BcollectedNoteReq) //C
        {
            currentMultiplier = Cmultipler;
        }
        else if (collectedNotes >= BcollectedNoteReq && collectedNotes < AcollectedNoteReq) //B
        {
            currentMultiplier = Bmultipler;
        }
        else if (collectedNotes >= AcollectedNoteReq && collectedNotes < ScollectedNoteReq) //A
        {
            currentMultiplier = Amultipler;
        }
        else if (collectedNotes >= ScollectedNoteReq && collectedNotes < SScollectedNoteReq) //S
        {
            currentMultiplier = Smultipler;
        }
        else if(collectedNotes >= SScollectedNoteReq) //SS
        {
            currentMultiplier = SSmultipler;
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
