using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace States
{
    public class IdleState : State
    {
        //[SerializeField] private State attackState;

        //[SerializeField] private State hitState;

        [SerializeField] private State wakeUpState;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken;


        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Idle);

            base.EnterState();

            if (characterController.gameObject.CompareTag("Player"))
            {
                _cancellationToken = _cancellationTokenSource.Token;

                WaitForNextAttack().Forget();
            }
        }

        private async UniTask WaitForNextAttack()
        {
            await UniTask.Delay(GameManager.Instance.HeroController.AttackCooldown,
                cancellationToken: _cancellationToken);

            //var wakeUpState = characterController.GetState(StateType.WakeUp);
            characterController.TransitionToState(wakeUpState);
        }

        protected override void ExitState()
        {
            _cancellationTokenSource.Cancel();
            base.ExitState();
        }
    }
}