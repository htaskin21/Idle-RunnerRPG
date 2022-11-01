using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void InstantOpenPanel()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void InstantClosePanel()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void OpenPanel()
        {
            gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.Linear)
                .OnComplete(() => this.gameObject.SetActive(true));
        }

        public virtual void ClosePanel()
        {
            gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.Linear)
                .OnComplete(() => this.gameObject.SetActive(false));
        }
    }
}