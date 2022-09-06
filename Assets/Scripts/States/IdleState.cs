using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace States
{
    public class IdleState : State
    {
        [SerializeField]
        private State wakeUpState;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.Idle);

            base.EnterState();

            if (CharacterController.gameObject.CompareTag("Player"))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;

                WaitForNextAttack().Forget();
            }
        }

        private async UniTask WaitForNextAttack()
        {
            var heroController = GameManager.Instance.HeroController;

            heroController.heroUI.SetCoolDownSlider(heroController.heroAttack.AttackCooldown);

            await UniTask.Delay(heroController.heroAttack.AttackCooldown,
                cancellationToken: _cancellationToken);

            CharacterController.TransitionToState(wakeUpState);
        }

        protected override void ExitState()
        {
            if (CharacterController.gameObject.CompareTag("Player"))
            {
                _cancellationTokenSource.Cancel();
            }

            GameManager.Instance.HeroController.heroUI.FadeOutSlider();
            base.ExitState();
        }
    }
}