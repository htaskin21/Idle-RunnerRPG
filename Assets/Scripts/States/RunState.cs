using DG.Tweening;
using Hero;

namespace States
{
    public class RunState : State
    {
        private Tweener _runningTweener;
        
        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.Run);
            HeroMovement.OnHeroStartRunning?.Invoke();

            base.EnterState();
        }

        protected override void ExitState()
        {
            HeroMovement.OnHeroStopRunning?.Invoke();
            base.ExitState();
        }
    }
}