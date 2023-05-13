using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace UI.Weapon
{
    public class WeaponUIRow : UIRow
    {
        [SerializeField]
        protected List<TextMeshProUGUI> _weaponDescriptionTexts;

        protected global::Weapon.Weapon _weapon;

        private List<global::Weapon.Weapon> _weapons;

        [SerializeField]
        protected Button _addWeaponButton;

        [SerializeField]
        protected Button _takeOffWeaponButton;

        [SerializeField]
        protected Button _sellWeaponButton;

        [SerializeField]
        private TextMeshProUGUI _sellWeaponCostText;

        [SerializeField]
        [CanBeNull]
        protected IconDataSO _weaponIconData;

        private bool isSelled;


        public override void SetUIRow(global::Weapon.Weapon weapon, bool isEquipped)
        {
            _weapon = weapon;
            cellIdentifier = weapon.id.ToString();

            icon.sprite = _weaponIconData.Icons[_weapon.WeaponSpriteID];

            _sellWeaponCostText.text = $"{_weapon.Cost} <sprite=7>";

            for (int i = 0; i < weapon.WeaponSkills.Length; i++)
            {
                var desc = weapon.WeaponSkills[i].GetDescription();
                _weaponDescriptionTexts[i].text = desc;
                _weaponDescriptionTexts[i].gameObject.SetActive(true);
            }

            ToggleAddButton(!isEquipped);
        }

        public override void FillUIRow()
        {
            throw new NotImplementedException();
        }

        public override void SetButtonState(double totalGem = 0)
        {
            DisableAllButtons();

            _addWeaponButton.gameObject.SetActive(true);
            _sellWeaponButton.gameObject.SetActive(true);
        }

        public override void OnBuy()
        {
            throw new NotImplementedException();
        }

        private void DisableAllButtons()
        {
            _addWeaponButton.gameObject.SetActive(false);
            _takeOffWeaponButton.gameObject.SetActive(false);
            _sellWeaponButton.gameObject.SetActive(false);
        }

        public void OnEquip()
        {
            SaveLoadManager.Instance.SaveSelectedWeapon(_weapon, true);
            ToggleAddButton(false);

            WeaponManager.OnEquipWeapon.Invoke(_weapon);
        }

        public virtual void OnTakeOff()
        {
            SaveLoadManager.Instance.SaveSelectedWeapon(_weapon, false);

            _addWeaponButton.gameObject.SetActive(true);
            _sellWeaponButton.gameObject.SetActive(true);

            WeaponManager.OnTakeOffWeapon.Invoke(_weapon);
        }

        public void ToggleAddButton(bool status)
        {
            _takeOffWeaponButton.gameObject.SetActive(!status);
            _addWeaponButton.gameObject.SetActive(status);
            _sellWeaponButton.gameObject.SetActive(status);
        }

        public void OnSell()
        {
            SaveLoadManager.Instance.RemoveWeapon(_weapon);
            EconomyManager.OnCollectGem.Invoke(_weapon.Cost);

            isSelled = true;
            this.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (isSelled)
            {
                isSelled = false;
                WeaponManager.OnSellWeapon.Invoke();
            }
        }
    }
}