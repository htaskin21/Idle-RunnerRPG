using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI;
using UnityEngine;

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
        o.transform.position = enemyPosition.transform.position;

        for (int i = 0; i < _defaultPositions.Count - 1; i++)
        {
            sprites[i].transform.localPosition = _defaultPositions[i];
            sprites[i].transform.localScale = new Vector3(.5f, .5f, .5f);
        }

        o.SetActive(true);
    }

    public async UniTask MoveLoot(CancellationTokenSource cts)
    {
        var targetPosition = Camera.main.ScreenToWorldPoint(UIManager.Instance.CoinHud.transform.position);
        Sequence mySequence = null;

        foreach (var coinSprite in sprites)
        {
            mySequence = DOTween.Sequence();

            mySequence.Append(coinSprite.transform.DOMove(targetPosition, movementDuration).SetEase(Ease.Linear))
                .Append(coinSprite.transform.DOScale(new Vector3(0, 0, 0), movementDuration).SetEase(Ease.InCirc));

            await UniTask.Delay(100);
        }

        await mySequence.AsyncWaitForCompletion();

        this.gameObject.SetActive(false);

        cts.Cancel();
    }
}

public enum LootType
{
    Coin,
    Mana
}