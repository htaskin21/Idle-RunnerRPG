using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using Enums;
using UI;
using UnityEngine;

namespace Skill
{
    public class SkillUIPanel : UIPanel, IEnhancedScrollerDelegate
    {
        [SerializeField]
        private EnhancedScroller enhancedScroller;

        [SerializeField]
        private EnhancedScrollerCellView enhancedScrollerCellView;

        private List<SkillUpgrade> _skillUis;

        public override void Start()
        {
            var baseHeroSkill = _skillUis.FirstOrDefault(x => x.SkillTypes == SkillTypes.BaseHeroSkill);
            _skillUis.Remove(baseHeroSkill);
            
            enhancedScroller.Delegate = this;
            base.Start();
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

            skillUIRow.SetUIRow(_skillUis[dataIndex]);

            return skillUIRow;
        }
    }
}