using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton

        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("Missing GameManager");

                return _instance;
            }
        }

        #endregion
        
        private void Awake()
        {
            _instance = this;
        }

        [SerializeField]
        private GameObject coinHud;

        public GameObject CoinHud => coinHud;
    }
}