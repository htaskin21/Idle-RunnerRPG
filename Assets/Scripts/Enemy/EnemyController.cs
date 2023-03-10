using Enums;
using Hero;
using States;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyController : CharacterController
    {
        public EnemyHealth enemyHealth;

        [SerializeField]
        private BoxCollider2D boxCollider2D;

        public Transform specialAttackPosition;

        public Transform damagePopUpPosition;

        [SerializeField]
        private TapDamageController tapDamageController;

        public TapDamageController TapDamageController => tapDamageController;

        public EnemyLoot enemyLoot;
        
        public BoxCollider2D BoxCollider2D => boxCollider2D;

        public DamageType enemyDamageType;

        public Image enemyDamageTypeIcon;

        public int enemyLevel;

        private void Start()
        {
            HeroAttack.OnInflictDamage += TakeDamage;
            HeroAttack.OnTapDamage += TakeDamage;
            
            tapDamageController.isTapDamageEnable = false;
        }

        private void TakeDamage(double attackPoint, AttackType attackType)
        {
            if (!(enemyHealth.Health > 0)) return;
            enemyHealth.SetHealth(attackPoint);

            var hitState = GetState(StateType.Hit);
            TransitionToState(hitState);
        }

        private void OnDestroy()
        {
            HeroAttack.OnInflictDamage -= TakeDamage;
            HeroAttack.OnTapDamage -= TakeDamage;
        }
    }
}