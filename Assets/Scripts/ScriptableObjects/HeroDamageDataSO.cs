using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Hero Damage Data", fileName = "HeroDamageDataSO")]
    public class HeroDamageDataSO : ScriptableObject
    {
        public double heroAttack;
        public int attackCooldown;
        public float currentRageAmount;

        public double tapAttack;
        public int tapAttackCoolDown;

        public int autoTapAttackDuration;
        public int autoTapAttackCooldown;

        public int goldenTapDuration;
        public int goldenTapCooldown;

        public float rageAmount;
        public int rageDuration;
        public int rageCoolDown;

        public float criticalAttackMultiplier;
        public float criticalAttackChance;

        public double lightningSpecialAttackMultiplier;
        public double fireSpecialAttackMultiplier;
        public double waterSpecialAttackMultiplier;
        public double holySpecialAttackMultiplier;
        public double plantSpecialAttackMultiplier;

        public int lightningSpecialAttackCoolDown;
        public int fireSpecialAttackCoolDown;
        public int waterSpecialAttackCoolDown;
        public int holySpecialAttackCoolDown;
        public int plantSpecialAttackCoolDown;

        public double earthDamageMultiplier;
        public double plantDamageMultiplier;
        public double waterDamageMultiplier;
        public double holyDamageMultiplier;
        public double fireDamageMultiplier;

        public bool isCriticalTapActive = false;
        public bool isAddTimeToDamageActive = false;
        public bool isAddClickCountToDPS;

        public int bossDurationBonus;

        public double passiveGoldAmount;

        public int GetCoolDownBySpecialAttackType(SpecialAttackType specialAttackType)
        {
            switch (specialAttackType)
            {
                case SpecialAttackType.Lightning:
                    return lightningSpecialAttackCoolDown;
                case SpecialAttackType.Explosion:
                    return fireSpecialAttackCoolDown;
                case SpecialAttackType.IceAttack:
                    return waterSpecialAttackCoolDown;
                case SpecialAttackType.AutoTap:
                    return autoTapAttackCooldown;
                case SpecialAttackType.Holy:
                    return holySpecialAttackCoolDown;
                case SpecialAttackType.GoldenTap:
                    return goldenTapCooldown;
                case SpecialAttackType.Rage:
                    return rageCoolDown;
                case SpecialAttackType.PlantAttack:
                    return plantSpecialAttackCoolDown;
                default:
                    Debug.LogError("GetCoolDownBySpecialAttackType is Null");
                    return 1500;
            }
        }

        public int GetDurationBySpecialAttackType(SpecialAttackType specialAttackType)
        {
            switch (specialAttackType)
            {
                case SpecialAttackType.AutoTap:
                    return autoTapAttackDuration;
                case SpecialAttackType.GoldenTap:
                    return goldenTapDuration;
                case SpecialAttackType.Rage:
                    return rageDuration;
                default:
                    Debug.LogError("GetDurationBySpecialAttackType is Null");
                    return 1500;
            }
        }

        public void ResetHeroDamageDataSO(HeroDamageDataSO baseHeroDamageDataSo)
        {
            heroAttack = baseHeroDamageDataSo.heroAttack;
            attackCooldown = baseHeroDamageDataSo.attackCooldown;
            currentRageAmount = baseHeroDamageDataSo.currentRageAmount;

            tapAttack = baseHeroDamageDataSo.tapAttack;
            tapAttackCoolDown = baseHeroDamageDataSo.tapAttackCoolDown;

            autoTapAttackDuration = baseHeroDamageDataSo.autoTapAttackDuration;
            autoTapAttackCooldown = baseHeroDamageDataSo.autoTapAttackCooldown;

            goldenTapDuration = baseHeroDamageDataSo.goldenTapDuration;
            goldenTapCooldown = baseHeroDamageDataSo.goldenTapCooldown;

            rageAmount = baseHeroDamageDataSo.rageAmount;
            rageDuration = baseHeroDamageDataSo.rageDuration;
            rageCoolDown = baseHeroDamageDataSo.rageCoolDown;

            criticalAttackMultiplier = baseHeroDamageDataSo.criticalAttackMultiplier;
            criticalAttackChance = baseHeroDamageDataSo.criticalAttackChance;

            lightningSpecialAttackMultiplier = baseHeroDamageDataSo.lightningSpecialAttackMultiplier;
            fireSpecialAttackMultiplier = baseHeroDamageDataSo.fireSpecialAttackMultiplier;
            waterSpecialAttackMultiplier = baseHeroDamageDataSo.waterSpecialAttackMultiplier;
            holySpecialAttackMultiplier = baseHeroDamageDataSo.holySpecialAttackMultiplier;
            plantSpecialAttackMultiplier = baseHeroDamageDataSo.plantSpecialAttackMultiplier;

            lightningSpecialAttackCoolDown = baseHeroDamageDataSo.lightningSpecialAttackCoolDown;
            fireSpecialAttackCoolDown = baseHeroDamageDataSo.fireSpecialAttackCoolDown;
            waterSpecialAttackCoolDown = baseHeroDamageDataSo.waterSpecialAttackCoolDown;
            holySpecialAttackCoolDown = baseHeroDamageDataSo.holySpecialAttackCoolDown;
            plantSpecialAttackCoolDown = baseHeroDamageDataSo.plantSpecialAttackCoolDown;

            earthDamageMultiplier = baseHeroDamageDataSo.earthDamageMultiplier;
            plantDamageMultiplier = baseHeroDamageDataSo.plantDamageMultiplier;
            waterDamageMultiplier = baseHeroDamageDataSo.waterDamageMultiplier;
            holyDamageMultiplier = baseHeroDamageDataSo.holyDamageMultiplier;
            fireDamageMultiplier = baseHeroDamageDataSo.fireDamageMultiplier;

            isCriticalTapActive = false;
            isAddTimeToDamageActive = false;
            isAddClickCountToDPS = false;

            bossDurationBonus = baseHeroDamageDataSo.bossDurationBonus;

            passiveGoldAmount = baseHeroDamageDataSo.passiveGoldAmount;
        }
    }
}