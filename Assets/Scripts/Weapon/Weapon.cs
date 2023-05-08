using System;
using Enums;
using UnityEngine;
using Weapon.WeaponSkills;

namespace Weapon
{
    public class Weapon
    {
        public Guid id;

        public WeaponRarityType WeaponRarityType;

        public WeaponSkill[] WeaponSkills;

        public Sprite WeaponSprite;

        public Weapon(WeaponRarityType weaponRarityType, WeaponSkill[] weaponSkills, Sprite weaponSprite)
        {
            id = Guid.NewGuid();
            WeaponRarityType = weaponRarityType;
            WeaponSkills = weaponSkills;
            WeaponSprite = weaponSprite;
        }
    }
}