using Hero;
using UI;

namespace States
{
    public class AttackState : State
    {
        public State runState;
        public State idleState;

        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.Attack);

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(GameManager.Instance.HeroController.heroAttack.CalculateDamage()));

            HeroController heroController = (HeroController) CharacterController;
            CharacterController.AnimationController.onAnimationEnd.AddListener(heroController.DecideNextState);

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