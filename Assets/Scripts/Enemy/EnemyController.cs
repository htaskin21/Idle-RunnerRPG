using Hero;
using States;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : CharacterController
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;

        public BoxCollider2D BoxCollider2D => _boxCollider2D;

        private void Start()
        {
            HeroAttack.OnInflictDamage += TakeDamage;
        }

        private void TakeDamage(float attackPoint)
        {
            healthPoint -= attackPoint;
            var hitState = GetState(StateType.Hit);
            TransitionToState(hitState);

            if (healthPoint <= 0)
            {
                var runState = GameManager.Instance.HeroController.GetState(StateType.Run);
                GameManager.Instance.HeroController.TransitionToState(runState);
            }
        }
    }
}