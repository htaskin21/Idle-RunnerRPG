using Hero;
using Managers;
using States;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [HideInInspector]
    public int petId;
    
    [SerializeField]
    private AnimationController animationController;

    private void OnEnable()
    {
        HeroMovement.OnHeroStartRunning += StartRunning;
        HeroMovement.OnHeroStopRunning += StartIdle;

        if (GameManager.Instance.HeroController.currentState.stateType == StateType.Run)
        {
            animationController.PlayAnimation(AnimationType.Run);
        }
        else
        {
            animationController.PlayAnimation(AnimationType.Idle);
        }
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