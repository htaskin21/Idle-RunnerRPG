using System;
using TMPro;
using UnityEngine;
using Utils;

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
        private TextMeshProUGUI coinText;

        [SerializeField]
        private SkillUIPanel skillUIPanel;

        public static Action<double> OnUpdateCoinHud;

        private void Awake()
        {
            _instance = this;
            
            OnUpdateCoinHud = delegate(double d) { };
            OnUpdateCoinHud += UpdateCoinHud;
        }

        public void OpenSkillPanel()
        {
            skillUIPanel.panelObject.gameObject.SetActive(true);
        }

        public void LoadScrollers()
        {
            skillUIPanel.LoadData();
        }

        private void UpdateCoinHud(double coin)
        {
            coinText.text = CalcUtils.FormatNumber(coin);
        }
    }
}