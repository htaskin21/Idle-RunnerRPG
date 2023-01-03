using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create HeroDamageDataSO", fileName = "HeroDamageDataSO")]
    public class HeroDamageDataSO :ScriptableObject
    {
        public double heroAttack = 10;
        public int attackCooldown;
    
        public double tapAttack = 1;
        public int tapAttackCoolDown;
        public double autoTapAttackDuration;
        public double autoTapAttackCooldown;

        public float criticalAttack = 5;
        public float criticalAttackChance = 0;

        public double lightningSpecialAttackMultiplier;
        public double FireSpecialAttackMultiplier;
        public double WaterSpecialAttackMultiplier;

        public int specialAttackCoolDown;


        public double earthDamageMultiplier = 1;
        public double plantDamageMultiplier = 1;
        public double waterDamageMultiplier = 1;

    }
}