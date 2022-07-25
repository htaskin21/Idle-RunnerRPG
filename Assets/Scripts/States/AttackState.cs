namespace States
{
    public class AttackState : State
    {
        public State runState;

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Attack);
            base.EnterState();
        }
    }
}