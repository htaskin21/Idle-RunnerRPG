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
            WeaponSkill[] weaponSkills;

            switch (weaponRarityType)
            {
                case WeaponRarityType.Common:
                    return weaponSkills = new[] {GetRandomWeaponSkill()};

                case WeaponRarityType.Rare:
                    return weaponSkills = new[] {GetRandomWeaponSkill(), GetRandomWeaponSkill()};

                case WeaponRarityType.Epic:
                    return weaponSkills = new[]
                    {
                        GetRandomWeaponSkill(), GetRandomWeaponSkill(),
                        GetRandomWeaponSkill()
                    };
                default:
                    Debug.LogError("Weapon Skills Null Geldi");
                    throw new ArgumentOutOfRangeException();
            }
        }


        private Weapon CreateWeapon(WeaponRarityType weaponRarityType)
        {
            var iconCount = weaponIconData.Icons.Keys.Count;
            var rnd = Random.Range(0, iconCount);

            var w = new Weapon(weaponRarityType, GetWeaponSkills(weaponRarityType), rnd);

            return w;
        }

        private WeaponSkill GetRandomWeaponSkill()
        {
            var totalSkillsCount = _weaponSkills.Count;
            var randomSkillCount = Random.Range(0, totalSkillsCount);

            var percentage = CalculatePercentageByHeroLevel();

            switch (randomSkillCount)
            {
                case 0:
                    return new TapPercentage(percentage);
                case 1:
                    return new UltiMultiplier(percentage);
                case 2:
                    return new ElementalDmgPercentage(percentage);
                case 3:
                    return new DPSPercentage(percentage);
                default:
                    return new TapPercentage(percentage);
            }
        }

        private float CalculatePercentageByHeroLevel()
        {
            var highestHeroLevel = SaveLoadManager.Instance.LoadHighestHeroLevel();
            highestHeroLevel = Mathf.Clamp(highestHeroLevel, 20, 100);

            var result = Random.Range((float) (highestHeroLevel - 20), (float) highestHeroLevel);
            result = (float) Math.Round(result, 2);

            return result;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                var a = CreateWeapon(WeaponRarityType.Common);
                SaveLoadManager.Instance.SaveWeapon(a);
                WeaponManager.OnGetWeapon.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                var a = SaveLoadManager.Instance.LoadWeapons();
                var z = a.Count;
            }
        }
    }
}