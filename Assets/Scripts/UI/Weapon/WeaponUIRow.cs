using System;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


        public override void SetUIRow(global::Weapon.Weapon weapon)
        {
            _weapon = weapon;
            cellIdentifier = weapon.id.ToString();

            icon.sprite = _weapon.WeaponSprite;


            for (int i = 0; i < weapon.WeaponSkills.Length; i++)
            {
                var desc = weapon.WeaponSkills[i].GetDescription();
                _weaponDescriptionTexts[i].text = desc;
                _weaponDescriptionTexts[i].gameObject.SetActive(true);
            }

            //var gem = SaveLoadManager.Instance.LoadGem();
            //UpdateRow(gem);
        }

        public override void FillUIRow()
        {
            _weapons = SaveLoadManager.Instance.LoadWeapons();
            SetButtonState();
        }

        public override void SetButtonState(double totalGem = 0)
        {
            DisableAllButtons();

            var selectedWeapons = SaveLoadManager.Instance.LoadSelectedWeapons();

            if (selectedWeapons.Contains(_weapon))
            {
                //_takeOffPetButton.gameObject.SetActive(true);
            }
            else
            {
                _addWeaponButton.gameObject.SetActive(true);
                _sellWeaponButton.gameObject.SetActive(true);
            }
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
            DisableAllButtons();
            //_takeOffPetButton.gameObject.SetActive(true);
            //PetManager.OnEquipPet.Invoke(_pet);
        }

        public virtual void OnTakeOff()
        {
            SaveLoadManager.Instance.SaveSelectedWeapon(_weapon, false);
            //ActivateAddPetButton();
            //PetManager.OnTakeOffPet.Invoke(_pet);
        }
        
        public void ToggleAddButton(bool status)
        {
            _addWeaponButton.enabled = status;
        }

        public override void OnBuy()
        {
            throw new NotImplementedException();
        }

        public override void UpdateRow()
        {
            SetButtonState();
        }
    }
}