using DamageNumbersPro;
using Enums;
using Hero;
using UnityEngine;
using Utils;

namespace UI
{
    public class DamagePopUpManager : MonoBehaviour
    {
        [Header("Damage Pop-Ups")]
        [SerializeField]
        private DamageNumber _heroAttackPrefab;

        [SerializeField]
        private DamageNumber _tapAttackPrefab;

        [SerializeField]
        private DamageNumber _critAttackPrefab;

        [SerializeField]
        private DamageNumber _specialAttackPrefab;

        private void Start()
        {
            HeroAttack.OnInflictDamage = delegate(double damage, AttackType attackType) { };
            HeroAttack.OnInflictDamage += SpawnDamagePopUp;

            HeroAttack.OnTapDamage = delegate(double damage, AttackType attackType) { };
            HeroAttack.OnTapDamage += SpawnDamagePopUp;
        }

        private void SpawnDamagePopUp(double damage, AttackType attackType)
        {
            Vector3 enemyPosition = GameManager.Instance.EnemyController.transform.position;
            Vector3 correctedPosition = new Vector3(enemyPosition.x, enemyPosition.y + 1f, enemyPosition.z);

            switch (attackType)
            {
                case AttackType.HeroDamage:

                    _heroAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
                case AttackType.TapDamage:
                    _tapAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
                case AttackType.CriticalDamage:
                    _critAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
                case AttackType.SpecialAttackDamage:
                    _specialAttackPrefab.Spawn(correctedPosition, CalcUtils.FormatNumber(damage));
                    break;
            }
        }
    }
}