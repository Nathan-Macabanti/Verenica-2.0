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

    [SerializeField] private NoteSpawner[] spawners;

    [Header("Note Prefab")]
    public Note DangerNote;
    public Note SafeNote;

    public ObjectPool<Note> danger_Note_Pool;
    public ObjectPool<Note> safe_Note_Pool;

    // Start is called before the first frame update
    void Awake()
    {
        IntializeSingleton();
        IntializeAllPool();
        InitializeSpawnersArray();
    }

    #region Initializers
        private void InitializeSpawnersArray()
        {
            if (spawners.Length != 0) return;

            NoteSpawner[] ntSpawners = GetComponentsInChildren<NoteSpawner>();
            int count = ntSpawners.Length;
            spawners = new NoteSpawner[count];
            for (int i = 0; i < count; i++)
            {
                spawners[i] = ntSpawners[i];
            }

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
        #endregion
    #endregion

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
        
        note.initialize(path, beat, key);
    }

    private void OnEnable()
    {
        EventManager.OnNoteSpawn += Spawn;
    }

    private void OnDisable()
    {
        EventManager.OnNoteSpawn -= Spawn;
    }

    public NoteSpawner[] GetNoteSpawners() { return spawners; }
}
