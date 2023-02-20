using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Managers;
using ScriptableObjects;
using UI.SpecialAttack;
using UnityEngine;

namespace SpecialAttacks
{
    public abstract class BaseSpecialAttack : MonoBehaviour
    {
        public int identifier;

        public HeroDamageDataSO heroDamageDataSo;

        public SpecialAttackButton specialAttackButton;
        
        protected CancellationTokenSource _cts;

        protected void CheckLockState(int id)
        {
            if (id == identifier)
            {
                specialAttackButton.SetLockState(identifier);
            }
        }

        protected void LoadCoolDownState(int coolDownDuration, CancellationTokenSource cooldownCts)
        {
            var durations = SaveLoadManager.Instance.LoadSpecialAttackCoolDown();
            var isContain = durations.ContainsKey(identifier);
            if (isContain)
            {
                var durationDate = durations[identifier];
                if (durationDate > DateTime.UtcNow)
                {
                    cooldownCts = new CancellationTokenSource();
                    var duration = durationDate.Subtract(DateTime.UtcNow).TotalMilliseconds;
                    specialAttackButton.StartCoolDownState((int) duration,
                        (int) heroDamageDataSo.autoTapAttackCooldown, cooldownCts).Forget();
                }
            }
        }
    }
}