namespace States
{
    public class DieState : State
    {
        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Die);

            characterController.AnimationController.OnAnimationEnd.AddListener(() =>
                this.transform.parent.gameObject.SetActive(false));

            base.EnterState();
        }

        protected override void ExitState()
        {
            characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();
            base.ExitState();
        }
    }
}