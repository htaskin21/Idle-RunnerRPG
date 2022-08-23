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
            if (GameManager.Instance.EnemyController.enemyHealth.Health <= 0)
            {
                GameManager.Instance.EnemyController.BoxCollider2D.enabled = false;
                CharacterController.TransitionToState(dieState);
            }
            else
            {
                CharacterController.TransitionToState(idleState);
            }
        }
    }
}