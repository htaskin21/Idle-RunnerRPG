using System;
using Managers;
using UI;
using UnityEngine;

namespace Items.Potion
{
    public class StrengthPotion : Potion
    {
        [SerializeField]
        private BoostIconController _boostIconController;

        private void Start()
        {
            SetData();

            var boostFinishTime = SaveLoadManager.Instance.LoadStrengthBoostTime();
            if (boostFinishTime > DateTime.UtcNow)
            {
                _boostIconController.SetBoostIcon(boostFinishTime);
            }
        }

        public override void UsePotion()
        {
            SaveLoadManager.Instance.SavePotion(_potionData._potionType, -1);
            SaveLoadManager.Instance.SaveStrengthBoostTime(_potionData.boostDuration);

            var strengthBoostTime = SaveLoadManager.Instance.LoadStrengthBoostTime();
            _boostIconController.SetBoostIcon(strengthBoostTime);

            SetData();
        }


        //TODO Store'u ekledikten sonra sil
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SaveLoadManager.Instance.SavePotion(_potionData._potionType, 1);
                SetData();
            }
        }
    }
}