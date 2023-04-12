using Enums;
using Items;
using Managers;
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
            var prestigeMultiplier = SaveLoadManager.Instance.LoadPrestigeCount() * 0.5f;
            lootAmount *= (enemyController.enemyLevel + prestigeMultiplier);

            var go = ObjectPool.Instance.GetGameObject(lootType.ToString());
            var lootObject = go.GetComponent<LootObject>();

            lootObject.SetInitialPosition(enemyController.transform, lootAmount);
        }
    }
}