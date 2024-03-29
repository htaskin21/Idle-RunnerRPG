using System.Threading;
using Cysharp.Threading.Tasks;
using Enums;
using Hero;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Enemy
{
    public class TapDamageController : MonoBehaviour, IPointerDownHandler
    {
        private CancellationTokenSource _cts;

        [SerializeField]
        private HeroDamageDataSO heroDamageDataSo;

        public bool isTapDamageEnable;
        private bool _canAttack = true;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canAttack && isTapDamageEnable)
            {
                TapToDamage().Forget();
            }
        }

        private async UniTask TapToDamage()
        {
            _cts = new CancellationTokenSource();

            _canAttack = false;
            var tapAttack = StageManager.Instance.HeroController.heroAttack.CalculateTapDamage();
            HeroAttack.OnTapDamage?.Invoke(tapAttack, AttackType.TapDamage);

            await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown);
            _canAttack = true;

            _cts.Cancel();
        }
    }
}