using System;
using DG.Tweening;
using Hero;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private Tweener _cameraMoveTweener;

    private void Start()
    {
        HeroMovement.OnHeroStartRunning += StartCameraMove;
        HeroMovement.OnHeroStopRunning += StopCameraMove;
    }

    private void StartCameraMove()
    {
        _cameraMoveTweener = mainCamera.transform.DOLocalMoveX(mainCamera.transform.position.x + 1, .75f)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void StopCameraMove()
    {
        _cameraMoveTweener?.Kill();
    }

    private void OnDestroy()
    {
        _cameraMoveTweener?.Kill();
    }
}