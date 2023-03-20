using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UI.Pet;
using UnityEngine;

namespace Managers
{
    public class PetManager : MonoBehaviour
    {
        [SerializeField]
        private List<PetSO> _pets;

        [SerializeField]
        private PetUIPanel _petUIPanel;

        [SerializeField]
        private List<Pet> _activePetGameObjects;

        [SerializeField]
        private List<Pet> _deActivePetGameObjects;

        [Header("Hero")]
        [SerializeField]
        private Transform _heroParent;

        [SerializeField]
        private List<Transform> _petPositions;

        public static Action<PetSO> OnEquipPet;
        public static Action<PetSO> OnTakeOffPet;

        private void Start()
        {
            _activePetGameObjects = new List<Pet>();
            _deActivePetGameObjects = new List<Pet>();

            OnEquipPet = delegate(PetSO so) { };
            OnTakeOffPet = delegate(PetSO so) { };

            OnEquipPet += SetPetGameObject;
            OnTakeOffPet += ResetPetGameObject;

            _petUIPanel.LoadData(_pets);
        }

        private void SetPetGameObject(PetSO pet)
        {
            var selectedPetCount = SaveLoadManager.Instance.LoadSelectedPetData().Count - 1;
            if (_deActivePetGameObjects.Contains(pet.petPrefab))
            {
                var petGO = _deActivePetGameObjects.FirstOrDefault(x => x == pet.petPrefab);
                petGO.transform.position = _petPositions[selectedPetCount].position;
                petGO.gameObject.SetActive(true);

                _deActivePetGameObjects.Remove(petGO);
                _activePetGameObjects.Add(petGO);
            }
            else
            {
                var petGameObject = Instantiate(pet.petPrefab, _petPositions[selectedPetCount].position,
                    Quaternion.identity, _heroParent);
                petGameObject.gameObject.SetActive(true);
                _activePetGameObjects.Add(petGameObject.GetComponent<Pet>());
            }
        }

        private void ResetPetGameObject(PetSO petSo)
        {
            var petGO = _activePetGameObjects.FirstOrDefault(x => x == petSo.petPrefab);
            _activePetGameObjects.Remove(petGO);

            if (petGO.transform.position.x == _petPositions[0].position.x)
            {
                if (_activePetGameObjects.Count > 0 & petGO.transform.position.x == _petPositions[0].position.x)
                {
                    var activePet = _activePetGameObjects.FirstOrDefault(x => x.gameObject.activeInHierarchy);
                    activePet.transform.position = _petPositions[0].transform.position;
                }
            }

            petGO.gameObject.SetActive(false);
            _deActivePetGameObjects.Add(petGO);
        }
    }
}