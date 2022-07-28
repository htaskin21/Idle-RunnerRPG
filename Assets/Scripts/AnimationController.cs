using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.Idle:
                Play("Idle");
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
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
        }
    }

    public void Play(string name)
    {
        animator.Play(name, -1, 0f);
    }
}

public enum AnimationType
{
    Idle,
    Run,
    Hit,
    Attack,
    SuperAttack,
    Die
}