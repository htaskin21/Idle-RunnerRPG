using DG.Tweening;
using Hero;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private HeroMovement _heroMovement;

    private Tweener _cameraMoveTweener;

    private void Start()
    {
        HeroMovement.OnHeroStartRunning += StartCameraMove;
        HeroMovement.OnHeroStopRunning += StopCameraMove;
    }

    private void StartCameraMove()
    {
        Debug.Log(_heroMovement.transform.position.x);
        int multiplier;

        if (_heroMovement.transform.position.x < 1)
        {
            multiplier = -1;
            
            _cameraMoveTweener = mainCamera.transform.DOMoveX(multiplier * _heroMovement.transform.position.x - .3f, 1f)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }
        else
        {
            multiplier = 1;
            
            _cameraMoveTweener = mainCamera.transform.DOLocalMoveX(mainCamera.transform.position.x+1, 1f)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);   
        }
        
        


       
    }

    private void StopCameraMove()
    {
        _cameraMoveTweener.Kill();
    }
}