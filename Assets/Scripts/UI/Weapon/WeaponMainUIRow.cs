using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Weapon
{
    public class WeaponMainUIRow : WeaponUIRow
    {
        [SerializeField]
        private Sprite _defaultIcon;

        [SerializeField]
        private WeaponUIPanel _weaponUIPanel;

        [SerializeField]
        private Image _iconFrame;

        public global::Weapon.Weapon CurrentWeapon => _weapon;

        private protected virtual void Start()
        {
        }

        public void SetMainUIRow(global::Weapon.Weapon weapon)
        {
            _weapon = weapon;

            icon.color = Color.white;
            icon.sprite = weapon.WeaponSprite;
            _iconFrame.enabled = true;

            for (int i = 0; i < weapon.WeaponSkills.Length; i++)
            {
                var desc = weapon.WeaponSkills[i].GetDescription();
                _weaponDescriptionTexts[i].text = desc;
                _weaponDescriptionTexts[i].gameObject.SetActive(true);
            }

            _takeOffWeaponButton.gameObject.SetActive(true);
            _weaponUIPanel.SetAddButtonStatus();
        }

        public void ResetMainUIRow()
        {
            _weapon = null;

            icon.color = Color.black;
            icon.sprite = _defaultIcon;
            _iconFrame.enabled = false;

            _weaponDescriptionTexts.ForEach(x => gameObject.SetActive(false));

            _takeOffWeaponButton.gameObject.SetActive(false);
            _weaponUIPanel.SetAddButtonStatus();
        }

        public override void OnTakeOff()
        {
            SaveLoadManager.Instance.SaveSelectedWeapon(_weapon, false);
            //_pet.PetSkill.RemoveSkill(_pet.heroDamageDataSo);
            //_petUIPanel._petUIRows.FirstOrDefault(x => x.cellIdentifier == _pet.id.ToString()).ActivateAddPetButton();
            //PetManager.OnTakeOffPet.Invoke(_pet);
        }
    }
}