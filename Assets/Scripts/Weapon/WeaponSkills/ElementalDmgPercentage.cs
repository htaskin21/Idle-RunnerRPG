using System;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Weapon.WeaponSkills
{
    public class ElementalDmgPercentage : WeaponSkill
    {
        public DamageType CurrentDamageType;

        public ElementalDmgPercentage(float percentage)
        {
            CurrentDamageType = GetDamageType();
            WeaponSkillPercentage = percentage;
        }

        public override void AddWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            switch (CurrentDamageType)
            {
                case DamageType.Plant:
                    heroDamageDataSo.plantDamageMultiplier *= WeaponSkillPercentage;
                    break;
                case DamageType.Water:
                    heroDamageDataSo.waterDamageMultiplier *= WeaponSkillPercentage;
                    break;
                case DamageType.Fire:
                    heroDamageDataSo.fireDamageMultiplier *= WeaponSkillPercentage;
                    break;
                case DamageType.Holy:
                    heroDamageDataSo.holyDamageMultiplier *= WeaponSkillPercentage;
                    break;
                case DamageType.Lightning:
                    heroDamageDataSo.lightningDamageMultiplier *= WeaponSkillPercentage;
                    break;
                default:
                    Debug.LogWarning("ElementalDmgPercentage Boş Geldi");
                    break;
            }
        }

        public override void RemoveWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            switch (CurrentDamageType)
            {
                case DamageType.Plant:
                    heroDamageDataSo.plantDamageMultiplier = RemovePercentage(heroDamageDataSo.plantDamageMultiplier);
                    break;
                case DamageType.Water:
                    heroDamageDataSo.waterDamageMultiplier = RemovePercentage(heroDamageDataSo.waterDamageMultiplier);
                    break;
                case DamageType.Fire:
                    heroDamageDataSo.fireDamageMultiplier = RemovePercentage(heroDamageDataSo.fireDamageMultiplier);
                    break;
                case DamageType.Holy:
                    heroDamageDataSo.holyDamageMultiplier = RemovePercentage(heroDamageDataSo.holyDamageMultiplier);
                    break;
                case DamageType.Lightning:
                    heroDamageDataSo.lightningDamageMultiplier =
                        RemovePercentage(heroDamageDataSo.lightningDamageMultiplier);
                    break;
                default:
                    Debug.LogWarning("ElementalDmgPercentage Boş Geldi");
                    break;
            }
        }

        public override string GetDescription()
        {
            string elementIcon = String.Empty;
            switch (CurrentDamageType)
            {
                case DamageType.Plant:
                    elementIcon = "<sprite=4>";
                    break;
                case DamageType.Water:
                    elementIcon = "<sprite=2>";
                    break;
                case DamageType.Fire:
                    elementIcon = "<sprite=1>";
                    break;
                case DamageType.Holy:
                    elementIcon = "<sprite=4>";
                    break;
                case DamageType.Lightning:
                    elementIcon = "<sprite=3>";
                    break;
                default:
                    Debug.LogWarning("ElementalDmg GetDescription Boş Geldi");
                    break;
            }
            
            return $"Increase +{WeaponSkillPercentage}% to {elementIcon} Dmg";
        }

        private double RemovePercentage(double attackAmount)
        {
            var result = attackAmount - (attackAmount * WeaponSkillPercentage);
            return result;
        }
    }
}