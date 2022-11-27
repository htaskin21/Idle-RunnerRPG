using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using UI;
using UnityEngine;

namespace States
{
    public class SpecialAttackState : State
    {
        public State runState;
        public State idleState;

        [Header("Special Attack Prefabs")]
        public GameObject explosionAttackPrefab;

        public GameObject lightningAttackPrefab;

        public GameObject iceAttackPrefab;

        protected override void EnterState()
        {
            ButtonController.OnActiveAttackButtons?.Invoke(false);
            
            CharacterController.AnimationController.PlayAnimation(AnimationType.SpecialAttack);

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                SetSpecialAttackPrefab().SetActive(true));

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(
                    GameManager.Instance.HeroController.heroAttack.GetSpecialAttackDamage()));

            CharacterController.AnimationController.onAnimationEnd.AddListener(DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            CharacterController.AnimationController.onAnimationAction.RemoveAllListeners();
            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();

            base.ExitState();
        }

        private void DecideNextState()
        {
            if (GameManager.Instance.HeroController.heroAttack.CurrentEnemy.enemyHealth.Health <= 0)
            {
                TransitionToRunState().Forget();
            }
            else
            {
                CharacterController.TransitionToState(idleState);
            }
        }

        private async UniTask TransitionToRunState()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            await UniTask.Delay(500, cancellationToken: cts.Token);
            CharacterController.TransitionToState(runState);

            cts.Cancel();
        }

        private GameObject SetSpecialAttackPrefab()
        {
            GameObject specialAttack = null;

            switch (GameManager.Instance.HeroController.heroAttack.specialAttackType)
            {
                case SpecialAttackType.Explosion:
                    specialAttack = explosionAttackPrefab;
                    break;
                case SpecialAttackType.Lightning:
                    specialAttack = lightningAttackPrefab;
                    break;
                case SpecialAttackType.IceAttack:
                    specialAttack = iceAttackPrefab;
                    break;
            }

            var position = specialAttack.transform.position;
            position = new Vector3(GameManager.Instance.EnemyController.transform.position.x,
                position.y, position.z);
            specialAttack.transform.position = position;

            return specialAttack;
        }
    }
}