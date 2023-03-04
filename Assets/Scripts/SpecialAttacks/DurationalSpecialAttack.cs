using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Items.Potion;
using Managers;
using UI;
using UI.SpecialAttack;
using UnityEngine;

namespace SpecialAttacks
{
    public class DurationalSpecialAttack : BaseSpecialAttack
    {
        [SerializeField]
        private BoostIconController _boostIconController;

        protected CancellationTokenSource _durationCts;
        protected CancellationTokenSource _cooldownCts;
        protected CancellationTokenSource _uiTimerCts;

        private void Start()
        {
            var isUnlocked = specialAttackButton.SetLockState(identifier);
            if (isUnlocked)
            {
                var coolDown = heroDamageDataSo.GetCoolDownBySpecialAttackType(_specialAttackType);
                SetSpecialAttackState(DurationalSpecialAttackRoutine, coolDown);
            }
            else
            {
                SpecialAttackUIRow.OnUpdateSpecialAttack += CheckLockState;
            }

            RefreshPotion.OnRefreshAllSpecialAttack += RefreshSpecialAttack;
        }


        protected virtual async UniTask DurationalSpecialAttackRoutine(double duration)
        {
            _durationCts = new CancellationTokenSource();
            _cooldownCts = new CancellationTokenSource();
            _cts = new CancellationTokenSource();

            _boostIconController.SetBoostIcon(DateTime.UtcNow.AddMilliseconds(duration));
        }

        protected async UniTask StartTimerUI(int remainingSpecialAttackDuration, int baseSpecialAttackCoolDown,
            int baseSpecialAttackDuration, CancellationTokenSource durationCts,
            CancellationTokenSource cooldownCts)
        {
            _uiTimerCts = new CancellationTokenSource();

            SaveLoadManager.Instance.SaveSpecialAttackDuration(identifier,
                DateTime.UtcNow.AddMilliseconds(remainingSpecialAttackDuration));

            var totalDuration = remainingSpecialAttackDuration + baseSpecialAttackCoolDown;
            SaveLoadManager.Instance.SaveSpecialAttackCoolDown(identifier,
                DateTime.UtcNow.AddMilliseconds(totalDuration));

            specialAttackButton
                .StartDurationState(remainingSpecialAttackDuration, baseSpecialAttackDuration, durationCts)
                .Forget();
            await UniTask.WaitUntilCanceled(durationCts.Token);

            specialAttackButton.StartCoolDownState(baseSpecialAttackCoolDown,
                baseSpecialAttackCoolDown, cooldownCts).Forget();
            await UniTask.WaitUntilCanceled(cooldownCts.Token);

            _uiTimerCts.Cancel();
        }

        /// <summary>
        /// /Check special attack save file for this special attack is coolDown or already using.
        /// </summary>
        private void SetSpecialAttackState(Func<double, UniTask> method, double coolDown)
        {
            var durations = SaveLoadManager.Instance.LoadSpecialAttackDuration();
            var isContain = durations.ContainsKey(identifier);
            if (isContain)
            {
                var durationDate = durations[identifier];
                if (durationDate > DateTime.UtcNow)
                {
                    var duration = durationDate.Subtract(DateTime.UtcNow).TotalMilliseconds;
                    method(duration).Forget();
                    return;
                }
            }

            if (isContain)
            {
                LoadCoolDownState(_cooldownCts);
            }
        }

        public void StartSpecialAttack()
        {
            var specialAttackDuration =
                heroDamageDataSo.GetDurationBySpecialAttackType(_specialAttackType);

            DurationalSpecialAttackRoutine(specialAttackDuration).Forget();
        }

        private void RefreshSpecialAttack()
        {
            if (specialAttackButton.SpecialAttackButtonState == SpecialAttackButtonState.OnCoolDown)
            {
                SaveLoadManager.Instance.SaveSpecialAttackCoolDown(identifier,
                    DateTime.UtcNow);

                _cooldownCts?.Cancel();
            }
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
        }
    }
}