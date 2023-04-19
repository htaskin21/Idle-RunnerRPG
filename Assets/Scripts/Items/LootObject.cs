using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using States;
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

            for (int i = 0; i < _defaultPositions.Count; i++)
            {
                sprites[i].transform.localPosition = _defaultPositions[i];
                sprites[i].transform.localScale = new Vector3(.5f, .5f, .5f);
            }

            o.SetActive(true);
        }

        public async UniTask MoveLoot(CancellationTokenSource cts)
        {
            var targetPosition = Camera.main.ScreenToWorldPoint(UIManager.Instance.CoinHud.transform.position);
            targetPosition = new Vector3(targetPosition.x + GetTargetDifference(), targetPosition.y, targetPosition.z);

            Sequence mySequence = null;

            foreach (var coinSprite in sprites)
            {
                mySequence = DOTween.Sequence();
                mySequence.Append(coinSprite.transform.DOMove(targetPosition, movementDuration).SetEase(Ease.Linear))
                    .OnPlay(() =>
                        coinSprite.transform.DOScale(new Vector3(0, 0, 0), movementDuration).SetEase(Ease.InCirc));

                await UniTask.Delay(150);
            }

            if (lootType == LootType.Gem)
            {
                EconomyManager.OnCollectGem.Invoke((int) _lootAmount);
            }
            else
            {
                EconomyManager.OnCollectCoin.Invoke(_lootAmount);
            }
            
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

        private float GetTargetDifference()
        {
            var targetDifference = -0.5f;
            if (StageManager.Instance.HeroController.currentState.stateType == StateType.Run)
            {
                targetDifference = 0.5f;
            }

            return targetDifference;
        }
    }
}