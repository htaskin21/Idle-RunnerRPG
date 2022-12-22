using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create SkillIconDataSO", fileName = "SkillIconDataSO", order = 1)]
    public class SkillIconDataSO: SerializedScriptableObject
    {
        public Dictionary<int, Sprite> Icons = new Dictionary<int, Sprite>();

        public Sprite GetIcon(int skillID)
        {
            if (Icons.ContainsKey(skillID))
            {
                return Icons[skillID];
            }

            Debug.LogError("Skill Iconu Eksik");
            return null;
        }
    }
}