using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using ScriptableObjects;
using States;
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

            specialAttackButton.StartDurationState((int) autoTapAttackDuration, _durationCts).Forget();

            while (finishTime >= DateTime.UtcNow)
            {
                HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack);

                GameManager.Instance.HeroController.DecideNextStateAfterTapDamage(heroDamageDataSo.tapAttack);

                await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown);
                await UniTask.WaitUntil(() =>
                    GameManager.Instance.EnemyController.enemyHealth.Health > 0 &&
                    GameManager.Instance.HeroController.currentState.stateType != StateType.Run);
            }

            specialAttackButton.StartCoolDownState((int) heroDamageDataSo.autoTapAttackCooldown, _cooldownCts).Forget();
            _cts.Cancel();
        }

        public void StartAutoTap()
        {
            _cts = new CancellationTokenSource();
            AutoTapRoutine().Forget();
        }
    }
}