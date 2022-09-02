using System;
using DamageNumbersPro;
using Enemy;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField]
        private HeroController heroController;

        public EnemyController CurrentEnemy { get; private set; }

        public static Action<float> OnInflictDamage;

        [SerializeField]
        private float attackPoint;
        public float AttackPoint => attackPoint;
        
        [SerializeField]
        private float specialAttackPoint;
        public float SpecialAttackPoint => specialAttackPoint;

        [SerializeField]
        private int attackCooldown;
        public int AttackCooldown => attackCooldown;

        public SpecialAttackType specialAttackType;
        
        [Space]
        [Header("Damage Pop-Ups")]
        [SerializeField]
        private DamageNumber heroAttackPrefab;

        [SerializeField]
        private DamageNumber tapAttackPrefab;

        public HeroAttack(EnemyController currentEnemy)
        {
            this.CurrentEnemy = currentEnemy;
        }
        
        private void Awake()
        {
            OnInflictDamage = delegate(float damage) { };
            OnInflictDamage += SpawnDamagePopUp;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag($"Enemy"))
            {
                CurrentEnemy = col.GetComponent<EnemyController>();

                var attackState = heroController.GetState(StateType.Attack);
                heroController.TransitionToState(attackState);
            }
        }

        private void SpawnDamagePopUp(float damage)
        {
            Vector3 enemyPosition = GameManager.Instance.EnemyController.transform.position;

            Vector3 correctedPosition = new Vector3(enemyPosition.x, enemyPosition.y + 1f, enemyPosition.z);

            if (damage < attackPoint)
            {
                DamageNumber damageNumber =
                    heroAttackPrefab.Spawn(correctedPosition, damage);
            }
            else
            {
                DamageNumber damageNumber =
                    tapAttackPrefab.Spawn(correctedPosition, damage);
            }
        }
    }

    public enum SpecialAttackType
    {
        Lightning,
        Explosion
    }
}