using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Enemy
{
    public class TapDamageController : MonoBehaviour, IPointerDownHandler
    {
        private CancellationTokenSource cts;

        [SerializeField] private float tapDamage = 20;

        private bool _canAttack = true;

        private int tapCoolDown = 500;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canAttack)
            {
                TapToDamage().Forget();
            }
        }

        private async UniTask TapToDamage()
        {
            cts = new CancellationTokenSource();

            _canAttack = false;
            HeroAttack.OnInflictDamage?.Invoke(tapDamage);
            await UniTask.Delay(tapCoolDown);
            _canAttack = true;

            cts.Cancel();
            Debug.Log("hala calisiyor mu ?");
        }
    }
}