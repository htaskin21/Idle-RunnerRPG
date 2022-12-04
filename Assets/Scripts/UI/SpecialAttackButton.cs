using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using States;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpecialAttackButton : MonoBehaviour
    {
        [SerializeField]
        private SpecialAttackType specialAttackType;

        [SerializeField]
        private HeroDamageDataSO heroDamageDataSo;

        public Button buttonComponent;
        public Image buttonBackground;

        private CancellationTokenSource _cts;

        public void StartSpecialAttack()
        {
            GameManager.Instance.HeroController.heroAttack.specialAttackType = specialAttackType;

            var specialAttackState = GameManager.Instance.HeroController.GetState(StateType.SpecialAttack);
            GameManager.Instance.HeroController.TransitionToState(specialAttackState);
        }

        private async UniTask AutoTapRoutine()
        {
            _cts = new CancellationTokenSource();

            var autoTapAttackDuration =
                heroDamageDataSo.autoTapAttackDuration;
            var finishTime = DateTime.UtcNow.AddMilliseconds(autoTapAttackDuration);

            while (finishTime >= DateTime.UtcNow)
            {
                HeroAttack.OnInflictDamage?.Invoke(heroDamageDataSo.tapAttack);

                GameManager.Instance.HeroController.DecideNextState();

                await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown);
                await UniTask.WaitUntil(() =>
                    GameManager.Instance.EnemyController.enemyHealth.Health > 0 &&
                    GameManager.Instance.HeroController.currentState.stateType != StateType.Run);
            }

            _cts.Cancel();
        }

        public void StartAutoTap()
        {
            AutoTapRoutine().Forget();
        }
    }
}