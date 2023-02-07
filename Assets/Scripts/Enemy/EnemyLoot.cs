using System;
using Items;
using Managers;
using UnityEngine;
using LootType = Enums.LootType;

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
        
        public void InitiateLootItem()
        {
            lootAmount *= enemyController.enemyLevel;

            var go = GameManager.Instance.ObjectPool.GetGameObject(lootType.ToString());
            var lootObject = go.GetComponent<LootObject>();

            lootObject.SetInitialPosition(enemyController.transform, lootAmount);
        }
    }
}