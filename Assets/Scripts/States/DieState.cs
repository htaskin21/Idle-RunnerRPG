using Enemy;

namespace States
{
    public class DieState : State
    {
        protected override void EnterState()
        {
            var enemyController = (EnemyController) CharacterController;
            enemyController.BoxCollider2D.enabled = false;
            
            CharacterController.AnimationController.PlayAnimation(AnimationType.Die);
            CharacterController.AnimationController.onAnimationEnd.AddListener(OnDie);

            base.EnterState();
        }

        private void OnDie()
        {
            var enemyController = (EnemyController) CharacterController;
            enemyController.enemyHealth.OnEnemyDie?.Invoke();

            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();
        }
    }
}