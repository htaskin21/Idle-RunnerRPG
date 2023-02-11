using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public class BoostIconController : MonoBehaviour
    {
        [SerializeField]
        private Sprite _iconSprite;

        [SerializeField]
        private Transform _boostIconsPanel;

        [HideInInspector]
        public BoostIcon boostIcon;

        private void InstantiateBoostIcon()
        {
            var go = ObjectPool.Instance.GetGameObject("BoostIcon");
            boostIcon = go.GetComponent<BoostIcon>();
            boostIcon.icomImage.sprite = _iconSprite;

            go.transform.SetParent(_boostIconsPanel);
        }

        public void SetBoostIcon(DateTime boostTime)
        {
            if (boostIcon == null)
            {
                InstantiateBoostIcon();
            }

            boostIcon.gameObject.SetActive(true);
            boostIcon.SetCoolDown(boostTime).Forget();
        }
    }
}