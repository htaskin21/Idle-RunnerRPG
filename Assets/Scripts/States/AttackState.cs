using Hero;

namespace States
{
    public class AttackState : State
    {
        //public State runState;
        //public State idleState;

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Attack);

            characterController.AnimationController.OnAnimationEnd.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(characterController.attackPoint));

            base.EnterState();
        }

        protected override void ExitState()
        {
            characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();
            base.ExitState();
        }
    }
}