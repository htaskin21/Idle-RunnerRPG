using System;
using DamageNumbersPro;
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

        [SerializeField] public DamageNumber damageNumberPrefab;

        private void Start()
        {
            OnInflictDamage = delegate(float damage) { };
            OnInflictDamage += SpawnDamagePopUp;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag($"Enemy"))
            {
                var attackState = _heroController.GetState(StateType.Attack);
                _heroController.TransitionToState(attackState);
            }
        }

        private void SpawnDamagePopUp(float damage)
        {
            Vector3 enemyPosition = GameManager.Instance.EnemyController.transform.position;

            Vector3 correctedPosition = new Vector3(enemyPosition.x, enemyPosition.y + 1f, enemyPosition.z);

            DamageNumber damageNumber =
                damageNumberPrefab.Spawn(correctedPosition, damage);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                DamageNumber damageNumber =
                    damageNumberPrefab.Spawn(new Vector3(transform.position.x, 0, transform.position.z), attackPoint);
            }
        }
    }
}