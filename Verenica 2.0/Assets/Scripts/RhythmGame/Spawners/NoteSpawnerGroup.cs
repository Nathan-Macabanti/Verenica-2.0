using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NoteSpawnerGroup : MonoBehaviour
{
    #region Singleton
    private static NoteSpawnerGroup instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static NoteSpawnerGroup GetInstance() { return instance; }
    #endregion
    #region Subscribe to event
    private void OnEnable()
    {
        EventManager.OnNoteSpawn += Spawn;
    }

    private void OnDisable()
    {
        EventManager.OnNoteSpawn -= Spawn;
    }
    #endregion

    [SerializeField] private NoteSpawner[] spawners;

    [Header("Note Prefab")]
    public Note DangerNote;
    public Note DangerNote2;
    public Note SafeNote;

    public ObjectPool<Note> danger_Note_Pool;
    public ObjectPool<Note> safe_Note_Pool;

    // Start is called before the first frame update
    void Awake()
    {
        IntializeSingleton();
        IntializeAllPool();
    }

    #region Initialize Pools
    private void IntializeAllPool()
    {
        InitializeDangerNotePool();
        InitializeSafeNotePool();
    }

    private void InitializeDangerNotePool()
    {
        danger_Note_Pool = new ObjectPool<Note>(() =>
        {
            var note = Instantiate(DangerNote);
            note.SetPool(danger_Note_Pool);
            return note;
        }, note =>
        {
            note.gameObject.SetActive(true); //OnGet
        }, note =>
        {
            note.gameObject.SetActive(false); //OnRelease
        }, note =>
        {
            Destroy(note.gameObject); //OnDestroy
        }, false, 10, 30);
    }

    private void InitializeSafeNotePool()
    {
        safe_Note_Pool = new ObjectPool<Note>(() =>
        {
            var note = Instantiate(SafeNote);
            note.SetPool(safe_Note_Pool);
            return note;
        }, note =>
        {
            note.gameObject.SetActive(true); //OnGet
        }, note =>
        {
            note.gameObject.SetActive(false); //OnRelease
        }, note =>
        {
            Destroy(note.gameObject); //OnDestroy
        }, false, 10, 30);
    }

    public void ChangeNote()
    {
        var temp = DangerNote;
        DangerNote = DangerNote2;
        DangerNote2 = temp;
    }
    #endregion

    private void Update()
    {
        //Debug.Log(danger_Note_Pool.CountActive.ToString() + " " + danger_Note_Pool.CountInactive.ToString());
    }

    public void Spawn(float beat, int index, NoteType type)
    {
        if(type == NoteType.Danger)
        {
            //Debug.Log("Spawner " + index.ToString() + " " + beat.ToString() + "DANGER");
            InitializeNote(beat, index, type, danger_Note_Pool);
        }
        else if (type == NoteType.Safe)
        {
            //Debug.Log("Spawner " + index.ToString() + " " + beat.ToString() + "SAFE");
            InitializeNote(beat, index, type, safe_Note_Pool);
        }
    }

    public void InitializeNote(float beat, int index, NoteType type, ObjectPool<Note> pool)
    {
        //Spawner info
        Note note = pool.Get();
        if (note == null) throw new System.Exception("No note exists");
        
        NoteSpawner spawner = spawners[index];
        NotePath path = spawner.GetPath();
        int key = spawner.GetKey();

        //Instantiate Note
        Vector3 source = spawner.GetPath().source.position;
        note.transform.position = spawner.GetPath().source.position;
        //Note temp_note = ObjectPool.GetInstance().GetFromPool(tag, source, Quaternion.identity);

        //temp_note = Instantiate(note, source, Quaternion.identity);
        note.initialize(path, beat, key);
    }
}
