using System;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroController : CharacterController
    {
        [SerializeField] private int _attackCooldown;

        public int AttackCooldown => _attackCooldown;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var runState = GetState(StateType.Run);
                TransitionToState(runState);
            }
        }
    }
}