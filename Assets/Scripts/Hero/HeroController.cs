using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroController : CharacterController
    {
        public HeroAttack heroAttack;

        public HeroUI heroUI;

        [SerializeField]
        private List<PetController> pets;

        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            pets[0].gameObject.SetActive(true);

            HeroAttack.OnTapDamage += DecideNextStateAfterTapDamage;
        }

        public void DecideNextState()
        {
            if (heroAttack.CurrentEnemy.enemyHealth.Health <= 0)
            {
                StartRunning();
            }
            else
            {
                var idleState = GetState(StateType.Idle);
                TransitionToState(idleState);
                //_cancellationTokenSource = new CancellationTokenSource();
                //TransitionToIdleState().Forget();
            }
        }

        public void DecideNextStateAfterTapDamage(double damage)
        {
            var enemyHealth = heroAttack.CurrentEnemy.enemyHealth.Health - damage;

            if (enemyHealth <= 0)
            {
                TransitionToRunState().Forget();
            }
            else
            {
                TransitionToIdleState2();
            }
        }

        private async UniTask TransitionToIdleState()
        {
            await UniTask.WaitUntil(() =>
                currentState.stateType != StateType.Attack && currentState.stateType != StateType.SpecialAttack);

            if (currentState.stateType != StateType.Idle)
            {
                var idleState = GetState(StateType.Idle);
                TransitionToState(idleState);
            }

            _cancellationTokenSource.Cancel();
        }

        private void TransitionToIdleState2()
        {
            if (currentState.stateType == StateType.Attack || currentState.stateType == StateType.SpecialAttack)
            {
                return;
            }

            if (currentState.stateType != StateType.Idle)
            {
                var idleState = GetState(StateType.Idle);
                TransitionToState(idleState);
            }
        }

        private async UniTask TransitionToRunState()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            await UniTask.WaitUntil(() => currentState.stateType == StateType.Idle, cancellationToken: cts.Token);

            StartRunning();

            cts.Cancel();
        }

        public void StartRunning()
        {
            var runState = GetState(StateType.Run);
            TransitionToState(runState);
        }
    }
}