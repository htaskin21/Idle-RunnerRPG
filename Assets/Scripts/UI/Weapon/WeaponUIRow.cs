using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Weapon
{
    public class WeaponUIRow : UIRow
    {
        [SerializeField]
        private List<TextMeshProUGUI> _weaponDescriptionTexts;

        private global::Weapon.Weapon _weapon;


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
            throw new NotImplementedException();
        }

        public override void SetButtonState(double totalGem)
        {
            throw new NotImplementedException();
        }

        public override void OnBuy()
        {
            throw new NotImplementedException();
        }
    }
}