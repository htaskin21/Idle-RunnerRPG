using System;
using Cysharp.Threading.Tasks;
using Enums;
using Hero;

namespace SpecialAttacks
{
    public class AutoTap : DurationalSpecialAttack
    {
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

            HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack, AttackType.TapDamage);

            var finishTime = DateTime.UtcNow.AddMilliseconds(duration);
            while (finishTime >= DateTime.UtcNow || _cts.IsCancellationRequested == false)
            {
                await UniTask.WaitUntil(
                    () => _heroController.heroAttack.CurrentEnemy != null &&
                          _heroController.heroAttack.CurrentEnemy.enemyHealth.Health > 0 &&
                          _heroController.heroAttack.CurrentEnemy.TapDamageController.isTapDamageEnable,
                    cancellationToken:
                    _cts.Token);
                await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown, cancellationToken: _cts.Token);
                if (finishTime >= DateTime.UtcNow)
                {
                    HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack, AttackType.TapDamage);
                }
            }

            _cts.Cancel();
        }
    }
}