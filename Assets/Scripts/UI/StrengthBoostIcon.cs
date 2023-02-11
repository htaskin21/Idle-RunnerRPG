using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StrengthBoostIcon : MonoBehaviour
    {
        [SerializeField]
        private GameObject _boostIcon;
        
        [SerializeField]
        private TextMeshProUGUI _timeText;

        private int _passingTime = 100;
        private CancellationTokenSource _cancellationTokenSource;

        void Start()
        {
            var boostFinishTime = SaveLoadManager.Instance.LoadStrengthBoostTime();
            if (boostFinishTime > DateTime.UtcNow)
            {
                SetCoolDown(boostFinishTime).Forget();
            }
        }

        public async UniTask SetCoolDown(DateTime boostTime)
        {
            _boostIcon.SetActive(true);
            _cancellationTokenSource = new CancellationTokenSource();

            var difference = boostTime.Subtract(DateTime.UtcNow).TotalMilliseconds;
            SetTimeText(difference);

            while (difference > 0)
            {
                await UniTask.Delay(_passingTime);
                difference -= _passingTime;
                SetTimeText(difference);
            }

            _cancellationTokenSource.Cancel();
            _boostIcon.SetActive(false);
        }

        private void SetTimeText(double milliSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliSeconds);
            _timeText.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
        }
    }
}