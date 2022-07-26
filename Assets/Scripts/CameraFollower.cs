using DG.Tweening;
using Hero;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private HeroMovement _heroMovement;

    private Tweener cameraMoveTweener;

    private void Start()
    {
        _heroMovement.OnHeroStartRunning += StartCameraMove;
        _heroMovement.OnHeroStopRunning += StopCameraMove;
    }

    private void StartCameraMove()
    {
        cameraMoveTweener = mainCamera.transform.DOMoveX(-_heroMovement.transform.position.x - .3f, 1f)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void StopCameraMove()
    {
        cameraMoveTweener.Kill();
    }
}