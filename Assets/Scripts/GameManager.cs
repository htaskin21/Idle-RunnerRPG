using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enemy;
using Hero;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Missing GameManager");

            return _instance;
        }
    }

    #endregion

    [SerializeField] private HeroController _heroController;
    public HeroController HeroController => _heroController;

    [SerializeField] private EnemyController _enemyController;
    public EnemyController EnemyController => _enemyController;

    [SerializeField] private List<EnemyController> enemyList;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        CreateCharacters();
    }

    private async UniTask CreateHero()
    {
        _heroController = Instantiate(_heroController, new Vector3(-1.3f, _heroController.transform.position.y, 0),
            quaternion.identity);
    }

    private void CreateEnemy()
    {
        if (_enemyController != null)
        {
            foreach (var d in _enemyController.enemyHealth.OnEnemyDie.GetInvocationList())
            {
                _enemyController.enemyHealth.OnEnemyDie -= (Action) d;
            }

            Destroy(_enemyController.gameObject);
        }

        var rnd = Random.Range(0, enemyList.Count);

        _enemyController = Instantiate(enemyList[rnd],
            new Vector3((_heroController.transform.position.x + 14f), enemyList[rnd].transform.position.y, 0),
            quaternion.identity);

        _enemyController.enemyHealth.OnEnemyDie += CreateEnemy;
    }

    private void CreateCharacters()
    {
        CreateEnemy();
    }
}