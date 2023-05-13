using System;
using ScriptableObjects;
using UI.Weapon;
using UnityEngine;

namespace Weapon
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField]
        private WeaponUIPanel _weaponUIPanel;

        [SerializeField]
        private HeroDamageDataSO _heroDamageDataSo;

        public static Action<Weapon> OnEquipWeapon;
        public static Action<Weapon> OnTakeOffWeapon;

        private void Awake()
        {
            OnEquipWeapon = delegate(Weapon weapon) { };
            OnTakeOffWeapon = delegate(Weapon weapon) { };

            OnEquipWeapon += AddWeaponEffect;
            OnTakeOffWeapon += RemoveWeaponEffect;
        }

        void Start()
        {
            _weaponUIPanel.LoadData();
        }

        private void AddWeaponEffect(Weapon weapon)
        {
            foreach (var weaponSkill in weapon.WeaponSkills)
            {
                weaponSkill.AddWeapon(_heroDamageDataSo);
            }
        }

        private void RemoveWeaponEffect(Weapon weapon)
        {
            foreach (var weaponSkill in weapon.WeaponSkills)
            {
                weaponSkill.RemoveWeapon(_heroDamageDataSo);
            }
        }
    }
}