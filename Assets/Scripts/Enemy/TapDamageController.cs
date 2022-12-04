using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
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

        public async UniTask TapToDamage()
        {
            _cts = new CancellationTokenSource();

            _canAttack = false;
            HeroAttack.OnInflictDamage?.Invoke(heroDamageDataSo.tapAttack);
            await UniTask.Delay(heroDamageDataSo.tapAttackCoolDown);
            _canAttack = true;

            _cts.Cancel();
        }

        public double GetAutoTapAttackDuration()
        {
            return heroDamageDataSo.autoTapAttackDuration;
        }
        
        public int GetAutoTapAttackCoolDown()
        {
            return heroDamageDataSo.tapAttackCoolDown;
        }
        
        
    }
}