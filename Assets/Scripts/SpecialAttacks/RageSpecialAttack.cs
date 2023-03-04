using System;
using Cysharp.Threading.Tasks;

namespace SpecialAttacks
{
    public class RageSpecialAttack : DurationalSpecialAttack
    {
        protected override async UniTask DurationalSpecialAttackRoutine(double duration)
        {
            base.DurationalSpecialAttackRoutine(duration).Forget();

            var baseSpecialAttackCoolDown = heroDamageDataSo.GetCoolDownBySpecialAttackType(_specialAttackType);
            var baseSpecialAttackDuration = heroDamageDataSo.GetDurationBySpecialAttackType(_specialAttackType);

            StartTimerUI((int) duration, baseSpecialAttackCoolDown, baseSpecialAttackDuration, _durationCts,
                _cooldownCts).Forget();

            heroDamageDataSo.currentRageAmount = heroDamageDataSo.rageAmount;

            var finishTime = DateTime.UtcNow.AddMilliseconds(duration);
            while (finishTime >= DateTime.UtcNow || _cts.IsCancellationRequested == false)
            {
                await UniTask.Delay(100, cancellationToken: _cts.Token);
            }

            heroDamageDataSo.currentRageAmount = 1;
            _cts.Cancel();
        }
    }
}