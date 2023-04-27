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

            var oldDamage = _skillUpgrade.StartAmount + (_skillUpgrade.BaseIncrementAmount * (_level - 1));
            var newDamage = _skillUpgrade.StartAmount + (_skillUpgrade.BaseIncrementAmount * _level);
            //var damage = _level == 1 ? _skillUpgrade.StartAmount : newDamage - oldDamage;
            var damage = _skillUpgrade.BaseIncrementAmount * Mathf.Pow((_level), 1.2f);

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

            levelText.text = _skillUpgrade.SkillTypes == SkillTypes.BaseHeroSkill
                ? $" Hero Level {_level}"
                : $"Level {_level}";

            icon.sprite = _iconDataSo.GetIcon(_skillUpgrade.ID);
        }

        public override void SetButtonState(double totalGem)
        {
            var cost = _skillUpgrade.BaseIncrementCost * Mathf.Pow(_level, 1.2f);
            buttonCostText.text = $"{CalcUtils.FormatNumber(cost)} <sprite index= 0>";

            buttonDescriptionText.text = _level > 1 ? "LEVEL UP" : "BUY";

            buyButton.enabled = cost <= totalGem;
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

        public override void UpdateRow(double totalGem)
        {
            FillUIRow();
            SetButtonState(totalGem);
        }
    }
}