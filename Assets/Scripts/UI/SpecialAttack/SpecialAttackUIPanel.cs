using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using Enums;
using SpecialAttacks;
using UnityEngine;

namespace UI.SpecialAttack
{
    public class SpecialAttackUIPanel : UIPanel, IEnhancedScrollerDelegate
    {
        [SerializeField]
        private EnhancedScroller enhancedScroller;

        [SerializeField]
        private EnhancedScrollerCellView enhancedScrollerCellView;

        private List<SpecialAttackUpgrade> _specialAttackUpgrades;

        public override void Start()
        {
            enhancedScroller.Delegate = this;
            base.Start();
        }

        public void LoadData(List<SpecialAttackUpgrade> specialAttackUpgrades)
        {
            _specialAttackUpgrades = new List<SpecialAttackUpgrade>();
            _specialAttackUpgrades = specialAttackUpgrades;

            var baseHeroSkill = _specialAttackUpgrades.FirstOrDefault(x => x.SkillTypes == SkillTypes.BaseHeroSkill);
            _specialAttackUpgrades.Remove(baseHeroSkill);

            enhancedScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _specialAttackUpgrades.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 200f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            SpecialAttackUIRow specialAttackUIRow =
                enhancedScroller.GetCellView(enhancedScrollerCellView) as SpecialAttackUIRow;

            specialAttackUIRow.SetUIRow(_specialAttackUpgrades[dataIndex]);

            return specialAttackUIRow;
        }
    }
}