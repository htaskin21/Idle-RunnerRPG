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
        public static Action<double> OnTapDamage;

        private void Awake()
        {
            OnInflictDamage = delegate(double damage) { };
            OnInflictDamage += SpawnDamagePopUp;

            OnTapDamage = delegate(double damage) { };
            OnTapDamage += SpawnDamagePopUp;
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

            var a = CalcUtils.FormatNumber(damage);

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
            double specialAttackMultiplier = 0;
            double damageMultiplierByDamageType = 0;


            switch (specialAttackType)
            {
                case SpecialAttackType.Lightning:
                    specialAttackMultiplier = heroDamageDataSo.lightningSpecialAttackMultiplier;
                    break;

                case SpecialAttackType.Explosion:
                    specialAttackMultiplier = heroDamageDataSo.FireSpecialAttackMultiplier;
                    break;

                case SpecialAttackType.IceAttack:
                    specialAttackMultiplier = heroDamageDataSo.WaterSpecialAttackMultiplier;
                    break;

                default:
                    specialAttackMultiplier = 1;
                    break;
            }

            if (specialAttackType == SpecialAttackType.Lightning && CurrentEnemy.enemyDamageType == DamageType.Water)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
            }

            if (specialAttackType == SpecialAttackType.IceAttack && CurrentEnemy.enemyDamageType == DamageType.Fire)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
            }

            if (specialAttackType == SpecialAttackType.Explosion && CurrentEnemy.enemyDamageType == DamageType.Plant)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
            }

            var totalDamage = heroDamageDataSo.heroAttack * specialAttackMultiplier * damageMultiplierByDamageType;
            return totalDamage;
        }
    }

    public enum SpecialAttackType
    {
        Lightning,
        Explosion,
        IceAttack,
        AutoTap
    }
}