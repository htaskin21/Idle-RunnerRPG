using System;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroController _heroController;

        public static Action<float> OnInflictDamage;


        [SerializeField] private float attackPoint;
        public float AttackPoint => attackPoint;


        [SerializeField] private int _attackCooldown;
        public int AttackCooldown => _attackCooldown;

        private void Start()
        {
            OnInflictDamage = delegate(float damage) { };
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            /*
            if (col.gameObject.CompareTag($"Enemy"))
            {
                if (GameManager.Instance.EnemyController.healthPoint > 0)
                {
                    var attackState = _heroController.GetState(StateType.Attack);
                    _heroController.TransitionToState(attackState);
                }
            }*/

            if (col.gameObject.CompareTag($"Enemy"))
            {
                var attackState = _heroController.GetState(StateType.Attack);
                _heroController.TransitionToState(attackState);
            }
        }
    }
}