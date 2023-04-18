using System.Linq;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Pet
{
    public class PetMainUIRow : PetUIRow
    {
        [SerializeField]
        private Sprite _defaultIcon;

        [SerializeField]
        private PetUIPanel _petUIPanel;

        [SerializeField]
        private Image _iconFrame;

        public PetSO CurrentPet => _pet;

        private protected override void Start()
        {
        }

        public void SetMainUIRow(PetSO pet)
        {
            _pet = pet;

            icon.color = Color.white;
            icon.sprite = pet.icon;
            _iconFrame.enabled = true;

            var stringBuilder = DescriptionUtils.GetDescription(pet.PetSkill);
            descriptionText.text = stringBuilder.ToString();

            _takeOffPetButton.gameObject.SetActive(true);
            _petUIPanel.SetAddButtonStatus();
        }

        public void ResetMainUIRow()
        {
            _pet = null;

            icon.color = Color.black;
            icon.sprite = _defaultIcon;
            _iconFrame.enabled = false;

            var stringBuilder = DescriptionUtils.GetDescription(null);
            descriptionText.text = stringBuilder.ToString();

            _takeOffPetButton.gameObject.SetActive(false);
            _petUIPanel.SetAddButtonStatus();
        }

        public override void OnTakeOff()
        {
            SaveLoadManager.Instance.SaveSelectedPetData(_pet.id, false);
            _pet.PetSkill.RemoveSkill(_pet.heroDamageDataSo);
            _petUIPanel._petUIRows.FirstOrDefault(x => x.cellIdentifier == _pet.id.ToString()).ActivateAddPetButton();
            PetManager.OnTakeOffPet.Invoke(_pet);
        }
    }
}