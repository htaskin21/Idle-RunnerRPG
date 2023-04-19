using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Managers;
using UI;
using UnityEngine;

namespace Items
{
    public class Coin : LootObject
    {
        public override async UniTask MoveLoot(CancellationTokenSource cts)
        {
            SetTargetPosition();

            var mySequence = await TweenObjectToHud();
            
            EconomyManager.OnCollectCoin.Invoke(_lootAmount);
            
            await mySequence.AsyncWaitForCompletion();

            this.gameObject.SetActive(false);

            cts.Cancel();
        }

        protected override void SetTargetPosition()
        {
            _targetPosition = Camera.main.ScreenToWorldPoint(UIManager.Instance.CoinHud.transform.position);
            _targetPosition = new Vector3(_targetPosition.x + GetTargetDifference(), _targetPosition.y,
                _targetPosition.z);
        }
    }
}
