using System;
using Enemy;
using Enums;
using Managers;
using ScriptableObjects;
using States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField]
        private HeroController heroController;

        [Header("Data")]
        [SerializeField]
        private HeroDamageDataSO heroDamageDataSo;

        public HeroDamageDataSO HeroDamageDataSo => heroDamageDataSo;

        [Header("Variables")]
        public SpecialAttackType specialAttackType;

        public EnemyController CurrentEnemy { get; set; }

        private bool _isCriticalAttack;

        public static Action<double, AttackType> OnInflictDamage;
        public static Action<double, AttackType> OnTapDamage;

        private void Awake()
        {
            OnInflictDamage = delegate(double damage, AttackType attackType) { };
            OnTapDamage = delegate(double damage, AttackType attackType) { };

            OnTapDamage += UpdateTapCount;

            PlayerPrefs.SetInt("TapCount", 0);
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

        public double CalculateDamage()
        {
            return (heroDamageDataSo.heroAttack + GetPassingTime() + GetTapCount()) *
                   GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType) *
                   GetCriticalDamage() * heroDamageDataSo.currentRageAmount;
        }

        public double CalculateTapDamage()
        {
            var critDamage = 1f;
            if (heroDamageDataSo.isCriticalTapActive)
            {
                critDamage = GetCriticalDamage();
            }

            return heroDamageDataSo.tapAttack *
                   critDamage;
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
                case DamageType.Holy:
                    return heroDamageDataSo.holyDamageMultiplier;
                case DamageType.Normal:
                default:
                    return 1f;
            }
        }

        private int GetTapCount()
        {
            var tapCount = 0;

            if (heroDamageDataSo.isAddClickCountToDPS)
            {
                tapCount = PlayerPrefs.GetInt("TapCount", 0);
            }

            return tapCount;
        }

        private double GetPassingTime()
        {
            var timeAttack = 0;

            if (heroDamageDataSo.isAddTimeToDamageActive)
            {
                var startingTime = SaveLoadManager.Instance.LoadGameStartTime();
                var currentTime = DateTime.UtcNow;

                timeAttack = currentTime.Subtract(startingTime).Minutes;
            }

            return timeAttack;
        }

        private float GetCriticalDamage()
        {
            float rnd = Random.Range(0.1f, 100);
            var critChance = heroDamageDataSo.criticalAttackChance;

            if (rnd <= critChance)
            {
                _isCriticalAttack = true;
                return heroDamageDataSo.criticalAttackMultiplier;
            }

            _isCriticalAttack = false;
            return 1;
        }

        public AttackType GetAttackType()
        {
            return _isCriticalAttack ? AttackType.CriticalDamage : AttackType.HeroDamage;
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
                specialAttackMultiplier = heroDamageDataSo.waterSpecialAttackMultiplier;
            }

            if (specialAttackType == SpecialAttackType.Explosion && CurrentEnemy.enemyDamageType == DamageType.Plant)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
                specialAttackMultiplier = heroDamageDataSo.fireSpecialAttackMultiplier;
            }

            if (specialAttackType == SpecialAttackType.Holy && CurrentEnemy.enemyDamageType == DamageType.Normal)
            {
                damageMultiplierByDamageType = GetDamageMultiplierByDamageType(CurrentEnemy.enemyDamageType);
                specialAttackMultiplier = heroDamageDataSo.holySpecialAttackMultiplier;
            }

            var totalDamage = heroDamageDataSo.heroAttack * specialAttackMultiplier * damageMultiplierByDamageType *
                              heroDamageDataSo.currentRageAmount;
            return totalDamage;
        }

        private void UpdateTapCount(double damage, AttackType attackType)
        {
            var count = PlayerPrefs.GetInt("TapCount", 0);
            count++;
            PlayerPrefs.SetInt("TapCount", count);
        }
    }
}