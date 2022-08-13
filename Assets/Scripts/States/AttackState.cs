using Hero;

namespace States
{
    public class AttackState : State
    {
        public State runState;
        public State idleState;

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Attack);

            characterController.AnimationController.OnAnimationAction.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(GameManager.Instance.HeroController.heroAttack.AttackPoint));

            characterController.AnimationController.OnAnimationEnd.AddListener(DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            characterController.AnimationController.OnAnimationAction.RemoveAllListeners();
            characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();

            base.ExitState();
        }

        private void DecideNextState()
        {
            if (GameManager.Instance.HeroController.heroAttack.CurrentEnemy.enemyHealth.Health <= 0)
            {
                characterController.TransitionToState(runState);
            }
            else
            {
                characterController.TransitionToState(idleState);
            }
        }
    }
}