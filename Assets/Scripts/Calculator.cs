using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Skill;
using UI;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField]
    private DataReader _dataReader;

    [SerializeField]
    private HeroDamageDataSO _heroDamageDataSo;

    private List<SkillUpgrade> _skillUpgrades = new List<SkillUpgrade>();

    public static Action<int, int> OnUpdateDamageCalculation;

    private void Awake()
    {
        OnUpdateDamageCalculation = delegate(int d, int d1) { };
        OnUpdateDamageCalculation += UpdateDamage;
    }

    public void CalculateDamages()
    {
        ResetHeroDamageData();

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
                    _heroDamageDataSo.heroAttack += availableSkillUpgrade.StartAmount +
                                                    availableSkillUpgrade.BaseIncrementAmount *
                                                    saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.TapDamageBoost:
                    _heroDamageDataSo.tapAttack += availableSkillUpgrade.StartAmount +
                                                   availableSkillUpgrade.BaseIncrementAmount *
                                                   saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.CriticalAttackBoost:
                    _heroDamageDataSo.criticalAttack += (float) availableSkillUpgrade.StartAmount +
                                                        (float) availableSkillUpgrade.BaseIncrementAmount *
                                                        saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.CriticalAttackChance:
                    _heroDamageDataSo.criticalAttackChance += (float) availableSkillUpgrade.StartAmount +
                                                              (float) availableSkillUpgrade.BaseIncrementAmount *
                                                              saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.FireDmgSpecial:
                    _heroDamageDataSo.FireSpecialAttackMultiplier += (float) availableSkillUpgrade.StartAmount +
                                                                     (float) availableSkillUpgrade.BaseIncrementAmount *
                                                                     saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.LightningDmgSpecial:
                    _heroDamageDataSo.lightningSpecialAttackMultiplier += (float) availableSkillUpgrade.StartAmount +
                                                                          (float) availableSkillUpgrade
                                                                              .BaseIncrementAmount *
                                                                          saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.WaterDmgSpecial:
                    _heroDamageDataSo.WaterSpecialAttackMultiplier += (float) availableSkillUpgrade.StartAmount +
                                                                      (float) availableSkillUpgrade
                                                                          .BaseIncrementAmount *
                                                                      saveData[availableSkillUpgrade.ID];
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

    private void UpdateDamage(int skillID, int skillLevel)
    {
        var skillUpgrade = _skillUpgrades.FirstOrDefault(x => x.ID == skillID);

        var newDamageAmount = skillUpgrade.BaseIncrementAmount * skillLevel;
        var oldDamageAmount = skillUpgrade.BaseIncrementAmount * (skillLevel - 1);
        var difference = newDamageAmount - oldDamageAmount;

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
                _heroDamageDataSo.criticalAttack += (float) difference;
                break;
            case SkillTypes.CriticalAttackChance:
                _heroDamageDataSo.criticalAttackChance += (float) difference;
                break;
            case SkillTypes.FireDmgSpecial:
                _heroDamageDataSo.FireSpecialAttackMultiplier += (float) difference;
                break;
            case SkillTypes.WaterDmgSpecial:
                _heroDamageDataSo.WaterSpecialAttackMultiplier += (float) difference;
                break;
            case SkillTypes.LightningDmgSpecial:
                _heroDamageDataSo.lightningSpecialAttackMultiplier += (float) difference;
                break;
            default:
                Debug.LogWarning("Update Damage Default geldi");
                _heroDamageDataSo.heroAttack +=
                    0;
                break;
        }

        UIManager.OnUpdateDamageHud.Invoke(_heroDamageDataSo.heroAttack, _heroDamageDataSo.tapAttack);
    }

    private void ResetHeroDamageData()
    {
        _heroDamageDataSo.criticalAttack = 0;
        _heroDamageDataSo.heroAttack = 10;
        _heroDamageDataSo.tapAttack = 1;
        _heroDamageDataSo.earthDamageMultiplier = 1;
        _heroDamageDataSo.FireSpecialAttackMultiplier = 1;
        _heroDamageDataSo.lightningSpecialAttackMultiplier = 1;
        _heroDamageDataSo.plantDamageMultiplier = 1;
        _heroDamageDataSo.waterDamageMultiplier = 1;
        _heroDamageDataSo.WaterSpecialAttackMultiplier = 1;
        _heroDamageDataSo.criticalAttack = 1;
        _heroDamageDataSo.criticalAttack = 0;
    }
}