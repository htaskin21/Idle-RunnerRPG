using DG.Tweening;
using Hero;

namespace States
{
    public class RunState : State
    {
        public State attackState;
        private Tweener _runningTweener;


        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Run);
            characterController.GetComponent<HeroMovement>().OnHeroStartRunning?.Invoke();

            base.EnterState();
        }

        protected override void ExitState()
        {
            characterController.GetComponent<HeroMovement>().OnHeroStopRunning?.Invoke();
            base.ExitState();
        }
    }
}