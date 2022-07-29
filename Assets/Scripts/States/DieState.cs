namespace States
{
    public class DieState : State
    {
        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Die);
            base.EnterState();
        }
    }
}