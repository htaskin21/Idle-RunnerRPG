using System.Collections.Generic;
using Enums;
using Managers;
using ScriptableObjects;
using UI;
using UnityEngine;
using Utils;

namespace Skill
{
    public class SkillUIRow : UIRow
    {
        [SerializeField]
        private IconDataSO _iconDataSo;

        private SkillUpgrade _skillUpgrade;
        private int _level = 1;

        private Dictionary<int, int> _skillUpgradeDictionary;

        private Dictionary<int, int> _saveData;

        private void Start()
        {
            UIManager.OnUpdateCoinHud += UpdateRow;
        }

        public override void SetUIRow(UpgradableStat skillUpgrade)
        {
            _skillUpgrade = (SkillUpgrade) skillUpgrade;
            cellIdentifier = skillUpgrade.ID.ToString();

            var coin = SaveLoadManager.Instance.LoadCoin();

            UpdateRow(coin);
        }

        public override void FillUIRow()
        {
            _skillUpgradeDictionary = SaveLoadManager.Instance.LoadSkillUpgrade();

            if (_skillUpgradeDictionary.ContainsKey(_skillUpgrade.ID))
            {
                _level = _skillUpgradeDictionary[_skillUpgrade.ID];
            }
            else
            {
                _level = 1;
            }

            var damage = _skillUpgrade.StartAmount + (_skillUpgrade.BaseIncrementAmount * _level);

            var stringBuilder = DescriptionUtils.GetDescription(_skillUpgrade.SkillTypes);
            if (stringBuilder.ToString().Contains("j"))
            {
                var damageString = "";
                if (_skillUpgrade.SkillTypes == SkillTypes.AutoTapSpecial)
                {
                    damageString = DescriptionUtils.ConvertToMinutes((float) damage);
                }
                else
                {
                    damageString = CalcUtils.FormatNumber(damage);
                }

                stringBuilder.Replace("j", damageString);
            }

            descriptionText.text = stringBuilder.ToString();
            levelText.text = $"Level {_level}";

            icon.sprite = _iconDataSo.GetIcon(_skillUpgrade.ID);
        }

        public override void SetButtonState(double totalCoin)
        {
            var cost = _skillUpgrade.BaseIncrementCost * _level;
            buttonCostText.text = $"{CalcUtils.FormatNumber(cost)} <sprite index= 0>";

            buttonDescriptionText.text = _level > 1 ? "LEVEL UP" : "BUY";

            buyButton.enabled = cost <= totalCoin;
            buyButtonImage.sprite = buyButton.enabled ? activeButtonSprite : deActiveButtonSprite;
        }

        public override void OnBuy()
        {
            var coin = SaveLoadManager.Instance.LoadCoin();
            var cost = _skillUpgrade.BaseIncrementCost * _level;
            if (coin >= cost)
            {
                _level++;
                SaveLoadManager.Instance.SaveSkillUpgrade(_skillUpgrade.ID, _level);
                Calculator.OnUpdateDamageCalculation.Invoke(_skillUpgrade.ID, _level);
                EconomyManager.OnSpendCoin.Invoke(-cost);
                coin -= cost;

                UpdateRow(coin);
            }
        }

        public override void UpdateRow(double totalCoin)
        {
            FillUIRow();
            SetButtonState(totalCoin);
        }
    }
}