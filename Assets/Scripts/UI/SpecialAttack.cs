using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using ScriptableObjects;
using States;
using UnityEngine;

namespace UI
{
    public class SpecialAttack : MonoBehaviour
    {
        [SerializeField]
        private SpecialAttackType specialAttackType;

        [SerializeField]
        private SpecialAttackButton specialAttackButton;

        [SerializeField]
        private HeroDamageDataSO heroDamageDataSo;

        private CancellationTokenSource _cts;

        public void StartSpecialAttack()
        {
            _cts = new CancellationTokenSource();

            GameManager.Instance.HeroController.heroAttack.specialAttackType = specialAttackType;

            var specialAttackState = GameManager.Instance.HeroController.GetState(StateType.SpecialAttack);
            GameManager.Instance.HeroController.TransitionToState(specialAttackState);

            specialAttackButton.StartCoolDownState((int) heroDamageDataSo.autoTapAttackCooldown, _cts).Forget();
        }
    }
}