using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Managers;
using States;
using UI.SpecialAttack;
using UnityEngine;

namespace SpecialAttacks
{
    public class SpecialAttack : BaseSpecialAttack
    {
        [SerializeField]
        private SpecialAttackType _specialAttackType;

        private CancellationTokenSource _cts;

        private void Start()
        {
            var isUnlocked = specialAttackButton.SetLockState(identifier);
            if (isUnlocked)
            {
                LoadCoolDownState(heroDamageDataSo.specialAttackCoolDown, _cts);
            }
            else
            {
                SpecialAttackUIRow.OnUpdateSpecialAttack += CheckLockState;
            }
        }

        public void StartSpecialAttack()
        {
            _cts = new CancellationTokenSource();

            GameManager.Instance.HeroController.heroAttack.specialAttackType = _specialAttackType;

            var specialAttackState = GameManager.Instance.HeroController.GetState(StateType.SpecialAttack);
            GameManager.Instance.HeroController.TransitionToState(specialAttackState);

            SaveLoadManager.Instance.SaveSpecialAttackCoolDown(identifier,
                DateTime.UtcNow.AddMilliseconds(heroDamageDataSo.specialAttackCoolDown));
            
            specialAttackButton.StartCoolDownState(heroDamageDataSo.specialAttackCoolDown,
                heroDamageDataSo.specialAttackCoolDown, _cts).Forget();
        }
    }
}