using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using States;
using UI;
using UnityEngine;

namespace SpecialAttacks
{
    public class AutoTap : MonoBehaviour
    {
        [SerializeField]
        private string identifier;

        [SerializeField]
        private HeroDamageDataSO heroDamageDataSo;

        [SerializeField]
        private SpecialAttackButton specialAttackButton;

        private CancellationTokenSource _cts;
        private CancellationTokenSource _buttonCts;

        private void Start()
        {
         
        }

        private async UniTask AutoTapRoutine()
        {
            _cts = new CancellationTokenSource();
            _buttonCts = new CancellationTokenSource();

            var autoTapAttackDuration =
                heroDamageDataSo.autoTapAttackDuration;
            var finishTime = DateTime.UtcNow.AddMilliseconds(autoTapAttackDuration);

            specialAttackButton.StartDurationState((int) autoTapAttackDuration, _buttonCts).Forget();

            while (finishTime >= DateTime.UtcNow)
            {
                HeroAttack.OnInflictDamage?.Invoke(heroDamageDataSo.tapAttack);

                GameManager.Instance.HeroController.DecideNextState();

                await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown);
                await UniTask.WaitUntil(() =>
                    GameManager.Instance.EnemyController.enemyHealth.Health > 0 &&
                    GameManager.Instance.HeroController.currentState.stateType != StateType.Run);
            }

            specialAttackButton.StartCoolDownState((int) heroDamageDataSo.autoTapAttackCooldown, _buttonCts).Forget();
            _cts.Cancel();
        }

        public void StartAutoTap()
        {
            AutoTapRoutine().Forget();
        }
    }
}