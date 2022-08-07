using UnityEngine;

namespace States
{
    public class WakeUpState : State
    {
        [SerializeField] private State runState;
        [SerializeField] private State attackState;

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.WakeUp);
            
            characterController.AnimationController.OnAnimationEnd.AddListener(
                DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();
            base.ExitState();
        }

        private void DecideNextState()
        {
            if (GameManager.Instance.EnemyController.enemyHealth.Health <= 0)
            {
                characterController.TransitionToState(runState);
            }
            else
            {
                characterController.TransitionToState(attackState);
            }
        }
    }
}