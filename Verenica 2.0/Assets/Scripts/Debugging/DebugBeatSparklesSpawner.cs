using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is to test if the Beat event is being Invoked on beat
public class DebugBeatSparklesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    Transform _transform;

    private void OnEnable()
    {
        _transform = transform;
        EventManager.OnBeat += SpawnOnBeat;
    }

    private void OnDisable()
    {
        EventManager.OnBeat -= SpawnOnBeat;
    }

    public void SpawnOnBeat()
    {
        GameObject obj = Instantiate(prefab, _transform.position, Quaternion.identity) as GameObject;
        Destroy(obj, 2.0f);
    }
}
