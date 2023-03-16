using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class ButtonScaler : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private float newSize;

        [SerializeField]
        private float lerpDuration;

        public void ScaleButton()
        {
            rectTransform.DOScale(new Vector3(newSize, newSize, newSize), lerpDuration)
                .OnComplete(() => rectTransform.DOScale(new Vector3(1, 1, 1), lerpDuration));
        }
    }
}