using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Hero;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyTimer : MonoBehaviour
    {
        [SerializeField]
        private EnemyHealth enemyHealth;

        [SerializeField]
        private Slider timeBar;

        [SerializeField]
        private TextMeshProUGUI timeText;

        private Tweener sliderTweener;

        private int _duration = 60;

        private CancellationTokenSource _cts;

        private void Start()
        {
            timeBar.minValue = 0;
            timeBar.maxValue = 1;
            timeBar.value = 1;

            HeroMovement.OnHeroStopRunning += SetCoolDownSlider;
        }

        public void SetDuration(int bonusTime)
        {
            _duration += bonusTime;
        }

        private void SetCoolDownSlider()
        {
            sliderTweener = timeBar.DOValue(0, _duration).SetEase(Ease.Linear).OnComplete(OnTimesUp);
            CountDownTimeText().Forget();
        }

        private void OnTimesUp()
        {
            sliderTweener.Kill();

            if (enemyHealth.Health > 0)
            {
                GameManager.Instance.LoadSameLevel();
            }
        }

        private async UniTask CountDownTimeText()
        {
            _cts = new CancellationTokenSource();

            timeText.enabled = true;

            var time = _duration - 1;
            do
            {
                timeText.text = $"{time}s";
                await UniTask.Delay(1000);
                time--;
            } while (time > 0);

            _cts.Cancel();
        }

        private void OnDestroy()
        {
            HeroMovement.OnHeroStopRunning -= SetCoolDownSlider;
            _cts?.Cancel();
        }
    }
}