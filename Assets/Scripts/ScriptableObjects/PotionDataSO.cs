using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Potion Data", fileName = "PotionSO", order = 0)]
    public class PotionDataSO : ScriptableObject
    {
        public PotionType _potionType;

        public Sprite fullImage;

        public Sprite emptyImage;

        public int boostDuration;
    }
}