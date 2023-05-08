using System;
using System.Collections.Generic;
using Enums;
using Managers;
using ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Weapon.WeaponSkills;
using Random = UnityEngine.Random;

namespace Weapon
{
    public class WeaponCreator : SerializedMonoBehaviour
    {
        [OdinSerialize]
        private List<WeaponSkill> _weaponSkills;

        [SerializeField]
        private IconDataSO weaponIconData;


        private WeaponSkill[] GetWeaponSkills(WeaponRarityType weaponRarityType)
        {
            var heroLevel = 0;

            var _skillUpgradeDictionary = SaveLoadManager.Instance.LoadSkillUpgrade();

            var level = _skillUpgradeDictionary[0];


            WeaponSkill[] weaponSkills;

            switch (weaponRarityType)
            {
                case WeaponRarityType.Common:
                    return weaponSkills = new[] {new UltiMultiplier(heroLevel + 2)};

                case WeaponRarityType.Rare:
                    return weaponSkills = new[] {new UltiMultiplier(heroLevel + 3), new UltiMultiplier(heroLevel + 4)};

                case WeaponRarityType.Epic:
                    return weaponSkills = new[]
                    {
                        new UltiMultiplier(heroLevel + 5), new UltiMultiplier(heroLevel + 6),
                        new UltiMultiplier(heroLevel + 7)
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Weapon CreateWeapon(WeaponRarityType weaponRarityType)
        {
            var iconCount = weaponIconData.Icons.Keys.Count;
            var rnd = Random.Range(0, iconCount);

            var w = new Weapon(weaponRarityType, GetWeaponSkills(weaponRarityType), weaponIconData.Icons[rnd]);

            return w;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                var a = CreateWeapon(WeaponRarityType.Common);
                SaveLoadManager.Instance.SaveWeapon(a);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                var a = SaveLoadManager.Instance.LoadWeapons();
                var z = a.Count;
            }
        }
    }
}