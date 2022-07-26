using System;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroController : CharacterController
    {
        [SerializeField] private HeroMovement _heroMovement;

        public HeroMovement HeroMovement => _heroMovement;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var runState = _states.Find(x => x.stateType == StateType.Run);
                TransitionToState(runState);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag($"Enemy"))
            {
                var attackState = _states.Find(x => x.stateType == StateType.Attack);
                TransitionToState(attackState);
            }
            
        }
    }
}