namespace States
{
    public class DieState : State
    {
        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.Die);

            characterController.AnimationController.OnAnimationEnd.AddListener(OnDie);

            base.EnterState();
        }

        protected override void ExitState()
        {
            //GameManager.Instance.EnemyController.enemyHealth.OnEnemyDie?.Invoke();

            //characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();
            // Destroy(this.gameObject);
            base.ExitState();
        }

        private void OnDie()
        {
            GameManager.Instance.EnemyController.enemyHealth.OnEnemyDie?.Invoke();

            characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();
        }
    }
}