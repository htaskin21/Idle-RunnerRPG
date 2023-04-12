using System.Linq;
using Enums;
using Skill;
using UnityEngine;

namespace UI
{
    public class HeroUIRow : SkillUIRow
    {
        [SerializeField]
        private DataReader _dataReader;

        private void Start()
        {
            var heroSkillUpgrade = _dataReader.SkillData.FirstOrDefault(x => x.SkillTypes == SkillTypes.BaseHeroSkill);
            SetUIRow(heroSkillUpgrade);

            UIManager.OnUpdateCoinHud += UpdateRow;
        }
    }
}