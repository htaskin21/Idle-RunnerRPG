using DG.Tweening;

namespace States
{
    public class RunState : State
    {
        public State attackState;
        private Tweener _runningTweener;

        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Run);
            _runningTweener = characterController.GetComponent<HeroMovement>().StartRunning();

            base.EnterState();
        }

        protected override void ExitState()
        {
            _runningTweener.Kill();
            base.ExitState();
        }
    }
}