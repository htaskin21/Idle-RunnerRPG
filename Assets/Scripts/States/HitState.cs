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
            if (GameManager.Instance.EnemyController.enemyHealth.Health <= 0)
            {
                GameManager.Instance.EnemyController.BoxCollider2D.enabled = false;
                characterController.TransitionToState(dieState);
            }
            else
            {
                characterController.TransitionToState(idleState);
            }
        }
    }
}