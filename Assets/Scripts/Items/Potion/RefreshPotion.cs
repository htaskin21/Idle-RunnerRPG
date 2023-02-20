using System;
using Managers;
using UnityEngine;

namespace Items.Potion
{
    public class RefreshPotion : Potion
    {
        public static Action OnRefreshAllSpecialAttack;

        private void Awake()
        {
            OnRefreshAllSpecialAttack = () => { };
        }

        private void Start()
        {
            SetData();
        }

        public override void UsePotion()
        {
            SaveLoadManager.Instance.SavePotion(_potionData._potionType, -1);
            SetData();
            OnRefreshAllSpecialAttack?.Invoke();
        }


        //TODO Store'u ekledikten sonra sil
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SaveLoadManager.Instance.SavePotion(_potionData._potionType, 1);
                SetData();
            }
        }
    }
}