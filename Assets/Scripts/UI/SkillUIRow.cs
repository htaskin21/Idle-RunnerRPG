using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class SkillUIRow : EnhancedScrollerCellView
    {
        [SerializeField]
        private GameObject parentObject;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private Button buyButton;

        [SerializeField]
        private TextMeshProUGUI buttonCostText;

        [SerializeField]
        private TextMeshProUGUI buttonDescriptionText;

        [SerializeField]
        private Image buyButtonImage;

        [SerializeField]
        private Sprite activeButtonSprite;

        [SerializeField]
        private Sprite deActiveButtonSprite;

        [SerializeField]
        private SkillIconDataSO skillIconDataSo;

        private SkillUpgrade _skillUpgrade;
        private int _level = 1;

        private Dictionary<int, int> _skillUpgradeDictionary;

        private Dictionary<int, int> _saveData;

        private void Start()
        {
            UIManager.OnUpdateCoinHud += UpdateRow;
        }

        public void SetSkillUIRow(SkillUpgrade skillUpgrade)
        {
            _skillUpgrade = skillUpgrade;
            cellIdentifier = skillUpgrade.ID.ToString();

            var coin = SaveLoadManager.Instance.LoadCoin();

            UpdateRow(coin);
        }

        private void FillSkillUIRow()
        {
            _skillUpgradeDictionary = SaveLoadManager.Instance.LoadWeaponUpgrade();

            var a = _skillUpgradeDictionary.ContainsKey(_skillUpgrade.ID);

            if (_skillUpgradeDictionary.ContainsKey(_skillUpgrade.ID))
            {
                _level = _skillUpgradeDictionary[_skillUpgrade.ID];
            }
            else
            {
                _level = 1;
            }

            var damage = CalcUtils.FormatNumber(_skillUpgrade.BaseIncrementAmount * _level);

            var stringBuilder = DescriptionUtils.GetDescription(_skillUpgrade.SkillTypes);
            if (stringBuilder.ToString().Contains("x"))
            {
                stringBuilder.Replace("x", damage);
            }

            descriptionText.text = stringBuilder.ToString();
            levelText.text = $"Level {_level}";

            icon.sprite = skillIconDataSo.GetIcon(_skillUpgrade.ID);
        }

        private void SetButtonState(double totalCoin)
        {
            var cost = _skillUpgrade.BaseIncrementCost * _level;
            buttonCostText.text = $"{CalcUtils.FormatNumber(cost)} <sprite index= 11>";

            buttonDescriptionText.text = _level > 1 ? "LEVEL UP" : "BUY";

            buyButton.enabled = cost <= totalCoin;
            buyButtonImage.sprite = buyButton.enabled ? activeButtonSprite : deActiveButtonSprite;
        }

        public void OnBuy()
        {
            var coin = SaveLoadManager.Instance.LoadCoin();
            var cost = _skillUpgrade.BaseIncrementCost * _level;
            if (coin >= cost)
            {
                _level++;
                SaveLoadManager.Instance.SaveWeaponUpgrade(_skillUpgrade.ID, _level);
                Calculator.OnUpdateDamageCalculation.Invoke(_skillUpgrade.ID, _level);
                EconomyManager.OnSpendCoin.Invoke(-cost);
                coin -= cost;

                UpdateRow(coin);
            }
        }

        private void UpdateRow(double totalCoin)
        {
            FillSkillUIRow();
            SetButtonState(totalCoin);
        }

        public override void RefreshCellView()
        {
            base.RefreshCellView();
        }
    }
}