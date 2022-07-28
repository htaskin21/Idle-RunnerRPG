namespace States
{
    public class AttackState : State
    {
        public State runState;
        public State idleState; 

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Attack);
            base.EnterState();
        }
        
       
    }
}