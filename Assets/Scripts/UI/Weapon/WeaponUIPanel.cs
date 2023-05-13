using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using Managers;
using UnityEngine;
using Weapon;

namespace UI.Weapon
{
    public class WeaponUIPanel : UIPanel, IEnhancedScrollerDelegate
    {
        [SerializeField]
        private EnhancedScroller enhancedScroller;

        [SerializeField]
        private EnhancedScrollerCellView enhancedScrollerCellView;

        [SerializeField]
        private List<WeaponMainUIRow> _mainUIRows;

        public List<WeaponUIRow> _weaponUIRows;

        private List<global::Weapon.Weapon> _weapons;

        public override void Start()
        {
            LoadData();
            SetMainRows();

            WeaponManager.OnEquipWeapon += SetMainRow;
            WeaponManager.OnTakeOffWeapon += ResetMainRow;

            WeaponManager.OnGetWeapon += LoadData;
            WeaponManager.OnSellWeapon += XX;

            enhancedScroller.Delegate = this;
            base.Start();
        }

        private void OnEnable()
        {
            LoadData();
        }

        private void XX()
        {
            LoadData();
        }

        public void LoadData()
        {
            _weapons = new List<global::Weapon.Weapon>();
            _weapons = SaveLoadManager.Instance.LoadWeapons();

            enhancedScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _weapons.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 200f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            WeaponUIRow weaponUIRow =
                enhancedScroller.GetCellView(enhancedScrollerCellView) as WeaponUIRow;

            var data = SaveLoadManager.Instance.LoadSelectedWeapons();
            bool isEquipped = data.FirstOrDefault(x => x.id == _weapons[dataIndex].id) != null;

            weaponUIRow.SetUIRow(_weapons[dataIndex], isEquipped);
            _weaponUIRows.Add(weaponUIRow);

            return weaponUIRow;
        }

        private void SetMainRows()
        {
            var data = SaveLoadManager.Instance.LoadSelectedWeapons();
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var weapon = _weapons.FirstOrDefault(x => x.id == data[i].id);
                    SetMainRow(weapon);
                    WeaponManager.OnEquipWeapon.Invoke(weapon);
                }
            }
        }

        private void SetMainRow(global::Weapon.Weapon weapon)
        {
            var emptyMainRow = _mainUIRows.FirstOrDefault(x => x.CurrentWeapon == null);
            emptyMainRow.SetMainUIRow(weapon);
        }

        private void ResetMainRow(global::Weapon.Weapon weapon)
        {
            var mainRow = _mainUIRows.FirstOrDefault(x => x.CurrentWeapon == weapon);
            mainRow.ResetMainUIRow();
        }

        public void SetAddButtonStatus()
        {
            if (_mainUIRows.TrueForAll(x => x.CurrentWeapon != null))
            {
                _weaponUIRows.ForEach(x => x.ToggleAddButton(false));
            }
            else
            {
                _weaponUIRows.ForEach(x => x.ToggleAddButton(true));
            }
        }
    }
}