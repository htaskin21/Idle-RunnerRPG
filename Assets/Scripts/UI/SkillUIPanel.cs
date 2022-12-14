using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace UI
{
    public class SkillUIPanel : UIPanel, IEnhancedScrollerDelegate
    {
        [SerializeField]
        private EnhancedScroller enhancedScroller;

        [SerializeField]
        private EnhancedScrollerCellView enhancedScrollerCellView;

        public GameObject panelObject;

        private List<SkillUpgrade> _skillUis;

        private void Start()
        {
            enhancedScroller.Delegate = this;
        }

        public void LoadData(List<SkillUpgrade> skillUpgrades)
        {
            _skillUis = new List<SkillUpgrade>();
            _skillUis = skillUpgrades;

            enhancedScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _skillUis.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 200f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            SkillUIRow skillUIRow = enhancedScroller.GetCellView(enhancedScrollerCellView) as SkillUIRow;

            skillUIRow.SetSkillUIRow(_skillUis[dataIndex]);

            return skillUIRow;
        }
    }
}