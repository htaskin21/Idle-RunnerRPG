using Hero;
using States;

namespace Enemy
{
    public class EnemyController : CharacterController
    {
        private void Start()
        {
            HeroAttack.OnInflictDamage += TakeDamage;
        }

        private void TakeDamage(float attackPoint)
        {
            var hitState = _states.Find(x => x.stateType == StateType.Hit);
            TransitionToState(hitState);
            healthPoint -= attackPoint;

            if (healthPoint <= 0)
            {
                var dieState = _states.Find(x => x.stateType == StateType.Die);
                TransitionToState(dieState);
                HeroMovement.OnHeroStartRunning?.Invoke();
            }
            else
            {
                var idleState = _states.Find(x => x.stateType == StateType.Idle);
                TransitionToState(idleState);
            }
        }
    }
}