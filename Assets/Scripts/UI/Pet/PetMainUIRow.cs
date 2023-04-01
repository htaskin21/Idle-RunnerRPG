using ScriptableObjects;
using UnityEngine;
using Utils;

namespace UI.Pet
{
    public class PetMainUIRow : PetUIRow
    {
        [SerializeField]
        private Sprite _defaultIcon;

        public PetSO _currentPet;

        private protected override void Start()
        {
            
        }

        public void SetMainUIRow(PetSO pet)
        {
            _currentPet = pet;

            icon.color = Color.white;
            icon.sprite = pet.icon;

            var stringBuilder = DescriptionUtils.GetDescription(pet.PetSkill);
            descriptionText.text = stringBuilder.ToString();

            _takeOffPetButton.gameObject.SetActive(true);
        }

        public void ResetMainUIRow()
        {
            _currentPet = null;

            icon.color = Color.black;
            icon.sprite = _defaultIcon;

            var stringBuilder = DescriptionUtils.GetDescription(null);
            descriptionText.text = stringBuilder.ToString();

            _takeOffPetButton.gameObject.SetActive(false);
        }
    }
}