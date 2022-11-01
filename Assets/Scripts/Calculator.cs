using System;
using System.Collections.Generic;
using System.Linq;
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
        _skillUpgrades = _dataReader.SkillData;
        var saveData = SaveLoadManager.Instance.LoadWeaponUpgrade();
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
                    _heroDamageDataSo.heroAttack +=
                        availableSkillUpgrade.BaseIncrementAmount * saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.TapDamageBoost:
                    _heroDamageDataSo.tapAttack +=
                        availableSkillUpgrade.BaseIncrementAmount * saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.CriticalAttackBoost:
                    _heroDamageDataSo.criticalAttack += (float) availableSkillUpgrade.BaseIncrementAmount *
                                                        saveData[availableSkillUpgrade.ID];
                    break;
                case SkillTypes.CriticalAttackChance:
                    _heroDamageDataSo.criticalAttackChance += (float) availableSkillUpgrade.BaseIncrementAmount *
                                                              saveData[availableSkillUpgrade.ID];
                    break;
            }
        }

        UIManager.OnUpdateDamageHud.Invoke(_heroDamageDataSo.heroAttack, _heroDamageDataSo.tapAttack);
    }

    public void UpdateDamage(int skillID, int skillLevel)
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
        }

        UIManager.OnUpdateDamageHud.Invoke(_heroDamageDataSo.heroAttack, _heroDamageDataSo.tapAttack);
    }
}