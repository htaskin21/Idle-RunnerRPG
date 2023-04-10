using System;
using UI;
using UnityEngine;

namespace Managers
{
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

        [Header("Managers")]
        [SerializeField]
        private UIManager _uiManager;

        [SerializeField]
        private DataReader _dataReader;

        [SerializeField]
        private StageManager _stageManager;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            Application.targetFrameRate = 30;

            SetScene();
        }

        private void SetScene()
        {
            //DeleteSaveFiles();

            _dataReader.ReadAllData();
            _uiManager.LoadScrollers();

            _stageManager.SetStage();

            SaveLoadManager.Instance.SaveGameStartTime(DateTime.UtcNow);
        }

        private void DeleteSaveFiles()
        {
            ES3.DeleteDirectory(Application.persistentDataPath);
            ES3.DeleteFile(Application.persistentDataPath);
        }
    }
}