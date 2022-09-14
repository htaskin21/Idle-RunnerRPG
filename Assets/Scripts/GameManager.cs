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

    [SerializeField]
    private HeroController _heroController;

    public HeroController HeroController => _heroController;

    private EnemyController _enemyController;
    public EnemyController EnemyController => _enemyController;

    [Space]
    [Header("Controllers")]
    [SerializeField]
    private BackgroundController _backgroundController;

    public List<LevelDataSO> levelDatas;

    private LevelDataSO _currentLevelData;

    private int _enemyKillCount;

    private int _levelCount;

    [SerializeField]
    private int maxEnemyKillAmount;

    [SerializeField]
    private EnemyCreator enemyCreator;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //Buraya save sistemi gelicek.

        _currentLevelData = levelDatas[0];

        _backgroundController.SetBackgrounds(_currentLevelData.skyImage, _currentLevelData.groundObject).Forget();

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

        if (_enemyKillCount == maxEnemyKillAmount)
        {
            _enemyController = Instantiate(_currentLevelData.bossEnemy,
                new Vector3((_heroController.transform.position.x + 14f),
                    _currentLevelData.bossEnemy.transform.position.y, 0),
                quaternion.identity);
        }
        else
        {
            var rnd = Random.Range(0, _currentLevelData.regularEnemies.Count);

            _enemyController = Instantiate(_currentLevelData.regularEnemies[rnd],
                new Vector3((_heroController.transform.position.x + 14f),
                    _currentLevelData.regularEnemies[rnd].transform.position.y, 0),
                quaternion.identity);
        }

        enemyCreator.SetEnemyData(_enemyController);
        _enemyController.enemyHealth.OnEnemyDie += CheckLevelStatusAfterEnemyDie;
    }

    private void CreateCharacters()
    {
        CreateEnemy();
    }

    private void CheckLevelStatusAfterEnemyDie()
    {
        _enemyKillCount++;

        if (_enemyKillCount > maxEnemyKillAmount)
        {
            _levelCount++;

            _enemyKillCount = 0;

            _currentLevelData = levelDatas[Random.Range(0, levelDatas.Count)];

            _backgroundController.SetBackgrounds(_currentLevelData.skyImage, _currentLevelData.groundObject).Forget();

            CreateEnemy();
        }
        else
        {
            CreateEnemy();
        }
    }

    public void LoadSameLevel()
    {
        _enemyKillCount = 0;

        _backgroundController.SetBackgrounds(_currentLevelData.skyImage, _currentLevelData.groundObject).Forget();

        CreateEnemy();
    }
}