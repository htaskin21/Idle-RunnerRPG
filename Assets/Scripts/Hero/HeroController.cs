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
            }
        }

        public void DecideNextStateAfterTapDamage(double damage)
        {
            var enemyHealth = heroAttack.CurrentEnemy.enemyHealth.Health - damage;

            if (enemyHealth <= 0)
            {
                if (currentState.stateType == StateType.Idle)
                {
                    StartRunning();
                }
            }
            else
            {
                TransitionToIdleState();
            }
        }

        private void TransitionToIdleState()
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
            if (currentState.stateType == StateType.Attack || currentState.stateType == StateType.SpecialAttack)
            {
                await UniTask.WaitUntil(() => currentState.stateType == StateType.Idle, cancellationToken: cts.Token);
            }
            

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