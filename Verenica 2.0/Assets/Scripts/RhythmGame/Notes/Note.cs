using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Note : MonoBehaviour
{
    protected float beat;
    protected NotePath path;
    protected int notePos;
    protected IObjectPool<Note> _pool;

    public void initialize(NotePath _path, float _beat, int _notePos)
    {
        //Set the position to the start
        transform.position = _path.source.position;

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
        float songPosInBeats = SongManager.GetInstance().GetSongPositionInBeats();
        float beatOffset = SongManager.GetInstance().beatOffset;
        float timeToDestination = (beat - songPosInBeats);
        float distance = (beatOffset - timeToDestination) / beatOffset;

        //Interpolate the Note
        //Debug.Log(distance);
        Vector3 source = this.path.source.position;
        Vector3 destination = this.path.destination.position;
        transform.position = Vector3.Lerp(source, destination, distance);

        //Check if at destination
        PlayerMovement playerMovement = PlayerManager.GetInstance().playerMovement;
        if (distance >= 1.0f) 
        {
            EventManager.InvokeOnNoteHit(playerMovement.GetPosIndex(), notePos);
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        _pool.Release(this);
    }

    public abstract void Action();

    public void SetPool(IObjectPool<Note> pool) => _pool = pool; 
}
