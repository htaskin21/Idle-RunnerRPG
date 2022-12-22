using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create DamageIconDataSO", fileName = "DamageIconDataSO", order = 1)]
    public class DamageIconDataSO : SerializedScriptableObject
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