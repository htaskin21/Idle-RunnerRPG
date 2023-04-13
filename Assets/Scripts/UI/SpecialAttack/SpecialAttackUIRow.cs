using System;
using System.Collections.Generic;
using Enums;
using Managers;
using ScriptableObjects;
using SpecialAttacks;
using UnityEngine;
using Utils;

namespace UI.SpecialAttack
{
    public class SpecialAttackUIRow : UIRow
    {
        [SerializeField]
        private IconDataSO _iconDataSo;

        private SpecialAttackUpgrade _specialAttackUpgrade;

        private int _level = 1;

        private Dictionary<int, int> _specialAttackDictionary;

        private Dictionary<int, int> _saveData;

        public static Action<int> OnUpdateSpecialAttack;

        private bool _isAchieveMinHeroLevel;

        private void Start()
        {
            UIManager.OnUpdateCoinHud += UpdateRow;
        }

        public override void SetUIRow(UpgradableStat upgradableStat)
        {
            _specialAttackUpgrade = (SpecialAttackUpgrade) upgradableStat;
            cellIdentifier = _specialAttackUpgrade.ID.ToString();

            var coin = SaveLoadManager.Instance.LoadCoin();

            UpdateRow(coin);
        }

        public override void FillUIRow()
        {
            _specialAttackDictionary = SaveLoadManager.Instance.LoadSpecialAttackUpgrade();

            if (_specialAttackDictionary.ContainsKey(_specialAttackUpgrade.ID))
            {
                _level = _specialAttackDictionary[_specialAttackUpgrade.ID];
            }
            else
            {
                _level = 1;
            }

            var damage = _specialAttackUpgrade.StartAmount + (_specialAttackUpgrade.BaseIncrementAmount * _level);

            var stringBuilder = DescriptionUtils.GetDescription(_specialAttackUpgrade.SkillTypes);
            if (stringBuilder.ToString().Contains("j"))
            {
                var damageString = "";
                if (_specialAttackUpgrade.SkillTypes == SkillTypes.AutoTapSpecial)
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

            if (_isAchieveMinHeroLevel == false)
            {
                CheckMinimumLevel();
            }

            icon.sprite = _iconDataSo.GetIcon(_specialAttackUpgrade.ID);
        }

        public override void SetButtonState(double totalGem)
        {
            if (_isAchieveMinHeroLevel == false)
            {
                buyButton.gameObject.SetActive(false);
            }
            else
            {
                CheckEnoughCoin(totalGem);
            }
        }

        private void CheckMinimumLevel()
        {
            var saveData = SaveLoadManager.Instance.LoadSkillUpgrade();
            if (saveData.ContainsKey(0))
            {
                if (saveData[0] < _specialAttackUpgrade.MinimumHeroLevel)
                {
                    levelText.text = $"<color=#cc0000>Min. Hero Level {_specialAttackUpgrade.MinimumHeroLevel}</color>";
                }
                else
                {
                    _isAchieveMinHeroLevel = true;
                }
            }
        }

        private void CheckEnoughCoin(double totalGem)
        {
            buyButton.gameObject.SetActive(true);

            var cost = _specialAttackUpgrade.BaseIncrementCost * _level;
            buttonCostText.text = $"{CalcUtils.FormatNumber(cost)} <sprite index= 0>";

            buttonDescriptionText.text = _level > 1 ? "LEVEL UP" : "BUY";

            buyButton.enabled = cost <= totalGem;
            buyButtonImage.sprite = buyButton.enabled ? activeButtonSprite : deActiveButtonSprite;
        }

        public override void OnBuy()
        {
            var coin = SaveLoadManager.Instance.LoadCoin();
            var cost = _specialAttackUpgrade.BaseIncrementCost * _level;
            if (coin >= cost)
            {
                _level++;
                SaveLoadManager.Instance.SaveSpecialAttackUpgrade(_specialAttackUpgrade.ID, _level);
                Calculator.OnUpdateSpecialAttackDamageCalculation.Invoke(_specialAttackUpgrade.ID, _level);
                EconomyManager.OnSpendCoin.Invoke(-cost);
                coin -= cost;

                UpdateRow(coin);

                if (_level <= 2)
                {
                    OnUpdateSpecialAttack?.Invoke(_specialAttackUpgrade.ID);
                }
            }
        }

        public override void UpdateRow(double totalGem)
        {
            FillUIRow();
            SetButtonState(totalGem);
        }
    }
}