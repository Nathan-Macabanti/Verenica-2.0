using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note : MonoBehaviour
{
    protected float beat;
    protected NotePath path;
    protected int KeyToPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initialize(NotePath path, float beat, int keyToPress)
    {
        this.path = path;
        this.beat = beat;
        this.KeyToPress = keyToPress;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        //Caluclate the distance and time you need to travel to the destination so that you will land on beat
        float songPosInBeats = SongManager.GetInstance().GetSongPositionInBeats();
        float beatOffset = SongManager.GetInstance().beatOffset;
        float timeToDestination = (beat - songPosInBeats);
        float distance = (beatOffset - timeToDestination) / beatOffset;

        //Interpolate the Note
        Vector3 source = this.path.source.position;
        Vector3 destination = this.path.destination.position;
        transform.position = Vector3.Lerp(source, destination, distance);
    }

    public abstract void Action();
}
