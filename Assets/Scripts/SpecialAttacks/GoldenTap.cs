using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Hero;
using Items;

namespace SpecialAttacks
{
    public class GoldenTap : DurationalSpecialAttack
    {
        protected override async UniTask DurationalSpecialAttackRoutine(double duration)
        {
            base.DurationalSpecialAttackRoutine(duration).Forget();

            var baseSpecialAttackCoolDown = heroDamageDataSo.GetCoolDownBySpecialAttackType(_specialAttackType);
            var baseSpecialAttackDuration = heroDamageDataSo.GetDurationBySpecialAttackType(_specialAttackType);

            StartTimerUI((int) duration, baseSpecialAttackCoolDown, baseSpecialAttackDuration, _durationCts,
                _cooldownCts).Forget();

            HeroAttack.OnTapDamage += ActivateGoldenTap;

            while (duration > 0 && _cts.IsCancellationRequested == false)
            {
                await UniTask.Delay(100, cancellationToken: _cts.Token);
                duration -= 100;
            }

            HeroAttack.OnTapDamage -= ActivateGoldenTap;

            _cts.Cancel();
        }

        private void ActivateGoldenTap(double tapAttackAmount, AttackType attackType)
        {
            if (_heroController.heroAttack.CurrentEnemy == null) return;
            CancellationTokenSource cts = new CancellationTokenSource();
            var go = ObjectPool.Instance.GetGameObject(LootType.SingleCoin.ToString());
            var lootObject = go.GetComponent<LootObject>();

            var enemy = _heroController.heroAttack.CurrentEnemy;
            lootObject.SetInitialPosition(enemy.transform, enemy.enemyLoot.LootAmount * 0.05);
            lootObject.MoveLoot(cts).Forget();
        }
    }
}