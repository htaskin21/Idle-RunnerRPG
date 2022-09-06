using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Hero
{
    public class HeroUI : MonoBehaviour
    {
        [SerializeField]
        private Canvas heroCanvas;

        [SerializeField]
        private CanvasGroup heroCanvasGroup;

        [SerializeField]
        private Slider attackCoolDownSlider;

        private Tweener sliderTweener;

        public void SetCoolDownSlider(float duration)
        {
            sliderTweener = attackCoolDownSlider.DOValue(1, duration / 1000).SetEase(Ease.Linear);

            heroCanvasGroup.DOFade(1, .1f).SetEase(Ease.InCirc)
                .OnComplete(() => sliderTweener.Play());
        }

        public void FadeOutSlider()
        {
            sliderTweener.Kill();

            heroCanvasGroup.DOFade(0, .1f).SetEase(Ease.InCirc)
                .OnComplete(() => attackCoolDownSlider.value = 0);
        }
    }
}