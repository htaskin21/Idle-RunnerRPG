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
            
            while (duration > 0 && _cts.IsCancellationRequested == false)
            {
                await UniTask.Delay(100, cancellationToken: _cts.Token);
                duration -= 100;
            }

            heroDamageDataSo.currentRageAmount = 1;
            _cts.Cancel();
        }
    }
}