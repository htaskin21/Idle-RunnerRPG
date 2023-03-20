using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Pet Menu/Create Pet", fileName = "PetSO")]
    public class PetSO : SerializedScriptableObject
    {
        public int id;

        public Sprite icon;

        public int cost;

        public Pet petPrefab;

        [OdinSerialize]
        public PetSkill PetSkill;

        public HeroDamageDataSO heroDamageDataSo;
    }
}