using System.Linq;
using Enums;
using Managers;
using Skill;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeroUIRow : SkillUIRow
    {
        [SerializeField]
        private DataReader _dataReader;

        [SerializeField]
        private Button _levelUpButton;

        private void Start()
        {
            var heroSkillUpgrade = _dataReader.SkillData.FirstOrDefault(x => x.SkillTypes == SkillTypes.BaseHeroSkill);
            SetUIRow(heroSkillUpgrade);

            UIManager.OnUpdateCoinHud += UpdateRow;

            _levelUpButton.onClick.AddListener(SaveHighestHeroLevel);
        }

        private void SaveHighestHeroLevel()
        {
            var skills = SaveLoadManager.Instance.LoadSkillUpgrade();
            var heroLevel = skills[0];

            SaveLoadManager.Instance.SaveHighestHeroLevel(heroLevel);
        }
    }
}