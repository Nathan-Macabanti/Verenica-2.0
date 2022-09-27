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
    [SerializeField] private SafeNote safeNote;

    private ObjectPool<Note> note_pool;
    #region Subscribe to event

    private void OnEnable()
    {
        EventManager.GetInstance().onNoteSpawn += Spawn;
    }

    private void OnDisable()
    {
        EventManager.GetInstance().onNoteSpawn -= Spawn;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        IntializePool(note_pool, safeNote);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(float beat, int index, NoteType type)
    {
        if(type == NoteType.Danger)
        {
            Debug.Log("Spawner " + index.ToString() + " " + beat.ToString() + "DANGER");
            //Note note = note_pool.Get();
            //spawners[index].SpawnNote(beat, note);
        }
        else if (type == NoteType.Safe)
        {
            Debug.Log("Spawner " + index.ToString() + " " + beat.ToString() + "SAFE");
            //Note note = note_pool.Get();
            //spawners[index].SpawnNote(beat, note);
        }
    }
}
