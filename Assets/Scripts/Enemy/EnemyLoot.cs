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
        
        public void InitiateLootItem()
        {
            lootAmount *= enemyController.enemyLevel;

            var go = GameManager.Instance.ObjectPool.GetGameObject(lootType.ToString());
            var lootObject = go.GetComponent<LootObject>();

            var a = enemyController.AnimationController.transform.localPosition;

            
            
            lootObject.SetInitialPosition(enemyController.transform, lootAmount);
        }
    }
}