using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Managers;
using ScriptableObjects;
using Skill;
using SpecialAttacks;
using UI;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField]
    private DataReader _dataReader;

    [SerializeField]
    private HeroDamageDataSO _heroDamageDataSo;

    [SerializeField]
    private HeroDamageDataSO _baseHeroDamageDataSo;

    private List<SkillUpgrade> _skillUpgrades = new List<SkillUpgrade>();

    private List<SpecialAttackUpgrade> _specialAttackUpgrades = new List<SpecialAttackUpgrade>();

    public static Action<int, int> OnUpdateDamageCalculation;

    public static Action<int, int> OnUpdateSpecialAttackDamageCalculation;

    private PassiveGoldEarnCalculator _passiveGoldEarnCalculator;
    private CancellationTokenSource _passiveEarnCancellationTokenSource;

    private void Awake()
    {
        OnUpdateDamageCalculation = delegate(int d, int d1) { };
        OnUpdateDamageCalculation += UpdateDamage;

        OnUpdateSpecialAttackDamageCalculation = delegate(int i, int i1) { };
        OnUpdateSpecialAttackDamageCalculation += UpdateSpecialAttackDamage;
    }

    private void CalculateSpecialAttackDamage()
    {
        _specialAttackUpgrades = _dataReader.SpecialAttackData;
        var saveData = SaveLoadManager.Instance.LoadSpecialAttackUpgrade();
        List<SpecialAttackUpgrade> availableSpecialAttackUpgrades = new List<SpecialAttackUpgrade>();

        //Uygun tüm upgradeleri buluyoruz
        foreach (var specialAttackUpgrade in _specialAttackUpgrades)
        {
            if (saveData.ContainsKey(specialAttackUpgrade.ID))
            {
                if (saveData[specialAttackUpgrade.ID] > 0)
                {
                    availableSpecialAttackUpgrades.Add(specialAttackUpgrade);
                }
            }
        }

        foreach (var specialAttackUpgrade in _specialAttackUpgrades)
        {
            var level = 0;
            if (saveData.ContainsKey(specialAttackUpgrade.ID))
            {
                level = saveData[specialAttackUpgrade.ID];
            }

            switch (specialAttackUpgrade.SkillTypes)
            {
                case SkillTypes.FireDmgSpecial:
                    _heroDamageDataSo.fireSpecialAttackMultiplier += specialAttackUpgrade.StartAmount +
                                                                     specialAttackUpgrade
                                                                         .BaseIncrementAmount *
                                                                     level;
                    break;
                case SkillTypes.LightningDmgSpecial:
                    _heroDamageDataSo.lightningSpecialAttackMultiplier +=
                        specialAttackUpgrade.StartAmount +
                        specialAttackUpgrade
                            .BaseIncrementAmount *
                        level;
                    break;
                case SkillTypes.WaterDmgSpecial:
                    _heroDamageDataSo.waterSpecialAttackMultiplier +=
                        specialAttackUpgrade.StartAmount +
                        specialAttackUpgrade
                            .BaseIncrementAmount *
                        level;
                    break;
                case SkillTypes.HolyDmgSpecial:
                    _heroDamageDataSo.holySpecialAttackMultiplier +=
                        specialAttackUpgrade.StartAmount +
                        specialAttackUpgrade
                            .BaseIncrementAmount *
                        level;
                    break;
                case SkillTypes.PlantDmgSpecial:
                    _heroDamageDataSo.plantSpecialAttackMultiplier +=
                        specialAttackUpgrade.StartAmount +
                        specialAttackUpgrade
                            .BaseIncrementAmount *
                        level;
                    break;
                case SkillTypes.AutoTapSpecial:
                    _heroDamageDataSo.autoTapAttackDuration +=
                        (int) specialAttackUpgrade.StartAmount +
                        (int) specialAttackUpgrade
                            .BaseIncrementAmount *
                        level;
                    break;

                case SkillTypes.GoldenTap:
                    _heroDamageDataSo.goldenTapDuration +=
                        (int) specialAttackUpgrade.StartAmount +
                        (int) specialAttackUpgrade
                            .BaseIncrementAmount *
                        level;
                    break;

                default:
                    Debug.LogWarning($"{specialAttackUpgrade.SkillTypes} Calculate Damage Default geldi");
                    _heroDamageDataSo.heroAttack +=
                        0;
                    break;
            }
        }
    }

    private void UpdateSpecialAttackDamage(int skillID, int skillLevel)
    {
        var specialAttackUpgrade = _specialAttackUpgrades.FirstOrDefault(x => x.ID == skillID);

        var newDamageAmount = specialAttackUpgrade.BaseIncrementAmount * skillLevel;
        var oldDamageAmount = specialAttackUpgrade.BaseIncrementAmount * (skillLevel - 1);
        var difference = newDamageAmount - oldDamageAmount;

        switch (specialAttackUpgrade.SkillTypes)
        {
            case SkillTypes.FireDmgSpecial:
                _heroDamageDataSo.fireSpecialAttackMultiplier += difference;
                break;
            case SkillTypes.WaterDmgSpecial:
                _heroDamageDataSo.waterSpecialAttackMultiplier += difference;
                break;
            case SkillTypes.LightningDmgSpecial:
                _heroDamageDataSo.lightningSpecialAttackMultiplier += difference;
                break;
            case SkillTypes.PlantDmgSpecial:
                _heroDamageDataSo.plantSpecialAttackMultiplier += difference;
                break;
            case SkillTypes.HolyDmgSpecial:
                _heroDamageDataSo.holySpecialAttackMultiplier += difference;
                break;
            case SkillTypes.AutoTapSpecial:
                _heroDamageDataSo.autoTapAttackDuration += (int) difference;
                break;
            case SkillTypes.GoldenTap:
                _heroDamageDataSo.goldenTapDuration += (int) difference;
                break;
            default:
                Debug.LogWarning("Update Damage Default geldi");
                _heroDamageDataSo.heroAttack +=
                    0;
                break;
        }
    }

    private void CalculateDamages()
    {
        _skillUpgrades = _dataReader.SkillData;

        var saveData = SaveLoadManager.Instance.LoadSkillUpgrade();
        List<SkillUpgrade> availableSkillUpgrades = new List<SkillUpgrade>();

        //Uygun tüm upgradeleri buluyoruz
        foreach (var skillUpgrade in _skillUpgrades)
        {
            if (saveData.ContainsKey(skillUpgrade.ID))
            {
                if (saveData[skillUpgrade.ID] > 0)
                {
                    availableSkillUpgrades.Add(skillUpgrade);
                }
            }
        }

        foreach (var availableSkillUpgrade in availableSkillUpgrades)
        {
            switch (availableSkillUpgrade.SkillTypes)
            {
                case SkillTypes.BaseAttackBoost:
                    _heroDamageDataSo.heroAttack += CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.TapDamageBoost:
                    _heroDamageDataSo.tapAttack += CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.CriticalAttackBoost:
                    _heroDamageDataSo.criticalAttackMultiplier +=
                        (float) CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.CriticalAttackChance:
                    _heroDamageDataSo.criticalAttackChance += (float) CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.FireDmg:
                    _heroDamageDataSo.fireDamageMultiplier += CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.LightningDmg:
                    _heroDamageDataSo.lightningDamageMultiplier +=
                        CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.WaterDmg:
                    _heroDamageDataSo.waterDamageMultiplier += CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.PlantDmg:
                    _heroDamageDataSo.plantDamageMultiplier += CalculateSkill(availableSkillUpgrade, saveData);
                    break;
                case SkillTypes.HolyDmg:
                    _heroDamageDataSo.holyDamageMultiplier += CalculateSkill(availableSkillUpgrade, saveData);
                    break;

                case SkillTypes.BaseHeroSkill:
                    _heroDamageDataSo.heroAttack += CalculateSkill(availableSkillUpgrade, saveData);

                    _heroDamageDataSo.tapAttack += CalculateSkill(availableSkillUpgrade, saveData);

                    break;

                case SkillTypes.PassiveGoldEarn:
                    _heroDamageDataSo.passiveGoldAmount += CalculateSkill(availableSkillUpgrade, saveData);
                    InitPassiveEarnCalculator();
                    break;


                default:
                    Debug.LogWarning("Calculate Damage Default geldi");
                    _heroDamageDataSo.heroAttack +=
                        0;
                    break;
            }
        }

        UIManager.OnUpdateDamageHud.Invoke(_heroDamageDataSo.heroAttack, _heroDamageDataSo.tapAttack);
    }

    private double CalculateSkill(SkillUpgrade availableSkillUpgrade, Dictionary<int, int> saveData)
    {
        double skillAmount = 0;
        var skillLevel = saveData[availableSkillUpgrade.ID];
        for (int i = 0; i < skillLevel; i++)
        {
            skillAmount += availableSkillUpgrade.BaseIncrementAmount * Mathf.Pow(i, 1.2f);
        }

        return skillAmount;
    }

    private void UpdateDamage(int skillID, int skillLevel)
    {
        var skillUpgrade = _skillUpgrades.FirstOrDefault(x => x.ID == skillID);
        var difference = skillUpgrade.BaseIncrementAmount * Mathf.Pow(skillLevel - 1, 1.2f);

        switch (skillUpgrade.SkillTypes)
        {
            case SkillTypes.BaseAttackBoost:
                _heroDamageDataSo.heroAttack +=
                    difference;
                break;
            case SkillTypes.TapDamageBoost:
                _heroDamageDataSo.tapAttack +=
                    difference;
                break;
            case SkillTypes.CriticalAttackBoost:
                _heroDamageDataSo.criticalAttackMultiplier += (float) difference;
                break;
            case SkillTypes.CriticalAttackChance:
                _heroDamageDataSo.criticalAttackChance += (float) difference;
                break;
            case SkillTypes.FireDmg:
                _heroDamageDataSo.fireDamageMultiplier += difference;
                break;
            case SkillTypes.WaterDmg:
                _heroDamageDataSo.waterDamageMultiplier += difference;
                break;
            case SkillTypes.LightningDmg:
                _heroDamageDataSo.lightningDamageMultiplier += difference;
                break;
            case SkillTypes.PlantDmg:
                _heroDamageDataSo.plantDamageMultiplier += difference;
                break;
            case SkillTypes.HolyDmg:
                _heroDamageDataSo.holyDamageMultiplier += difference;
                break;
            case SkillTypes.BaseHeroSkill:
                _heroDamageDataSo.heroAttack +=
                    difference;
                _heroDamageDataSo.tapAttack +=
                    difference;
                break;
            case SkillTypes.PassiveGoldEarn:
                _heroDamageDataSo.passiveGoldAmount += difference;
                if (skillLevel == 2)
                {
                    InitPassiveEarnCalculator();
                }

                break;

            default:
                Debug.LogWarning("Update Damage Default geldi");
                _heroDamageDataSo.heroAttack +=
                    0;
                break;
        }

        UIManager.OnUpdateDamageHud.Invoke(_heroDamageDataSo.heroAttack, _heroDamageDataSo.tapAttack);
    }

    private void InitPassiveEarnCalculator()
    {
        _passiveGoldEarnCalculator =
            new PassiveGoldEarnCalculator(_heroDamageDataSo);

        _passiveEarnCancellationTokenSource = new CancellationTokenSource();

        _passiveGoldEarnCalculator.EarnPassiveGold(_passiveEarnCancellationTokenSource.Token).Forget();
    }

    public void InitialCalculation()
    {
        _heroDamageDataSo.ResetHeroDamageDataSO(_baseHeroDamageDataSo);

        CalculateDamages();
        CalculateSpecialAttackDamage();
    }

    private void OnDestroy()
    {
        _passiveEarnCancellationTokenSource?.Cancel();
    }
}