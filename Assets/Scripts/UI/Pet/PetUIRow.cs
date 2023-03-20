using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Pet
{
    public class PetUIRow : UIRow
    {
        [SerializeField]
        private PetManager _petManager;

        [SerializeField]
        protected Button _addPetButton;

        [SerializeField]
        protected Button _takeOffPetButton;

        private List<int> _petIDs;
        private PetSO _pet;

        void Start()
        {
            UIManager.OnUpdateGemHud += UpdateRow;
        }

        public override void SetUIRow(PetSO pet)
        {
            _pet = pet;
            cellIdentifier = pet.id.ToString();

            var gem = SaveLoadManager.Instance.LoadGem();
            UpdateRow(gem);
        }

        public override void FillUIRow()
        {
            _petIDs = SaveLoadManager.Instance.LoadPetData();

            var stringBuilder = DescriptionUtils.GetDescription(_pet.PetSkill);
            descriptionText.text = stringBuilder.ToString();

            icon.sprite = _pet.icon;
        }

        public override void SetButtonState(double totalGem)
        {
            buyButton.gameObject.SetActive(false);
            _addPetButton.gameObject.SetActive(false);
            _takeOffPetButton.gameObject.SetActive(false);

            if (_petIDs.Contains(_pet.id))
            {
                var selectedPets = SaveLoadManager.Instance.LoadSelectedPetData();
                if (selectedPets.Contains(_pet.id))
                {
                    _takeOffPetButton.gameObject.SetActive(true);
                }
                else
                {
                    _addPetButton.gameObject.SetActive(true);
                }
            }
            else
            {
                var cost = _pet.cost;
                buttonCostText.text = $"{CalcUtils.FormatNumber(cost)} <sprite index= 7>";

                buyButton.gameObject.SetActive(true);
                buyButton.enabled = cost <= totalGem;
                buyButtonImage.sprite = buyButton.enabled ? activeButtonSprite : deActiveButtonSprite;
            }
        }

        public override void OnBuy()
        {
            var gem = SaveLoadManager.Instance.LoadGem();
            var cost = _pet.cost;
            if (gem >= cost)
            {
                SaveLoadManager.Instance.SavePetData(_pet.id);
                EconomyManager.OnSpendGem.Invoke(-cost);
                gem -= cost;

                UpdateRow(gem);
            }
        }

        public void OnEquip()
        {
            SaveLoadManager.Instance.SaveSelectedPetData(_pet.id, true);
            _pet.PetSkill.AddSkill(_pet.heroDamageDataSo);
            PetManager.OnEquipPet.Invoke(_pet);
        }

        public void OnTakeOff()
        {
            SaveLoadManager.Instance.SaveSelectedPetData(_pet.id, false);
            _pet.PetSkill.RemoveSkill(_pet.heroDamageDataSo);
            PetManager.OnTakeOffPet.Invoke(_pet);
        }

        public override void UpdateRow(int totalGem)
        {
            FillUIRow();
            SetButtonState(totalGem);
        }
    }
}