using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using UI;
using UnityEngine;

namespace Items
{
    public class LootObject : MonoBehaviour
    {
        [SerializeField]
        private LootType lootType;

        private double _lootAmount;

        [SerializeField]
        private float movementDuration;

        [SerializeField]
        private List<GameObject> sprites;

        [SerializeField]
        private List<Vector3> _defaultPositions;

        public void SetInitialPosition(Transform enemyPosition, double amount)
        {
            _lootAmount = amount;

            var o = gameObject;
            var position = enemyPosition.transform.position;
            o.transform.position = new Vector3(position.x, 0, position.z);

            for (int i = 0; i < _defaultPositions.Count - 1; i++)
            {
                sprites[i].transform.localPosition = _defaultPositions[i];
                sprites[i].transform.localScale = new Vector3(.5f, .5f, .5f);
            }

            o.SetActive(true);
        }

        private async UniTask MoveLoot(CancellationTokenSource cts)
        {
            var targetPosition = Camera.main.ScreenToWorldPoint(UIManager.Instance.CoinHud.transform.position);
            Sequence mySequence = null;

            foreach (var coinSprite in sprites)
            {
                mySequence = DOTween.Sequence();
                mySequence.Append(coinSprite.transform.DOMove(targetPosition, movementDuration).SetEase(Ease.Linear))
                    .OnPlay(() =>
                        coinSprite.transform.DOScale(new Vector3(0, 0, 0), movementDuration).SetEase(Ease.InCirc));

                await UniTask.Delay(200);
            }

            EconomyManager.OnCollectCoin.Invoke(_lootAmount);

            await mySequence.AsyncWaitForCompletion();

            this.gameObject.SetActive(false);

            cts.Cancel();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                var cts = new CancellationTokenSource();
                MoveLoot(cts).Forget();
            }
        }
    }
}