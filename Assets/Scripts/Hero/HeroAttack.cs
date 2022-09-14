using System;
using System.Collections.Generic;
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

        [Header("Damage Pop-Ups")]
        [SerializeField]
        private DamageNumber heroAttackPrefab;

        [SerializeField]
        private DamageNumber tapAttackPrefab;

        [Header("Data")]
        [SerializeField]
        private float attackPoint;

        public float AttackPoint => attackPoint;

        [SerializeField]
        private float specialAttackPoint;

        public float SpecialAttackPoint => specialAttackPoint;

        [SerializeField]
        private int attackCooldown;

        public int AttackCooldown => attackCooldown;

        [Header("Variables")]
        public SpecialAttackType specialAttackType;

        public EnemyController CurrentEnemy { get; private set; }

        public static Action<float> OnInflictDamage;

        private Dictionary<DamageType, float> _damageTypeMultipliers;

        public HeroAttack(EnemyController currentEnemy)
        {
            this.CurrentEnemy = currentEnemy;
        }

        private void Awake()
        {
            InitializeDamageTypeMultipliers();
            
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

        private void InitializeDamageTypeMultipliers()
        {
            _damageTypeMultipliers = new Dictionary<DamageType, float>();

            foreach (int type in Enum.GetValues(typeof(DamageType)))
            {
                var damageType = (DamageType) type;
                _damageTypeMultipliers[damageType] = 1;
            }
        }
    }

    public enum SpecialAttackType
    {
        Lightning,
        Explosion
    }
}