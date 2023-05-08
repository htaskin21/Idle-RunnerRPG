using ScriptableObjects;

namespace Weapon.WeaponSkills
{
    public class TapPercentage : WeaponSkill
    {
        public override void AddWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            heroDamageDataSo.tapAttack *= WeaponSkillPercentage;
        }

        public override void RemoveWeapon(HeroDamageDataSO heroDamageDataSo)
        {
            var attackAmountWithPercentage = heroDamageDataSo.tapAttack;

            var attackAmount = attackAmountWithPercentage - (attackAmountWithPercentage * WeaponSkillPercentage);
            heroDamageDataSo.tapAttack = attackAmount;
        }

        public override string GetDescription()
        {
            return $"Increase +{WeaponSkillPercentage}% to Tap Dmg";
        }
    }
}