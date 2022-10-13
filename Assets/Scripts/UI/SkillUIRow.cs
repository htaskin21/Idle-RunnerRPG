using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class SkillUIRow : EnhancedScrollerCellView
    {
        [SerializeField]
        private GameObject parentObject;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private Button buyButton;

        public void SetSkillUIRow(SkillUI skillUI)
        {
            cellIdentifier = skillUI.ID.ToString();

            var damage = CalcUtils.FormatNumber(skillUI.BaseIncrementAmount);
            var stringBuilder = DescriptionUtils.GetDescription(skillUI.SkillTypes);
            stringBuilder.Replace("x", damage);

            descriptionText.text = stringBuilder.ToString();
        }
    }
}