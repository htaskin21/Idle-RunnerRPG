using Hero;
using UnityEngine;

public class PetController : MonoBehaviour
{
    [SerializeField]
    private AnimationController animationController;


    private void OnEnable()
    {
        HeroMovement.OnHeroStartRunning += StartRunning;
        HeroMovement.OnHeroStopRunning += StartIdle;
    }

    private void StartRunning()
    {
        animationController.PlayAnimation(AnimationType.Run);
    }

    private void StartIdle()
    {
        animationController.PlayAnimation(AnimationType.Idle);
    }

    private void OnDisable()
    {
        HeroMovement.OnHeroStartRunning -= StartRunning;
        HeroMovement.OnHeroStopRunning -= StartIdle;
    }
}