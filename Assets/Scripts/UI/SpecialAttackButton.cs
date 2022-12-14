using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hero;
using States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpecialAttackButton : MonoBehaviour
    {
        [SerializeField]
        private SpecialAttackType specialAttackType;

        public GameObject lockBackground;
        public Color baseBorderColor;
        public Button buttonComponent;
        public Image buttonBackground;
        public Image sliderImage;
        public TextMeshProUGUI timeText;

        private void Start()
        {
            baseBorderColor = buttonBackground.color;
        }

        public void StartSpecialAttack()
        {
            GameManager.Instance.HeroController.heroAttack.specialAttackType = specialAttackType;

            var specialAttackState = GameManager.Instance.HeroController.GetState(StateType.SpecialAttack);
            GameManager.Instance.HeroController.TransitionToState(specialAttackState);
        }

        public async UniTask StartDurationState(int maximumTime, CancellationTokenSource cancellationTokenSource)
        {
            buttonComponent.enabled = true;

            sliderImage.fillAmount = 0;
            sliderImage.fillClockwise = true;
            sliderImage.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);

            var baseTime = maximumTime;
            var passingTime = 100;
            var currentTime = 0;

            while (maximumTime > 0)
            {
                maximumTime -= passingTime;
                SetDurationState(currentTime, baseTime);
                await UniTask.Delay(passingTime);
                currentTime += passingTime;
            }

            cancellationTokenSource.Cancel();
        }

        private void SetDurationState(int currentTime, int maximumTime)
        {
            sliderImage.fillAmount = (float) currentTime / maximumTime;
            maximumTime -= currentTime;
            maximumTime /= 1000;

            TimeSpan timeSpan = TimeSpan.FromMilliseconds(maximumTime);
            timeText.text = $"{timeSpan.Milliseconds}";
        }

        public async UniTask StartCoolDownState(int maximumTime, CancellationTokenSource cancellationTokenSource)
        {
            buttonComponent.enabled = false;

            sliderImage.fillAmount = 1;
            sliderImage.fillClockwise = false;
            sliderImage.gameObject.SetActive(true);

            var baseTime = maximumTime;
            var passingTime = 100;
            var currentTime = 0;

            while (maximumTime > 0)
            {
                maximumTime -= passingTime;
                SetCoolDownState(currentTime, baseTime);
                await UniTask.Delay(passingTime);
                currentTime += passingTime;
            }

            DisableSliderImage();
            buttonComponent.enabled = true;
            cancellationTokenSource.Cancel();
        }

        private void SetCoolDownState(int currentTime, int maximumTime)
        {
            var difference = (float) currentTime / maximumTime;
            difference = 1 - difference;
            sliderImage.fillAmount = difference;

            TimeSpan timeSpan = TimeSpan.FromMilliseconds(maximumTime - currentTime);
            timeText.text = $"{timeSpan.Minutes}.{timeSpan.Seconds}";
        }

        private void DisableSliderImage()
        {
            sliderImage.gameObject.SetActive(false);
        }

        public void StartLockState()
        {
            lockBackground.SetActive(true);
            buttonComponent.enabled = false;
        }
    }
}