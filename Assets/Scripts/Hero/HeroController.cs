using System.Collections.Generic;
using System.Threading;
using Enums;
using Managers;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroController : CharacterController
    {
        public HeroAttack heroAttack;

        public HeroUI heroUI;

        [SerializeField]
        private List<Pet> pets;

        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            //pets[0].gameObject.SetActive(true);
            //pets[1].gameObject.SetActive(true);

            HeroAttack.OnTapDamage += DecideNextStateAfterTapDamage;
        }

        public void DecideNextState()
        {
            if (heroAttack.CurrentEnemy.enemyHealth.Health <= 0)
            {
                heroAttack.CurrentEnemy = null;
                StartRunning();
            }
            else
            {
                var idleState = GetState(StateType.Idle);
                TransitionToState(idleState);
            }
        }

        private void DecideNextStateAfterTapDamage(double damage, AttackType attackType)
        {
            double enemyHealth = 0;

            if (heroAttack.CurrentEnemy == null)
            {
                enemyHealth = 0;
            }
            else
            {
                enemyHealth =
                heroAttack.CurrentEnemy.enemyHealth.Health - damage;
            }
               

            if (enemyHealth <= 0)
            {
                if (currentState.stateType is StateType.Idle or StateType.WakeUp)
                {
                    heroAttack.CurrentEnemy = null;
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
            if (currentState.stateType is StateType.Attack or StateType.SpecialAttack or StateType.WakeUp)
            {
                return;
            }

            if (currentState.stateType != StateType.Idle)
            {
                var idleState = GetState(StateType.Idle);
                TransitionToState(idleState);
            }
        }

        public void StartRunning()
        {
            var runState = GetState(StateType.Run);
            TransitionToState(runState);
        }
    }
}