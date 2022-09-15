using System.Threading;
using Cysharp.Threading.Tasks;
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
            var cts = new CancellationTokenSource();

            lootAmount *= enemyController.enemyLevel;

            var go = GameManager.Instance.ObjectPooler.GetGameObject(lootType.ToString());
            var lootObject = go.GetComponent<LootObject>();

            var a = enemyController.AnimationController.transform.localPosition;

            lootObject.SetInitialPosition(enemyController.transform, lootAmount);

            lootObject.MoveLoot(cts).Forget();
        }
    }
}