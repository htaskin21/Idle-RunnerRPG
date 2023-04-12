using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enemy;
using Hero;
using ScriptableObjects;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class StageManager : MonoBehaviour
    {
        #region Singleton

        private static StageManager _instance;

        public static StageManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("Missing GameManager");

                return _instance;
            }
        }

        #endregion

        public EnemyController EnemyController { get; private set; }

        [Header("Managers")]
        [SerializeField]
        private HeroController _heroController;

        public HeroController HeroController => _heroController;

        [SerializeField]
        private BackgroundController _backgroundController;

        [SerializeField]
        private Calculator _calculator;

        [SerializeField]
        private PetManager _petManager;

        [SerializeField]
        private StageProgressBar _stageProgressBar;

        [SerializeField]
        private EnemyCreator _enemyCreator;

        [Header("Level Details")]
        [SerializeField]
        private List<LevelDataSO> _levelData;

        [SerializeField]
        private int _maxEnemyKillAmount;

        private LevelDataSO _currentLevelData;
        private int _enemyKillCount;
        private int _levelCount = 1;

        public static Action<int> OnPassStage;

        private void Awake()
        {
            _instance = this;
            OnPassStage = delegate(int i) { };
        }


        public void SetStage()
        {
            _levelCount = SaveLoadManager.Instance.LoadStageProgress();

            _currentLevelData = _levelData[0];

            _stageProgressBar.InitialProgressBar(_levelCount, _levelData[0].bossEnemy.enemyDamageType);

            _backgroundController.SetBackgrounds(_currentLevelData.skyImage, _currentLevelData.groundObject).Forget();

            _calculator.InitialCalculation();

            CreateEnemy();

            _heroController.StartRunning();

            _petManager.SetInitialPets();
        }

        private void CreateEnemy()
        {
            if (_enemyKillCount == _maxEnemyKillAmount)
            {
                EnemyController = _enemyCreator.CreateEnemy(EnemyController, _currentLevelData.bossEnemy,
                    _heroController.transform.position.x);
            }
            else
            {
                var rnd = Random.Range(0, _currentLevelData.regularEnemies.Count);

                EnemyController = _enemyCreator.CreateEnemy(EnemyController, _currentLevelData.regularEnemies[rnd],
                    _heroController.transform.position.x);
            }

            _enemyCreator.SetEnemyData(EnemyController, _levelCount);
            EnemyController.enemyHealth.OnEnemyDie += CheckLevelStatusAfterEnemyDie;
        }

        private void CheckLevelStatusAfterEnemyDie()
        {
            _enemyKillCount++;
            _stageProgressBar.IncreaseProgressBar(_enemyKillCount);

            if (_enemyKillCount > _maxEnemyKillAmount)
            {
                _levelCount++;

                SaveLoadManager.Instance.SaveStageProgress(_levelCount);
                OnPassStage?.Invoke(_levelCount);

                _enemyKillCount = 0;

                _currentLevelData = _levelData[Random.Range(0, _levelData.Count)];

                _backgroundController.SetBackgrounds(_currentLevelData.skyImage, _currentLevelData.groundObject)
                    .Forget();

                CreateEnemy();

                _stageProgressBar.InitialProgressBar(_levelCount, _currentLevelData.bossEnemy.enemyDamageType);
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

            _stageProgressBar.InitialProgressBar(_levelCount, _currentLevelData.bossEnemy.enemyDamageType);

            _heroController.StartRunning();
        }
    }
}