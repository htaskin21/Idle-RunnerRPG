using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public UnityEvent onAnimationAction;
    public UnityEvent onAnimationEnd;

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
            case AnimationType.SpecialAttack:
                Play("SpecialAttack");
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
        onAnimationEnd?.Invoke();
    }

    public void ResetAnimationEndEvent()
    {
        onAnimationEnd.RemoveAllListeners();
    }
    
    public void InvokeAnimationAction()
    {
        onAnimationAction?.Invoke();
    }

    public void ResetAnimationActionEvent()
    {
        onAnimationAction.RemoveAllListeners();
    }
}

public enum AnimationType
{
    Idle,
    WakeUp,
    Run,
    Hit,
    Attack,
    SpecialAttack,
    Die
}