using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Hero;

namespace SpecialAttacks
{
    public class AutoTap : DurationalSpecialAttack
    {
        private CancellationTokenSource _activeTapCts;

        protected override async UniTask DurationalSpecialAttackRoutine(double duration)
        {
            base.DurationalSpecialAttackRoutine(duration).Forget();

            var baseSpecialAttackCoolDown = heroDamageDataSo.GetCoolDownBySpecialAttackType(_specialAttackType);
            var baseSpecialAttackDuration = heroDamageDataSo.GetDurationBySpecialAttackType(_specialAttackType);

            StartTimerUI((int) duration, baseSpecialAttackCoolDown, baseSpecialAttackDuration, _durationCts,
                _cooldownCts).Forget();

            await UniTask.WaitUntil(
                () => _heroController.heroAttack.CurrentEnemy != null, cancellationToken:
                _cts.Token);

            var passingTimeToTap = 0;

            while (duration > 0 && _cts.IsCancellationRequested == false)
            {
                await UniTask.Delay(100, cancellationToken: _cts.Token);
                duration -= 100;
                passingTimeToTap += 100;

                if (passingTimeToTap >= 500 && _activeTapCts == null)
                {
                    passingTimeToTap = 0;
                    ActivateTap().Forget();
                }
            }

            _cts.Cancel();
        }

        private async UniTask ActivateTap()
        {
            if (_activeTapCts != null)
            {
                return;
            }

            _activeTapCts = new CancellationTokenSource();

            await UniTask.WaitUntil(
                () => _heroController.heroAttack.CurrentEnemy != null &&
                      _heroController.heroAttack.CurrentEnemy.enemyHealth.Health > 0 &&
                      _heroController.heroAttack.CurrentEnemy.TapDamageController.isTapDamageEnable,
                cancellationToken:
                _cts.Token);

            HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack, AttackType.TapDamage);

            _activeTapCts.Cancel();
            _activeTapCts = null;
        }
    }
}