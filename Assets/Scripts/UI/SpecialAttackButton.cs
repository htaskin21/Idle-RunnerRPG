using System;
using System.Threading;
using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpecialAttackButton : MonoBehaviour
    {
        public GameObject lockBackground;
        public Color baseBorderColor;
        public Button buttonComponent;
        public Image buttonBackground;
        public Image outerCircleImage;
        public Image sliderImage;
        public TextMeshProUGUI timeText;
        public UIShiny iconShine;

        private void Start()
        {
            baseBorderColor = buttonBackground.color;
            outerCircleImage.color = baseBorderColor;
            buttonBackground.color = Color.grey;
        }

        public async UniTask StartDurationState(int maximumTime, CancellationTokenSource cancellationTokenSource)
        {
            buttonComponent.enabled = false;
            outerCircleImage.fillClockwise = false;

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
            outerCircleImage.fillAmount = 1 - (float) currentTime / maximumTime;
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

            outerCircleImage.fillClockwise = true;

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
            iconShine.Play();
            cancellationTokenSource.Cancel();
        }

        private void SetCoolDownState(int currentTime, int maximumTime)
        {
            var difference = (float) currentTime / maximumTime;
            var reduceDifference = 1 - difference;
            sliderImage.fillAmount = reduceDifference;
            outerCircleImage.fillAmount = difference;

            TimeSpan timeSpan = TimeSpan.FromMilliseconds(maximumTime - currentTime);
            timeText.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
        }

        private void DisableSliderImage()
        {
            sliderImage.gameObject.SetActive(false);
        }

        public void SetLockState(int id)
        {
            var dictionary = SaveLoadManager.Instance.LoadSpecialAttackUpgrade();
            var state = dictionary.ContainsKey(id);

            lockBackground.SetActive(!state);
            buttonComponent.enabled = state;
        }
    }
}