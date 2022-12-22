using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIRow : EnhancedScrollerCellView
    {
        [SerializeField]
        protected GameObject parentObject;

        [SerializeField]
        protected Image icon;

        [SerializeField]
        protected TextMeshProUGUI levelText;

        [SerializeField]
        protected TextMeshProUGUI descriptionText;

        [SerializeField]
        protected Button buyButton;

        [SerializeField]
        protected TextMeshProUGUI buttonCostText;

        [SerializeField]
        protected TextMeshProUGUI buttonDescriptionText;

        [SerializeField]
        protected Image buyButtonImage;

        [SerializeField]
        protected Sprite activeButtonSprite;

        [SerializeField]
        protected Sprite deActiveButtonSprite;

        public abstract void SetUIRow(UpgradableStat upgradableStat);

        public abstract void FillUIRow();

        public abstract void SetButtonState(double totalCoin);

        public abstract void OnBuy();

        public abstract void UpdateRow(double totalCoin);
    }
}