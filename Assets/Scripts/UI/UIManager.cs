using System;
using System.Collections.Generic;
using Managers;
using Skill;
using TMPro;
using UI.Pet;
using UI.SpecialAttack;
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
        private GameObject _gemHud;

        public GameObject GemHud => _gemHud;

        [SerializeField]
        private TextMeshProUGUI gemText;

        [SerializeField]
        private TextMeshProUGUI heroDamageText;

        [SerializeField]
        private TextMeshProUGUI tapDamageText;

        [SerializeField]
        private SkillUIPanel skillUIPanel;

        [SerializeField]
        private SpecialAttackUIPanel heroUIPanel;

        [SerializeField]
        private PetUIPanel _petUIPanel;

        [SerializeField]
        private List<UIPanel> uiPanels;

        public static Action<double> OnUpdateCoinHud;
        public static Action<int> OnUpdateGemHud;
        public static Action<double, double> OnUpdateDamageHud;

        private void Awake()
        {
            _instance = this;

            OnUpdateCoinHud = delegate(double d) { };
            OnUpdateCoinHud += UpdateCoinHud;

            OnUpdateGemHud = delegate(int d) { };
            OnUpdateGemHud += UpdateGemHud;

            OnUpdateDamageHud = delegate(double d, double d1) { };
            OnUpdateDamageHud += UpdateDamageHud;
        }

        private void Start()
        {
            var coin = SaveLoadManager.Instance.LoadCoin();
            OnUpdateCoinHud.Invoke(coin);

            var gem = SaveLoadManager.Instance.LoadGem();
            OnUpdateGemHud.Invoke(gem);
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

        private void UpdateGemHud(int gem)
        {
            gemText.text = CalcUtils.FormatNumber(gem);
        }

        private void UpdateDamageHud(double heroDamage, double tapAttack)
        {
            heroDamageText.text = CalcUtils.FormatNumber(heroDamage);
            tapDamageText.text = CalcUtils.FormatNumber(tapAttack);
        }

        public void AddPanelToUIPanels(UIPanel panel)
        {
            uiPanels.Add(panel);
        }

        public void CloseOtherUIPanels(UIPanel panel)
        {
            if (uiPanels.FindAll(x => x.gameObject.activeInHierarchy).Count > 0)
            {
                foreach (var uiPanel in uiPanels)
                {
                    if (uiPanel != panel)
                    {
                        uiPanel.InstantClosePanel();
                    }
                }
            }
        }
    }
}