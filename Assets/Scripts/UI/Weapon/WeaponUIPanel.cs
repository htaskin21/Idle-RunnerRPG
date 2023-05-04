using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

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

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            throw new System.NotImplementedException();
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            throw new System.NotImplementedException();
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}
