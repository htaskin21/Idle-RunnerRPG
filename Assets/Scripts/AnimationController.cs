using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public UnityEvent OnAnimationAction;
    public UnityEvent OnAnimationEnd;

    public void PlayAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.Idle:
                Play("Idle");
                break;
            case AnimationType.WakeUp:
                Play("WakeUp");
                break;
            case AnimationType.Run:
                Play("Run");
                break;
            case AnimationType.Hit:
                Play("Hit");
                break;
            case AnimationType.Attack:
                Play("Attack");
                break;
            case AnimationType.SuperAttack:
                break;
            case AnimationType.Die:
                Play("Die");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
        }
    }

    private void Play(string name)
    {
        animator.Play(name, -1, 0f);
    }

    public void InvokeAnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }

    public void ResetAnimationEndEvent()
    {
        OnAnimationEnd.RemoveAllListeners();
    }
    
    public void InvokeAnimationAction()
    {
        OnAnimationAction?.Invoke();
    }

    public void ResetAnimationActionEvent()
    {
        OnAnimationAction.RemoveAllListeners();
    }
}

public enum AnimationType
{
    Idle,
    WakeUp,
    Run,
    Hit,
    Attack,
    SuperAttack,
    Die
}