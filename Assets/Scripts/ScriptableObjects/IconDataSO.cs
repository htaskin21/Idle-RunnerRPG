using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Create IconDataSO", fileName = "IconDataSO", order = 1)]
    public class IconDataSO : SerializedScriptableObject
    {
        public Dictionary<DamageType, Sprite> Icons = new Dictionary<DamageType, Sprite>();

        public Sprite GetIcon(DamageType type)
        {
            if (Icons.ContainsKey(type))
            {
                return Icons[type];
            }

            Debug.LogError("Damage Type Iconu Eksik");
            return null;
        }
    }
}