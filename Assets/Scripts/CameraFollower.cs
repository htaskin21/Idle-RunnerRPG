using DG.Tweening;
using Enums;
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
        
        HeroMovement.OnHeroStartRunning += ZoomOutCamera;
        HeroMovement.OnHeroStopRunning += ZoomInCamera;

        HeroAttack.OnInflictDamage += ShakeCamera;
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

    private void ZoomInCamera()
    {
        mainCamera.DOOrthoSize(8.5f,0.35f).SetEase(Ease.Flash);

    }
    
    private void ZoomOutCamera()
    {
        mainCamera.DOOrthoSize(9f,0.35f).SetEase(Ease.Flash);
    }

    private void ShakeCamera(double damage, AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.HeroDamage:
                break;
            case AttackType.TapDamage:
                break;
            case AttackType.CriticalDamage:
                mainCamera.DOShakeRotation(0.2f, 2f).OnComplete(() =>
                    mainCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)));
                break;
            case AttackType.SpecialAttackDamage:
                mainCamera.DOShakeRotation(0.35f, 2f).OnComplete(() =>
                    mainCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)));
                break;
        }
    }

    private void OnDestroy()
    {
        _cameraMoveTweener?.Kill();
    }
}