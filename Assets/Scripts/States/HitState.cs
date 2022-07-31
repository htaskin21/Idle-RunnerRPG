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
            characterController.AnimationController.OnAnimationEnd.AddListener(DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();
            base.ExitState();
        }

        private void DecideNextState()
        {
            if (characterController.healthPoint <= 0)
            {
                //var dieState = characterController.GetState(StateType.Die);
                characterController.TransitionToState(dieState);
            }
            else
            {
                //var idleState = characterController.GetState(StateType.Idle);
                characterController.TransitionToState(idleState);
            }
        }
    }
}