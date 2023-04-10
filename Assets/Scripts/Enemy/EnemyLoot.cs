using Enums;
using Items;
using UnityEngine;

namespace Enemy
{
    public class EnemyLoot : MonoBehaviour
    {
        [SerializeField]
        private EnemyController enemyController;

        [SerializeField]
        private LootType lootType;

        [SerializeField]
        private double lootAmount;

        public double LootAmount => lootAmount;

        public void InitiateLootItem()
        {
            lootAmount *= enemyController.enemyLevel;

            var go = ObjectPool.Instance.GetGameObject(lootType.ToString());
            var lootObject = go.GetComponent<LootObject>();

            lootObject.SetInitialPosition(enemyController.transform, lootAmount);
        }
    }
}