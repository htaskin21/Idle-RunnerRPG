using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillUIRow : MonoBehaviour
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
            descriptionText.text = skillUI.SkillTypes.ToString();
      
        }
    }
}