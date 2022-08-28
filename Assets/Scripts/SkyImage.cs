using System;
using DG.Tweening;
using Hero;
using UnityEngine;

public class SkyImage : MonoBehaviour
{
    [SerializeField]
    private HeroMovement heroMovement;

    public SpriteRenderer firstLayer;

    public SpriteRenderer secondLayer;

    public SpriteRenderer thirdLayer;

    private Tweener _skyImageTweener;

    private void Start()
    {
        HeroMovement.OnHeroStartRunning += StartSkyImageMove;
        HeroMovement.OnHeroStopRunning += StopSkyImageMove;
    }

    private void StartSkyImageMove()
    {
        var go = gameObject;
        _skyImageTweener = go.transform.DOMoveX(go.transform.position.x + 1, 1.05f).SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void StopSkyImageMove()
    {
        _skyImageTweener.Kill();
    }

    private void OnDestroy()
    {
        HeroMovement.OnHeroStartRunning -= StartSkyImageMove;
        HeroMovement.OnHeroStopRunning -= StopSkyImageMove;
    }
}