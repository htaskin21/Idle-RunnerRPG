using ScriptableObjects;

namespace Weapon.WeaponSkills
{
    public class DPSPercentage : WeaponSkill
    {
        public override void AddWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.heroAttack *= WeaponSkillPercentage;
        }

        public override void RemoveWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            var attackAmountWithPercentage = heroDamageDataSo.heroAttack;

            var attackAmount = attackAmountWithPercentage - (attackAmountWithPercentage * WeaponSkillPercentage);
            heroDamageDataSo.heroAttack = attackAmount;
        }

        public override string GetDescription()
        {
            return $"Increase +{WeaponSkillPercentage}% to DPS";
        }
    }
}