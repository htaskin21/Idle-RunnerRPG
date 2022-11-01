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
        private HeroDamageDataSO heroDamageDataSo;

        public HeroDamageDataSO HeroDamageDataSo => heroDamageDataSo;

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

            if (damage < heroDamageDataSo.heroAttack)
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
            return heroDamageDataSo.heroAttack * GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
        }

        private double GetDamageMultiplierByDamageType(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Earth:
                    return heroDamageDataSo.earthDamageMultiplier;
                case DamageType.Plant:
                    return heroDamageDataSo.plantDamageMultiplier;
                case DamageType.Water:
                    return heroDamageDataSo.waterDamageMultiplier;
                case DamageType.Normal:
                default:
                    return 1f;
            }
        }

        public double GetSpecialAttackDamage()
        {
            switch (specialAttackType)
            {
                case SpecialAttackType.Lightning:
                    return heroDamageDataSo.lightningAttackPoint;

                case SpecialAttackType.Explosion:
                    return 1;

                default:
                    return 1;
            }
        }
    }

    public enum SpecialAttackType
    {
        Lightning,
        Explosion
    }
}