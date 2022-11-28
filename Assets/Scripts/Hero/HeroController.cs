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
                TransitionToRunState().Forget();
            }
            else
            {
                var idleState = GetState(StateType.Idle);
                TransitionToState(idleState);
            }
        }

        private async UniTask TransitionToRunState()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            
            var runState = GetState(StateType.Run);
            await UniTask.Delay(500, cancellationToken: cts.Token);
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