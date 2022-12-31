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

        private void Start()
        {
            pets[0].gameObject.SetActive(true);
        }

        public void DecideNextState()
        {
            if (heroAttack.CurrentEnemy.enemyHealth.Health <= 0)
            {
                StartRunning();
            }
            else
            {
                if (currentState.stateType != StateType.Idle)
                {
                    var idleState = GetState(StateType.Idle);
                    TransitionToState(idleState);
                }
            }
        }

        public void DecideNextStateAfterTapDamage(double damage)
        {
            var enemyHealth = heroAttack.CurrentEnemy.enemyHealth.Health - damage;

            if (enemyHealth <= 0)
            {
                TransitionToRunState().Forget();
            }
        }
        
        private async UniTask TransitionToRunState()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            await UniTask.WaitUntil(() => currentState.stateType == StateType.Idle, cancellationToken: cts.Token);

            var runState = GetState(StateType.Run);
            TransitionToState(runState);

            cts.Cancel();
        }

        public void StartRunning()
        {
            var runState = GetState(StateType.Run);
            TransitionToState(runState);
        }
    }
}