using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
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

        private SkillUpgrade _skillUpgrade;
        private int _level = 1;

        private Dictionary<int, int> _skillUpgradeDictionary;

        private void OnEnable()
        {
            UIManager.OnUpdateCoinHud += UpdateRow;
        }

        private void OnDisable()
        {
            UIManager.OnUpdateCoinHud -= UpdateRow;
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

            _level = _skillUpgradeDictionary.ContainsKey(_skillUpgrade.ID)
                ? _skillUpgradeDictionary[_skillUpgrade.ID]
                : 1;

            var damage = CalcUtils.FormatNumber(_skillUpgrade.BaseIncrementAmount * _level);
            var stringBuilder = DescriptionUtils.GetDescription(_skillUpgrade.SkillTypes);
            stringBuilder.Replace("x", damage);

            descriptionText.text = stringBuilder.ToString();
            levelText.text = _level.ToString();
        }

        private void SetButtonState(double totalCoin)
        {
            var cost = _skillUpgrade.BaseIncrementCost * _level;
            buttonCostText.text = $"{CalcUtils.FormatNumber(cost)}<sprite index= 11> ";

            buttonDescriptionText.text = _level > 1 ? "LEVEL UP" : "BUY";

            buyButton.enabled = cost < totalCoin;
        }

        public void OnBuy()
        {
            var cost = _skillUpgrade.BaseIncrementCost * _level;
            _level++;
            SaveLoadManager.Instance.SaveWeaponUpgrade(_skillUpgrade.ID, _level);
            EconomyManager.OnSpendCoin.Invoke(cost);

            var coin = SaveLoadManager.Instance.LoadCoin();
            UpdateRow(coin);
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