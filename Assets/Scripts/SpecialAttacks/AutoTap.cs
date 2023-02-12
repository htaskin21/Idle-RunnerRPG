using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Hero;
using Managers;
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

        [SerializeField]
        private HeroController _heroController;

        private CancellationTokenSource _cts;
        private CancellationTokenSource _durationCts;
        private CancellationTokenSource _cooldownCts;
        private CancellationTokenSource _uiTimerCts;

        private void Start()
        {
            var isUnlocked = specialAttackButton.SetLockState(identifier);
            if (isUnlocked)
            {
                var durations = SaveLoadManager.Instance.LoadSpecialAttackDuration();
                var isContain = durations.ContainsKey(identifier);
                if (isContain)
                {
                    var durationDate = durations[identifier];
                    if (durationDate > DateTime.UtcNow)
                    {
                        var duration = durationDate.Subtract(DateTime.UtcNow).TotalMilliseconds;
                        AutoTapRoutine(duration).Forget();
                        return;
                    }
                }

                durations = SaveLoadManager.Instance.LoadSpecialAttackCoolDown();
                isContain = durations.ContainsKey(identifier);
                if (isContain)
                {
                    var durationDate = durations[identifier];
                    if (durationDate > DateTime.UtcNow)
                    {
                        _cooldownCts = new CancellationTokenSource();
                        var duration = durationDate.Subtract(DateTime.UtcNow).TotalMilliseconds;
                        specialAttackButton.StartCoolDownState((int) duration,
                            (int) heroDamageDataSo.autoTapAttackCooldown, _cooldownCts).Forget();
                    }
                }
            }
            else
            {
                SpecialAttackUIRow.OnUpdateSpecialAttack += CheckLockState;
            }
        }

        private async UniTask AutoTapRoutine(double duration)
        {
            _durationCts = new CancellationTokenSource();
            _cooldownCts = new CancellationTokenSource();
            _cts = new CancellationTokenSource();

            StartTimerUI((int) duration, _durationCts, _cooldownCts).Forget();

            await UniTask.WaitUntil(
                () => _heroController.heroAttack.CurrentEnemy != null, cancellationToken:
                _cts.Token);

            HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack, AttackType.TapDamage);

            var finishTime = DateTime.UtcNow.AddMilliseconds(duration);
            while (finishTime >= DateTime.UtcNow || _cts.IsCancellationRequested == false)
            {
                await UniTask.WaitUntil(
                    () => GameManager.Instance.EnemyController.enemyHealth.Health > 0 &&
                          GameManager.Instance.EnemyController.TapDamageController.isTapDamageEnable, cancellationToken:
                    _cts.Token);
                await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown, cancellationToken: _cts.Token);
                if (finishTime >= DateTime.UtcNow)
                {
                    HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack, AttackType.TapDamage);
                }
            }

            _cts.Cancel();
        }

        private async UniTask StartTimerUI(int autoTapAttackDuration, CancellationTokenSource durationCts,
            CancellationTokenSource cooldownCts)
        {
            _uiTimerCts = new CancellationTokenSource();

            SaveLoadManager.Instance.SaveSpecialAttackDuration(identifier,
                DateTime.UtcNow.AddMilliseconds(autoTapAttackDuration));
           
            var totalDuration = autoTapAttackDuration + heroDamageDataSo.autoTapAttackCooldown;
            SaveLoadManager.Instance.SaveSpecialAttackCoolDown(identifier,
                DateTime.UtcNow.AddMilliseconds(totalDuration));
            
            specialAttackButton.StartDurationState(autoTapAttackDuration, durationCts).Forget();
            await UniTask.WaitUntilCanceled(durationCts.Token);
            
            specialAttackButton.StartCoolDownState((int) heroDamageDataSo.autoTapAttackCooldown,
                (int) heroDamageDataSo.autoTapAttackCooldown, cooldownCts).Forget();
            await UniTask.WaitUntilCanceled(cooldownCts.Token);

            _uiTimerCts.Cancel();
        }

        public void StartAutoTap()
        {
            var autoTapAttackDuration =
                heroDamageDataSo.autoTapAttackDuration;

            AutoTapRoutine(autoTapAttackDuration).Forget();
        }

        private void CheckLockState(int id)
        {
            if (id == identifier)
            {
                specialAttackButton.SetLockState(identifier);
            }
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
        }
    }
}