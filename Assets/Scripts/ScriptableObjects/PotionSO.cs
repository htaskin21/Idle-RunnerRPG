using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Potion", fileName = "PotionSO", order = 0)]
    public class PotionSO : ScriptableObject
    {
        [SerializeField]
        private PotionType _potionType;

        [SerializeField]
        private Sprite fullImage;

        [SerializeField]
        private Sprite emptyImage;
    }
}