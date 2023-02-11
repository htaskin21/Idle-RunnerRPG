using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items.Potion
{
    public abstract class Potion : MonoBehaviour
    {
        [SerializeField]
        protected PotionDataSO _potionData;

        [SerializeField]
        protected Image _icon;

        [SerializeField]
        protected TextMeshProUGUI _amountText;

        public abstract void UsePotion();

        protected virtual void SetData()
        {
            int amount = SaveLoadManager.Instance.LoadPotion(_potionData._potionType);
            _amountText.text = amount.ToString();

            _icon.sprite = amount > 0 ? _potionData.fullImage : _potionData.emptyImage;
        }
    }
}