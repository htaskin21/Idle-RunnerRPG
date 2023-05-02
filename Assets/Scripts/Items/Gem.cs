using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using UI;
using UnityEngine;

namespace Items
{
    public class Gem : LootObject
    {
        public override async UniTask MoveLoot(CancellationTokenSource cts)
        {
            SetTargetPosition();

            var mySequence = await TweenObjectToHud();

            EconomyManager.OnCollectGem.Invoke((int) _lootAmount);
            CurrencyPopUpPanel.OnShowCurrencyPopUpPanel?.Invoke(_lootAmount, LootType.Gem);

            await mySequence.AsyncWaitForCompletion();

            this.gameObject.SetActive(false);

            cts.Cancel();
        }

        protected override void SetTargetPosition()
        {
            _targetPosition = Camera.main.ScreenToWorldPoint(UIManager.Instance.GemHud.transform.position);
            _targetPosition = new Vector3(_targetPosition.x + GetTargetDifference(), _targetPosition.y,
                _targetPosition.z);
        }
    }
}