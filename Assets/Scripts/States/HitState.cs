using Enemy;
using UnityEngine;

namespace States
{
    public class HitState : State
    {
        [SerializeField]
        private State idleState;

        [SerializeField]
        private State dieState;

        protected override void EnterState()
        {
            var enemyController = (EnemyController) CharacterController;
            enemyController.TapDamageController.isTapDamageEnable = true;

            CharacterController.AnimationController.PlayAnimation(AnimationType.Hit);
            CharacterController.AnimationController.onAnimationEnd.AddListener(DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();
            base.ExitState();
        }

        private void DecideNextState()
        {
            var enemyController = (EnemyController) CharacterController;
            CharacterController.TransitionToState(enemyController.enemyHealth.Health <= 0 ? dieState : idleState);
        }
    }
}