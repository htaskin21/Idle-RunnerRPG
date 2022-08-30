using Hero;
using UI;

namespace States
{
    public class SpecialAttackState : State
    {
        public State runState;
        public State idleState;

        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.SpecialAttack);

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(GameManager.Instance.HeroController.heroAttack.SpecialAttackPoint));

            CharacterController.AnimationController.onAnimationEnd.AddListener(DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            CharacterController.AnimationController.onAnimationAction.RemoveAllListeners();
            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();

            base.ExitState();
        }

        private void DecideNextState()
        {
            if (GameManager.Instance.HeroController.heroAttack.CurrentEnemy.enemyHealth.Health <= 0)
            {
                CharacterController.TransitionToState(runState);
            }
            else
            {
                CharacterController.TransitionToState(idleState);
            }
        }
    }
}
