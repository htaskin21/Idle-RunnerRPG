using UnityEngine;

namespace States
{
    public class HitState : State
    {
        [SerializeField] private State idleState;
        [SerializeField] private State dieState;

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Hit);
            base.EnterState();
        }
    }
}