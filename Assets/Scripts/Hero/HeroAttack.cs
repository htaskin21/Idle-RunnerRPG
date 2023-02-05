using System;
using DamageNumbersPro;
using Enemy;
using Enums;
using ScriptableObjects;
using States;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

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

        private bool _isCriticalAttack;

        public static Action<double, AttackType> OnInflictDamage;
        public static Action<double, AttackType> OnTapDamage;

        private void Awake()
        {
            /*
            OnInflictDamage = delegate(double damage, AttackType attackType) { };
            OnInflictDamage += SpawnDamagePopUp;

            OnTapDamage = delegate(double damage, AttackType attackType) { };
            OnTapDamage += SpawnDamagePopUp;
            */
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

        private void SpawnDamagePopUp(double damage, AttackType attackType)
        {
            Vector3 enemyPosition = GameManager.Instance.EnemyController.transform.position;

            Vector3 correctedPosition = new Vector3(enemyPosition.x, enemyPosition.y + 1f, enemyPosition.z);

            switch (attackType)
            {
                case AttackType.HeroDamage:

                    heroAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
                case AttackType.TapDamage:
                    tapAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
                case AttackType.CriticalDamage:
                    heroAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
                case AttackType.SpecialAttackDamage:
                    tapAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
            }

            Debug.Log("Damage Pop Up Yerine Girdi");
        }

        public double CalculateDamage()
        {
            return heroDamageDataSo.heroAttack * GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType) *
                   GetCriticalDamage();
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

        private float GetCriticalDamage()
        {
            float rnd = Random.Range(0, 100);
            var critChance = heroDamageDataSo.criticalAttackChance;

            if (rnd <= critChance)
            {
                _isCriticalAttack = true;
                return heroDamageDataSo.criticalAttack;
            }

            _isCriticalAttack = false;
            return 1;
        }

        public AttackType GetAttackType()
        {
            return _isCriticalAttack ? AttackType.SpecialAttackDamage : AttackType.CriticalDamage;
        }

        public double GetSpecialAttackDamage()
        {
            double specialAttackMultiplier = 1;
            double damageMultiplierByDamageType = 1;

            if (specialAttackType == SpecialAttackType.Lightning && CurrentEnemy.enemyDamageType == DamageType.Water)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
                specialAttackMultiplier = heroDamageDataSo.lightningSpecialAttackMultiplier;
            }

            if (specialAttackType == SpecialAttackType.IceAttack && CurrentEnemy.enemyDamageType == DamageType.Fire)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
                specialAttackMultiplier = heroDamageDataSo.WaterSpecialAttackMultiplier;
            }

            if (specialAttackType == SpecialAttackType.Explosion && CurrentEnemy.enemyDamageType == DamageType.Plant)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
                specialAttackMultiplier = heroDamageDataSo.FireSpecialAttackMultiplier;
            }

            var totalDamage = heroDamageDataSo.heroAttack * specialAttackMultiplier * damageMultiplierByDamageType;
            return totalDamage;
        }
    }
}