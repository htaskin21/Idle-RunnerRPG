using UnityEngine;

namespace States
{
    public class IdleState : State
    {
        [SerializeField] private State attackState;

        [SerializeField] private State hitState;

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Idle);
            base.EnterState();
        }
    }
}