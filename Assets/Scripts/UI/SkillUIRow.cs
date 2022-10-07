using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            descriptionText.text = skillUI.SkillTypes.ToString();
        }
    }
}