using Enums;
using Weapon.WeaponSkills;

namespace Weapon
{
    public class Weapon
    {
        public WeaponRarityType WeaponRarityType;

        public WeaponSkill[] WeaponSkills;

        public int WeaponSpriteID;

        public Weapon(WeaponRarityType weaponRarityType, WeaponSkill[] weaponSkills, int weaponSpriteID)
        {
            WeaponRarityType = weaponRarityType;
            WeaponSkills = weaponSkills;
            WeaponSpriteID = weaponSpriteID;
        }
    }
}