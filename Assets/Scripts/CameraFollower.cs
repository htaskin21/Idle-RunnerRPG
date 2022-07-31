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
        HeroMovement.OnHeroStartRunning += StartCameraMove;
        HeroMovement.OnHeroStopRunning += StopCameraMove;
    }

    private void StartCameraMove()
    {
        Debug.Log(_heroMovement.transform.position.x);
        
        cameraMoveTweener = mainCamera.transform.DOMoveX(-_heroMovement.transform.position.x - .3f, 1f)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void StopCameraMove()
    {
        cameraMoveTweener.Kill();
    }
}