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
        private TextMeshProUGUI heroDamageText;

        [SerializeField]
        private TextMeshProUGUI tapDamageText;

        [SerializeField]
        private SkillUIPanel skillUIPanel;

        [SerializeField]
        private SkillUIPanel heroUIPanel;

        public static Action<double> OnUpdateCoinHud;

        public static Action<double, double> OnUpdateDamageHud;

        private void Awake()
        {
            _instance = this;

            OnUpdateCoinHud = delegate(double d) { };
            OnUpdateCoinHud += UpdateCoinHud;

            OnUpdateDamageHud = delegate(double d, double d1) { };
            OnUpdateDamageHud += UpdateDamageHud;
        }

        private void Start()
        {
            var coin = SaveLoadManager.Instance.LoadCoin();
            OnUpdateCoinHud.Invoke(coin);
        }

        public void OpenSkillPanel()
        {
            skillUIPanel.OpenPanel();
        }

        public void CloseSkillPanel()
        {
            skillUIPanel.ClosePanel();
        }

        public void LoadScrollers()
        {
            skillUIPanel.LoadData(DataReader.Instance.SkillData);
            heroUIPanel.LoadData(DataReader.Instance.SpecialAttackData);
        }

        private void UpdateCoinHud(double coin)
        {
            coinText.text = CalcUtils.FormatNumber(coin);
        }

        private void UpdateDamageHud(double heroDamage, double tapAttack)
        {
            heroDamageText.text = CalcUtils.FormatNumber(heroDamage);
            tapDamageText.text = CalcUtils.FormatNumber(tapAttack);
        }
    }
}