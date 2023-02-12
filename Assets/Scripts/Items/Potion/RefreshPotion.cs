using Managers;
using UnityEngine;

namespace Items.Potion
{
    public class RefreshPotion : Potion
    {
        private void Start()
        {
            SetData();
        }

        public override void UsePotion()
        {
            SaveLoadManager.Instance.SavePotion(_potionData._potionType, -1);
            
            SetData();
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