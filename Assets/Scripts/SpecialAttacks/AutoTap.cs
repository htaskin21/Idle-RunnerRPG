using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace SpecialAttacks
{
    public class AutoTap : MonoBehaviour
    {
        [SerializeField]
        private int identifier;

        [SerializeField]
        private HeroDamageDataSO heroDamageDataSo;

        [SerializeField]
        private SpecialAttackButton specialAttackButton;

        private CancellationTokenSource _cts;
        private CancellationTokenSource _durationCts;
        private CancellationTokenSource _cooldownCts;
        private CancellationTokenSource _uiTimerCts;

        private void Start()
        {
            /*
            var dictionary = SaveLoadManager.Instance.LoadSkillUpgrade();
            if (dictionary.ContainsKey(identifier) == false)
            {
                specialAttackButton.StartLockState();
            }*/
        }

        private async UniTask AutoTapRoutine()
        {
            _durationCts = new CancellationTokenSource();
            _cooldownCts = new CancellationTokenSource();

            var autoTapAttackDuration =
                heroDamageDataSo.autoTapAttackDuration;
            var finishTime = DateTime.UtcNow.AddMilliseconds(autoTapAttackDuration);

            StartTimerUI((int) autoTapAttackDuration, _durationCts, _cooldownCts).Forget();

            HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack);
            while (finishTime >= DateTime.UtcNow)
            {
                await UniTask.WaitUntil(
                    () => GameManager.Instance.EnemyController.enemyHealth.Health > 0 &&
                          GameManager.Instance.EnemyController.TapDamageController.isTapDamageEnable);
                await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown);
                if (finishTime >= DateTime.UtcNow)
                {
                    HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack);
                }
            }

            _cts.Cancel();
        }

        private async UniTask StartTimerUI(int autoTapAttackDuration, CancellationTokenSource durationCts,
            CancellationTokenSource cooldownCts)
        {
            _uiTimerCts = new CancellationTokenSource();

            specialAttackButton.StartDurationState(autoTapAttackDuration, durationCts).Forget();
            await UniTask.WaitUntilCanceled(durationCts.Token);

            specialAttackButton.StartCoolDownState((int) heroDamageDataSo.autoTapAttackCooldown, cooldownCts).Forget();
            await UniTask.WaitUntilCanceled(cooldownCts.Token);

            _uiTimerCts.Cancel();
        }

        public void StartAutoTap()
        {
            _cts = new CancellationTokenSource();
            AutoTapRoutine().Forget();
        }
    }
}