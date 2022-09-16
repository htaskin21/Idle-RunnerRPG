using System;
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

        private void Start()
        {
            enemyController.enemyHealth.OnEnemyDie += InitiateLootItem;
        }

        public void InitiateLootItem()
        {
            lootAmount *= enemyController.enemyLevel;

            var go = GameManager.Instance.ObjectPooler.GetGameObject(lootType.ToString());
            var lootObject = go.GetComponent<LootObject>();

            var a = enemyController.AnimationController.transform.localPosition;

            lootObject.SetInitialPosition(enemyController.transform, lootAmount);
        }

        private void OnDestroy()
        {
            enemyController.enemyHealth.OnEnemyDie -= InitiateLootItem;
        }
    }
}