using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    #region Singleton
    private static EnemyGroup instance;
    private void InitializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static EnemyGroup GetInstance() { return instance; }
    #endregion

    private void Awake()
    {
        InitializeSingleton();
    }

    private Enemy currentEnemy;
    public Enemy CurrentEnemy { get { return currentEnemy; } }

    public void OnPhaseChange(Phase phase)
    {
        if (currentEnemy != null) currentEnemy = null;

        currentEnemy = phase.enemy;
        Instantiate(currentEnemy.gameObject, transform.position, Quaternion.identity, transform);
        currentEnemy.InitializeHP();
    }

    private void OnEnable()
    {
        EventManager.OnPhaseChange += OnPhaseChange;
    }
    private void OnDisable()
    {
        EventManager.OnPhaseChange -= OnPhaseChange;
    }
}
