using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Items.Potion;
using Managers;
using States;
using UI.SpecialAttack;

namespace SpecialAttacks
{
    public class SpecialAttack : BaseSpecialAttack
    {
        private void Start()
        {
            var isUnlocked = specialAttackButton.SetLockState(identifier);
            if (isUnlocked)
            {
                LoadCoolDownState(_cts);
            }
            else
            {
                SpecialAttackUIRow.OnUpdateSpecialAttack += CheckLockState;
            }

            RefreshPotion.OnRefreshAllSpecialAttack += RefreshSpecialAttack;
        }

        public void StartSpecialAttack()
        {
            _cts = new CancellationTokenSource();

            _heroController.heroAttack.specialAttackType = _specialAttackType;

            var specialAttackState = GameManager.Instance.HeroController.GetState(StateType.SpecialAttack);
            GameManager.Instance.HeroController.TransitionToState(specialAttackState);

            var coolDown = heroDamageDataSo.GetCoolDownBySpecialAttackType(_specialAttackType);

            SaveLoadManager.Instance.SaveSpecialAttackCoolDown(identifier,
                DateTime.UtcNow.AddMilliseconds(coolDown));

            specialAttackButton.StartCoolDownState(coolDown,
                coolDown, _cts).Forget();
        }

        private void RefreshSpecialAttack()
        {
            if (specialAttackButton.SpecialAttackButtonState == SpecialAttackButtonState.OnCoolDown)
            {
                SaveLoadManager.Instance.SaveSpecialAttackCoolDown(identifier,
                    DateTime.UtcNow);

                _cts?.Cancel();
            }
        }
    }
}