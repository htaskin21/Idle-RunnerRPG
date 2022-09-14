using System;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Create IconDataSO", fileName = "IconDataSO", order = 1)]
    public class IconData : ScriptableObject
    {
        [SerializeField]
        private Sprite earthIcon;

        [SerializeField]
        private Sprite plantIcon;

        [SerializeField]
        private Sprite waterIcon;

        [SerializeField]
        private Sprite normalIcon;


        public Sprite GetIcon(DamageType type)
        {
            return type switch
            {
                DamageType.Earth => earthIcon,
                DamageType.Plant => plantIcon,
                DamageType.Water => waterIcon,
                DamageType.Normal => normalIcon,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}