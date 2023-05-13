using System;
using Enums;
using Weapon.WeaponSkills;

namespace Weapon
{
    public class Weapon
    {
        public Guid id;

        public WeaponRarityType WeaponRarityType;

        public WeaponSkill[] WeaponSkills;

        public int WeaponSpriteID;

        public int Cost
        {
            get
            {
                switch (WeaponRarityType)
                {
                    case WeaponRarityType.Common:
                        return 1;
                    case WeaponRarityType.Rare:
                        return 4;
                    case WeaponRarityType.Epic:
                        return 16;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Weapon(WeaponRarityType weaponRarityType, WeaponSkill[] weaponSkills, int weaponSpriteID)
        {
            id = Guid.NewGuid();
            WeaponRarityType = weaponRarityType;
            WeaponSkills = weaponSkills;
            WeaponSpriteID = weaponSpriteID;
        }
    }
}