using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Hero Damage Data", fileName = "HeroDamageDataSO")]
    public class HeroDamageDataSO : ScriptableObject
    {
        public double heroAttack = 10;
        public int attackCooldown;
        public float currentRageAmount;

        public double tapAttack = 1;
        public int tapAttackCoolDown;

        public int autoTapAttackDuration;
        public int autoTapAttackCooldown;

        public int goldenTapDuration;
        public int goldenTapCooldown;

        public float rageAmount;
        public int rageDuration;
        public int rageCoolDown;

        public float criticalAttack = 5;
        public float criticalAttackChance = 0;

        public double lightningSpecialAttackMultiplier;
        public double fireSpecialAttackMultiplier;
        public double waterSpecialAttackMultiplier;
        public double holySpecialAttackMultiplier;

        public int lightningSpecialAttackCoolDown;
        public int fireSpecialAttackCoolDown;
        public int waterSpecialAttackCoolDown;
        public int holySpecialAttackCoolDown;

        public double earthDamageMultiplier = 1;
        public double plantDamageMultiplier = 1;
        public double waterDamageMultiplier = 1;
        public double holyDamageMultiplier = 1;

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
    }
}