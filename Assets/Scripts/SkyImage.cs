using DG.Tweening;
using Hero;
using UnityEngine;

public class SkyImage : MonoBehaviour
{
    [SerializeField] private HeroMovement _heroMovement;
    
    public SpriteRenderer firstLayer;

    public SpriteRenderer secondLayer;

    public SpriteRenderer thirdLayer;

    private Tweener skyImageTweener;

    private void Start()
    {
        HeroMovement.OnHeroStartRunning += StartSkyImageMove;
        HeroMovement.OnHeroStopRunning += StopSkyImageMove;
    }

    private void StartSkyImageMove()
    {
        var go = gameObject;
        skyImageTweener = go.transform.DOMoveX(go.transform.position.x + 1, 1.05f).SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void StopSkyImageMove()
    {
        skyImageTweener.Kill();
    }
}