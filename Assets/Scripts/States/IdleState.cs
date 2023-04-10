using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using UI;
using UnityEngine;

namespace States
{
    public class IdleState : State
    {
        [SerializeField]
        private State wakeUpState;

        private HeroController _heroController;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        private void Start()
        {
            if (CharacterController.gameObject.CompareTag("Player"))
            {
                _heroController = (HeroController) CharacterController;
            }
        }

        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.Idle);

            base.EnterState();

            if (CharacterController.gameObject.CompareTag("Player"))
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;

                ButtonController.OnActiveAttackButtons?.Invoke(true);

                WaitForNextAttack().Forget();
            }
        }

        private async UniTask WaitForNextAttack()
        {
            _heroController.heroUI.SetCoolDownSlider(_heroController.heroAttack.HeroDamageDataSo.attackCooldown);

            await UniTask.Delay(_heroController.heroAttack.HeroDamageDataSo.attackCooldown,
                cancellationToken: _cancellationToken);

            CharacterController.TransitionToState(wakeUpState);
        }

        protected override void ExitState()
        {
            if (CharacterController.gameObject.CompareTag("Player"))
            {
                _cancellationTokenSource.Cancel();
                _heroController.heroUI.FadeOutSlider();
            }

            base.ExitState();
        }
    }
}