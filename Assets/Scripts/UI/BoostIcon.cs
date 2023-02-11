using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BoostIcon : MonoBehaviour
    {
        public Image icomImage;

        [SerializeField]
        private TextMeshProUGUI _timeText;

        private int _passingTime = 100;
        private CancellationTokenSource _cancellationTokenSource;

        private double _difference = 0;

        public async UniTask SetCoolDown(DateTime boostTime)
        {
            if (_difference > 0)
            {
                _difference = boostTime.Subtract(DateTime.UtcNow).TotalMilliseconds;
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            _difference = boostTime.Subtract(DateTime.UtcNow).TotalMilliseconds;
            SetTimeText(_difference);

            while (_difference > 0)
            {
                await UniTask.Delay(_passingTime);
                _difference -= _passingTime;
                SetTimeText(_difference);
            }

            _cancellationTokenSource.Cancel();
            gameObject.SetActive(false);
        }

        private void SetTimeText(double milliSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliSeconds);
            _timeText.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}";
        }
    }
}