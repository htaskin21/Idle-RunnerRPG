using Managers;
using UnityEngine;

namespace States
{
    public class WakeUpState : State
    {
        [SerializeField]
        private State runState;

        [SerializeField]
        private State attackState;

        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.WakeUp);

            CharacterController.AnimationController.onAnimationEnd.AddListener(
                DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();
            base.ExitState();
        }

        private void DecideNextState()
        {
            if (GameManager.Instance.EnemyController.enemyHealth.Health <= 0)
            {
                CharacterController.TransitionToState(runState);
            }
            else
            {
                CharacterController.TransitionToState(attackState);
            }
        }
    }
}