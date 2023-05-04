using System;
using Enums;
using ScriptableObjects;
using Sirenix.Serialization;
using Random = UnityEngine.Random;

namespace Weapon.WeaponSkills
{
    [Serializable]
    public abstract class WeaponSkill : OdinSerializeAttribute
    {
        public float WeaponSkillPercentage;

        public abstract void AddWeapon(HeroDamageDataSO heroDamageDataSo);

        public abstract void RemoveWeapon(HeroDamageDataSO heroDamageDataSo);

        protected DamageType GetDamageType()
        {
            var enumCount = Enum.GetNames(typeof(DamageType)).Length;
            var rnd = Random.Range(0, enumCount);
            DamageType type = (DamageType) rnd;

            if (type is DamageType.Earth or DamageType.Normal)
            {
                while (type is DamageType.Earth or DamageType.Normal)
                {
                    enumCount = Enum.GetNames(typeof(DamageType)).Length;
                    rnd = Random.Range(0, enumCount);
                    type = (DamageType) rnd;
                }
            }

            return type;
        }
    }
}