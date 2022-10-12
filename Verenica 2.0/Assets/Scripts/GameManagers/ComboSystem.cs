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
    public uint CollectedNotes { 
        get { return collectedNotes; }
        set 
        {
            collectedNotes = value;
            OnValueChanged();
        }
    }
    #endregion
    private uint maxCollectionThreshold;
    private uint currentMultiplier;

    private uint DcollectedNoteReq = 5, Dmultipler = 2;
    private uint CcollectedNoteReq = 10, Cmultipler = 3;
    private uint BcollectedNoteReq = 20, Bmultipler = 5;
    private uint AcollectedNoteReq = 35, Amultipler = 8;
    private uint ScollectedNoteReq = 55, Smultipler = 13;
    private uint SScollectedNoteReq = 80, SSmultipler = 20;
    private string _letterRank;
    public uint Multiplier { get{ return currentMultiplier; } }

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

    public void UpdateRank()
    {
        if(collectedNotes >= 0 && collectedNotes < DcollectedNoteReq) //F
        {
            currentMultiplier = 1;
            _letterRank = "F";
        }
        else if (collectedNotes >= DcollectedNoteReq && collectedNotes < CcollectedNoteReq) //D
        {
            currentMultiplier = Dmultipler;
            _letterRank = "D";
        }
        else if (collectedNotes >= CcollectedNoteReq && collectedNotes < BcollectedNoteReq) //C
        {
            currentMultiplier = Cmultipler;
            _letterRank = "C";
        }

        else if (collectedNotes >= BcollectedNoteReq && collectedNotes < AcollectedNoteReq) //B
        {
            currentMultiplier = Bmultipler;
            _letterRank = "B";
        }
        else if (collectedNotes >= AcollectedNoteReq && collectedNotes < ScollectedNoteReq) //A
        {
            currentMultiplier = Amultipler;
            _letterRank = "A";
        }
        else if (collectedNotes >= ScollectedNoteReq && collectedNotes < SScollectedNoteReq) //S
        {
            currentMultiplier = Smultipler;
            _letterRank = "S";
        }
        else if(collectedNotes >= SScollectedNoteReq) //SS
        {
            currentMultiplier = SSmultipler;
            _letterRank = "SS";
        }
    }

    private void OnValueChanged()
    {
        UpdateRank();
        EventManager.InvokeComboValueChanged(collectedNotes, _letterRank);
    }

    public void CollectNote()
    {
        CollectedNotes++;
        CollectedNotes = (uint)Mathf.Clamp(collectedNotes, 0, maxCollectionThreshold);
    }

    public void ResetCollectedNote()
    {
        collectedNotes = 0;
    }
}
