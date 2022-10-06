using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Collider))]
public abstract class Note : MonoBehaviour
{
    protected float beat;
    protected NotePath path;
    protected int notePos;
    protected IObjectPool<Note> _pool;
    protected bool isDead;

    protected Transform _transform;
    protected SongManager _songManager;
    protected ComboSystem _comboSystem;
    protected GameManager _gameManger;

    private void Start()
    {
        _transform = transform;
        _songManager = SongManager.GetInstance();
        _comboSystem = ComboSystem.GetInstance();
        _gameManger = GameManager.GetInstance();
    }

    public void initialize(NotePath _path, float _beat, int _notePos)
    {
        isDead = false;
        this.path = _path;
        this.beat = _beat;
        this.notePos = _notePos;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        if (path.source == null) return;
        if (path.destination == null) return;

        //Caluclate the distance and time you need to travel to the destination so that you will land on beat
        float songPosInBeats = _songManager.GetSongPositionInBeats();
        float beatOffset = _songManager.BeatOffset;
        float timeToDestinationInBeats = (beat - songPosInBeats);
        float distancePercent = (beatOffset - timeToDestinationInBeats) / beatOffset;

        if (distancePercent >= 1.5f)
        {
            ReturnToPool();
        }

        //Interpolate the Note
        Vector3 source = this.path.source.position;
        Vector3 destination = this.path.destination.position;
        
        if (distancePercent <= 1.0f)
        {
            //Interpolate to the beat line
            _transform.position = Vector3.Lerp(source, destination, distancePercent);
        }
        else 
        {
            //Keep moving offscreen at the same velocity as going to the beat line
            float dDistanceX = destination.x + (destination.x - source.x);
            float dDistanceY = destination.y + (destination.y - source.y);
            float dDistanceZ = destination.z + (destination.z - source.z);
            Vector3 doubleDestination = new Vector3(dDistanceX, dDistanceY, dDistanceZ);
            _transform.position = Vector3.Lerp(destination, doubleDestination, distancePercent - 1.0f); //-1 to reset distancePercent to 0.0f
        }
    }

    public abstract void OnPlayerCollided();

    public void ReturnToPool()
    {
        isDead = true;
        _pool.Release(this);
    }

    private void OnEnable()
    {
        EventManager.OnEnemyDied += ReturnToPool;
        EventManager.OnGameIsOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.OnEnemyDied -= ReturnToPool;
        EventManager.OnGameIsOver -= OnGameOver;
    }

    public void OnGameOver(WinState winState)
    {
        ReturnToPool();
    }

    public void SetPool(IObjectPool<Note> pool) => _pool = pool; 
    public bool GetIsDead() { return isDead; }
}
