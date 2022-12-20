using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
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

        private bool _canAttack = true;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canAttack)
            {
                TapToDamage().Forget();
            }
        }

        private async UniTask TapToDamage()
        {
            _cts = new CancellationTokenSource();

            _canAttack = false;
            HeroAttack.OnTapDamage?.Invoke(heroDamageDataSo.tapAttack);

            await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown);
            _canAttack = true;

            _cts.Cancel();
        }
    }
}