using DG.Tweening;
using Hero;
using UI;

namespace States
{
    public class RunState : State
    {
        private Tweener _runningTweener;

        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.Run);
            HeroMovement.OnHeroStartRunning?.Invoke();

            ButtonController.OnActiveAttackButtons?.Invoke(false);

            base.EnterState();
        }

        protected override void ExitState()
        {
            HeroMovement.OnHeroStopRunning?.Invoke();
            base.ExitState();
        }
    }
}