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
                if (GameManager.Instance.EnemyController.healthPoint > 0)
                {
                    var attackState = _heroController.GetState(StateType.Attack);
                    _heroController.TransitionToState(attackState);
                }
            }
        }

        private void InflictDamage(float damage)
        {
            var idleState = _heroController.GetState(StateType.Idle);
            _heroController.TransitionToState(idleState);
        }
    }
}