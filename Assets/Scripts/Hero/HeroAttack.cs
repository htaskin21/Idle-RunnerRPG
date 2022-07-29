using System;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroController _heroController;

        public static Action<float> OnInflictDamage;

        private void Start()
        {
            OnInflictDamage = delegate(float damage) { };
            OnInflictDamage += InflictDamage;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag($"Enemy"))
            {
                var attackState = _heroController._states.Find(x => x.stateType == StateType.Attack);
                _heroController.TransitionToState(attackState);
            }
        }

        private void InflictDamage(float damage)
        {
            var idleState = _heroController._states.Find(x => x.stateType == StateType.Idle);
            _heroController.TransitionToState(idleState);
        }
    }
}