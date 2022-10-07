using Cysharp.Threading.Tasks;
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

        [SerializeField]
        private GameObject coinHud;

        public GameObject CoinHud => coinHud;

        [SerializeField]
        private SkillUIPanel skillUIPanel;
        
        private void Awake()
        {
            _instance = this;
        }

        public void OpenSkillPanel()
        {
            skillUIPanel.panelObject.gameObject.SetActive(true);
        }

        public void LoadScrollers()
        {
            skillUIPanel.LoadData();
        }
    }
}