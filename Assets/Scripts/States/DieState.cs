namespace States
{
    public class DieState : State
    {
        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.Die);

            CharacterController.AnimationController.onAnimationEnd.AddListener(OnDie);

            base.EnterState();
        }

        protected override void ExitState()
        {
            base.ExitState();
        }

        private void OnDie()
        {
            GameManager.Instance.EnemyController.enemyHealth.OnEnemyDie?.Invoke();

            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();
        }
    }
}