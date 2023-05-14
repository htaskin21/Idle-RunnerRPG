using System;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Weapon.WeaponSkills
{
    public class UltiMultiplier : WeaponSkill
    {
        public DamageType CurrentDamageType;

        public UltiMultiplier(float percentage)
        {
            CurrentDamageType = GetDamageType();
            WeaponSkillPercentage = percentage;
        }

        public override void AddWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            switch (CurrentDamageType)
            {
                case DamageType.Plant:
                    heroDamageDataSo.plantSpecialAttackMultiplier += WeaponSkillPercentage;
                    break;
                case DamageType.Water:
                    heroDamageDataSo.waterSpecialAttackMultiplier += WeaponSkillPercentage;
                    break;
                case DamageType.Fire:
                    heroDamageDataSo.fireSpecialAttackMultiplier += WeaponSkillPercentage;
                    break;
                case DamageType.Holy:
                    heroDamageDataSo.holySpecialAttackMultiplier += WeaponSkillPercentage;
                    break;
                case DamageType.Lightning:
                    heroDamageDataSo.lightningSpecialAttackMultiplier += WeaponSkillPercentage;
                    break;
                default:
                    Debug.LogWarning("Ulti Multiplier Boş Geldi");
                    break;
            }
        }

        public override void RemoveWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            switch (CurrentDamageType)
            {
                case DamageType.Plant:
                    heroDamageDataSo.plantSpecialAttackMultiplier -= WeaponSkillPercentage;
                    break;
                case DamageType.Water:
                    heroDamageDataSo.waterSpecialAttackMultiplier -= WeaponSkillPercentage;
                    break;
                case DamageType.Fire:
                    heroDamageDataSo.fireSpecialAttackMultiplier -= WeaponSkillPercentage;
                    break;
                case DamageType.Holy:
                    heroDamageDataSo.holySpecialAttackMultiplier -= WeaponSkillPercentage;
                    break;
                case DamageType.Lightning:
                    heroDamageDataSo.lightningSpecialAttackMultiplier -= WeaponSkillPercentage;
                    break;
                default:
                    Debug.LogWarning("Ulti Multiplier Boş Geldi");
                    break;
            }
        }

        public override string GetDescription()
        {
            string elementIcon = String.Empty;
            switch (CurrentDamageType)
            {
                case DamageType.Plant:
                    elementIcon = "<sprite=14>";
                    break;
                case DamageType.Water:
                    elementIcon = "<sprite=15>";
                    break;
                case DamageType.Fire:
                    elementIcon = "<sprite=16>";
                    break;
                case DamageType.Holy:
                    elementIcon = "<sprite=12>";
                    break;
                case DamageType.Lightning:
                    elementIcon = "<sprite=13>";
                    break;
                default:
                    Debug.LogWarning("ElementalDmg GetDescription Boş Geldi");
                    break;
            }
            return $"+{WeaponSkillPercentage}x to {elementIcon}";
        }
    }
}