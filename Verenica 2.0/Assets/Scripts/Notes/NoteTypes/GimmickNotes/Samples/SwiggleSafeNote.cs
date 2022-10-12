using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiggleSafeNote : SafeNote
{
    [SerializeField] private AnimationCurve curve;

    public override void Move()
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
            //Swiggity Swooty
            _transform.localPosition = Vector3.Lerp(source, destination, distancePercent) + (Vector3.right * curve.Evaluate(distancePercent));
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
}
