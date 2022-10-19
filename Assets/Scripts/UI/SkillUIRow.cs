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

        [SerializeField]
        private TextMeshProUGUI buttonCostText;

        [SerializeField]
        private TextMeshProUGUI buttonDescriptionText;

        private SkillUI _skillUI;
        private int _level = 1;
        
        private void Start()
        {
            UIManager.OnUpdateCoinHud += UpdateRow;
        }
        
        public void SetSkillUIRow(SkillUI skillUI)
        {
            _skillUI = skillUI;

            cellIdentifier = skillUI.ID.ToString();

            FillSkillUIRow();
        }

        private void FillSkillUIRow()
        {
            var damage = CalcUtils.FormatNumber(_skillUI.BaseIncrementAmount * _level);
            var stringBuilder = DescriptionUtils.GetDescription(_skillUI.SkillTypes);
            stringBuilder.Replace("x", damage);

            descriptionText.text = stringBuilder.ToString();
            levelText.text = _level.ToString();
        }

        private void SetButtonState(double totalCoin)
        {
            var cost = _skillUI.BaseIncrementCost * _level;
            buttonCostText.text = $"{CalcUtils.FormatNumber(cost)}<sprite index= 11> " ;

            buttonDescriptionText.text = _level > 1 ? "LEVEL UP" : "BUY";

            buyButton.enabled = cost < totalCoin;
        }

        public void OnBuy()
        {
            var cost = _skillUI.BaseIncrementCost * _level;
            _level++;
            EconomyManager.OnSpendCoin.Invoke(cost);
        }

        private void UpdateRow(double totalCoin)
        {
            FillSkillUIRow();
            SetButtonState(totalCoin);
        }

        public override void RefreshCellView()
        {
            base.RefreshCellView();
        }
    }
}