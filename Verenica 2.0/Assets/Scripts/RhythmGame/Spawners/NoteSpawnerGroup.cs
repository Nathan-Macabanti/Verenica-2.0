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

    [Header("Prefabs")]
    [SerializeField] private DangerNote dangerNote;
    [SerializeField] private SafeNote safeNote;

    private ObjectPool<Note> danger_note_pool;
    private ObjectPool<Note> safe_note_pool;
    #region Subscribe to event


    private void OnEnable()
    {
        EventManager.onNoteSpawn += Spawn;
    }

    private void OnDisable()
    {
        EventManager.onNoteSpawn -= Spawn;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        IntializePool(safe_note_pool, safeNote);
        IntializePool(danger_note_pool, dangerNote);
    }

    private void IntializePool(ObjectPool<Note> pool, Note prefab)
    {
        ObjectPool<Note> referencePool = pool;
        referencePool = new ObjectPool<Note>(() =>
        {
            return Instantiate(prefab);
        }, note =>
        {
            note.gameObject.SetActive(true); //OnGet
        }, note =>
        {
            note.gameObject.SetActive(false); //OnRelease
        }, note =>
        {
            Destroy(note.gameObject); //OnDestroy
        }, false, 50, 50);

        Debug.Log("Initialized " + prefab.name + " Pool");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(float beat, int index, NoteType type)
    {
        if(type == NoteType.Danger)
        {
            //Debug.Log("Spawner " + index.ToString() + " " + beat.ToString() + "DANGER");
            InitializeNote(beat, index, type, dangerNote);
        }
        else if (type == NoteType.Safe)
        {
            //Debug.Log("Spawner " + index.ToString() + " " + beat.ToString() + "SAFE");
            InitializeNote(beat, index, type, safeNote);
        }
    }

    public void InitializeNote(float beat, int index, NoteType type, Note note)
    {
        //Spawner info
        //Note note = danger_note_pool.Get();
        NoteSpawner spawner = spawners[index];
        NotePath path = spawner.GetPath();
        int key = spawner.GetKey();

        //Instantiate Note
        Vector3 source = spawner.GetPath().source.position;
        Note temp_note = null;
        temp_note = Instantiate(note, source, Quaternion.identity);
        temp_note.initialize(path, beat, key);
    }
}
