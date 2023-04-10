using Hero;
using UI;

namespace States
{
    public class AttackState : State
    {
        public State runState;
        public State idleState;

        private HeroController _heroController;

        private void Start()
        {
            _heroController = (HeroController) CharacterController;
        }

        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.Attack);

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(_heroController.heroAttack.CalculateDamage(),
                    _heroController.heroAttack.GetAttackType()));

            CharacterController.AnimationController.onAnimationEnd.AddListener(_heroController.DecideNextState);

            ButtonController.OnActiveAttackButtons?.Invoke(true);

            base.EnterState();
        }

        protected override void ExitState()
        {
            CharacterController.AnimationController.onAnimationAction.RemoveAllListeners();
            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();

            base.ExitState();
        }
    }
}