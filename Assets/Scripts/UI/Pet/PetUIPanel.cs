using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace UI.Pet
{
    public class PetUIPanel : UIPanel, IEnhancedScrollerDelegate
    {
        [SerializeField]
        private EnhancedScroller enhancedScroller;

        [SerializeField]
        private EnhancedScrollerCellView enhancedScrollerCellView;

        private List<PetMainUIRow> _mainUIRows;

        private List<PetSO> pets;

        public override void Start()
        {
            SetMainRows();

            PetManager.OnEquipPet += SetMainRow;
            PetManager.OnTakeOffPet += ResetMainRow;

            enhancedScroller.Delegate = this;
            base.Start();
        }

        public void LoadData(List<PetSO> petSOs)
        {
            pets = new List<PetSO>();
            pets = petSOs;

            enhancedScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return pets.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 200f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            PetUIRow petUIRow =
                enhancedScroller.GetCellView(enhancedScrollerCellView) as PetUIRow;

            petUIRow.SetUIRow(pets[dataIndex]);

            return petUIRow;
        }

        private void SetMainRows()
        {
            var data = SaveLoadManager.Instance.LoadSelectedPetData();
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var pet = pets.FirstOrDefault(x => x.id == data[i]);
                    _mainUIRows[i].SetMainUIRow(pet);
                }
            }
        }

        private void SetMainRow(PetSO petSo)
        {
            var emptyMainRow = _mainUIRows.FirstOrDefault(x => x._currentPet == null);
            emptyMainRow.SetMainUIRow(petSo);
        }

        private void ResetMainRow(PetSO petSo)
        {
            var mainRow = _mainUIRows.FirstOrDefault(x => x._currentPet == petSo);
            mainRow.ResetMainUIRow();
        }
    }
}