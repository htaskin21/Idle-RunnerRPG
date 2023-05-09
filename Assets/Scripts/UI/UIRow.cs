using EnhancedUI.EnhancedScroller;
using ScriptableObjects;
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

        public virtual void SetUIRow(UpgradableStat upgradableStat)
        {
        }

        public virtual void SetUIRow(PetSO pet)
        {
        }

        public virtual void SetUIRow(global::Weapon.Weapon weapon)
        {
        }


        public abstract void FillUIRow();

        public abstract void SetButtonState(double totalGem);

        public abstract void OnBuy();

        public virtual void UpdateRow(double totalCoin)
        {
        }

        public virtual void UpdateRow(int totalGem)
        {
        }
        
        public virtual void UpdateRow()
        {
        }
    }
}