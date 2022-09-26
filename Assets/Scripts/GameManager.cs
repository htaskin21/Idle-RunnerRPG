using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enemy;
using Hero;
using UI;
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

    private EnemyController _enemyController;

    [Header("Controllers")]
    [SerializeField]
    private UIManager _uiManager;
    
    [SerializeField]
    private BackgroundController _backgroundController;

    [SerializeField]
    private ObjectPool _objectPool;

    [SerializeField]
    private DataReader _dataReader;

    [Header("Level Details")]
    [SerializeField]
    private List<LevelDataSO> levelData;

    [SerializeField]
    private int maxEnemyKillAmount;

    [SerializeField]
    private EnemyCreator enemyCreator;

    private LevelDataSO _currentLevelData;

    private int _enemyKillCount;

    private int _levelCount;

    #region Public Variables

    public HeroController HeroController => _heroController;
    public EnemyController EnemyController => _enemyController;
    public ObjectPool ObjectPool => _objectPool;

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //Buraya save sistemi gelicek.

        SetScene().Forget();
    }

    private async UniTask SetScene()
    {
//        CancellationTokenSource cts = new CancellationTokenSource();

//        await _dataReader.ReadAllData();

        _currentLevelData = levelData[0];

        _backgroundController.SetBackgrounds(_currentLevelData.skyImage, _currentLevelData.groundObject).Forget();

        CreateCharacters();

//        cts.Cancel();
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

        enemyCreator.SetEnemyData(_enemyController, _levelCount);
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

            _currentLevelData = levelData[Random.Range(0, levelData.Count)];

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