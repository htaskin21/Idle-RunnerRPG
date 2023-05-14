using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

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

        public void SetMainUIRow(global::Weapon.Weapon weapon)
        {
            _weapon = weapon;

            icon.color = Color.white;
            icon.sprite = _weaponIconData.Icons[_weapon.WeaponSpriteID];

            _weaponDescriptionTexts.ForEach(x => x.gameObject.SetActive(false));

            for (int i = 0; i < weapon.WeaponSkills.Length; i++)
            {
                var desc = weapon.WeaponSkills[i].GetDescription();
                _weaponDescriptionTexts[i].text = desc;
                _weaponDescriptionTexts[i].gameObject.SetActive(true);
            }

            _takeOffWeaponButton.gameObject.SetActive(true);
        }

        public void ResetMainUIRow()
        {
            _weapon = null;

            icon.color = Color.black;
            icon.sprite = _defaultIcon;
            _iconFrame.enabled = false;

            _weaponDescriptionTexts.ForEach(x => x.gameObject.SetActive(false));
            _weaponDescriptionTexts[0].text = "EMPTY SLOT";
            _weaponDescriptionTexts[0].gameObject.SetActive(true);

            _takeOffWeaponButton.gameObject.SetActive(false);
            _weaponUIPanel.SetAddButtonStatus();
        }

        public override void OnTakeOff()
        {
            SaveLoadManager.Instance.SaveSelectedWeapon(_weapon, false);
            _weaponUIPanel._weaponUIRows.FirstOrDefault(x => x.cellIdentifier == _weapon.id.ToString())
                .ToggleAddButton(true);
            WeaponManager.OnTakeOffWeapon.Invoke(_weapon);
        }
    }
}