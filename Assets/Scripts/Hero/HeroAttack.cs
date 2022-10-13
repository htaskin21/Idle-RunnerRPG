using System;
using DamageNumbersPro;
using Enemy;
using States;
using UnityEngine;
using Utils;

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
        private float baseAttackPoint;

        public float BaseAttackPoint => baseAttackPoint;

        [SerializeField]
        private float specialAttackPoint;

        public float SpecialAttackPoint => specialAttackPoint;

        [SerializeField]
        private float earthDamageMultiplier = 1;

        [SerializeField]
        private float plantDamageMultiplier = 1;

        [SerializeField]
        private float waterDamageMultiplier = 1;

        [SerializeField]
        private int attackCooldown;

        public int AttackCooldown => attackCooldown;

        [Header("Variables")]
        public SpecialAttackType specialAttackType;

        public EnemyController CurrentEnemy { get; private set; }

        public static Action<double> OnInflictDamage;

        private void Awake()
        {
            OnInflictDamage = delegate(double damage) { };
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

        private void SpawnDamagePopUp(double damage)
        {
            Vector3 enemyPosition = GameManager.Instance.EnemyController.transform.position;

            Vector3 correctedPosition = new Vector3(enemyPosition.x, enemyPosition.y + 1f, enemyPosition.z);

            if (damage < baseAttackPoint)
            {
                DamageNumber damageNumber =
                    heroAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
            }
            else
            {
                DamageNumber damageNumber =
                    tapAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
            }
        }

        public double CalculateDamage()
        {
            return baseAttackPoint * GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
        }

        private float GetDamageMultiplierByDamageType(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Earth:
                    return earthDamageMultiplier;
                case DamageType.Plant:
                    return plantDamageMultiplier;
                case DamageType.Water:
                    return waterDamageMultiplier;
                case DamageType.Normal:
                default:
                    return 1f;
            }
        }
    }

    public enum SpecialAttackType
    {
        Lightning,
        Explosion
    }
}