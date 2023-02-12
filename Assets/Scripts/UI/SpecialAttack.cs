using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Managers;
using ScriptableObjects;
using SpecialAttacks;
using States;
using UnityEngine;

namespace UI
{
    public class SpecialAttack : MonoBehaviour
    {
        [SerializeField]
        private int identifier;

        [SerializeField]
        private SpecialAttackType specialAttackType;

        [SerializeField]
        private SpecialAttackButton specialAttackButton;

        [SerializeField]
        private HeroDamageDataSO heroDamageDataSo;

        private CancellationTokenSource _cts;

        private void Start()
        {
            var isUnlocked = specialAttackButton.SetLockState(identifier);
            if (isUnlocked)
            {
            }
            else
            {
                SpecialAttackUIRow.OnUpdateSpecialAttack += CheckLockState;
            }
        }

        public void StartSpecialAttack()
        {
            _cts = new CancellationTokenSource();

            GameManager.Instance.HeroController.heroAttack.specialAttackType = specialAttackType;

            var specialAttackState = GameManager.Instance.HeroController.GetState(StateType.SpecialAttack);
            GameManager.Instance.HeroController.TransitionToState(specialAttackState);

            specialAttackButton.StartCoolDownState(heroDamageDataSo.specialAttackCoolDown,
                heroDamageDataSo.specialAttackCoolDown, _cts).Forget();
        }

        private void CheckLockState(int id)
        {
            if (id == identifier)
            {
                specialAttackButton.SetLockState(identifier);
            }
        }
    }
}