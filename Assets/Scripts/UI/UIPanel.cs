using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public GameObject panelObject;
        
        public virtual void Start()
        {
            UIManager.Instance.AddPanelToUIPanels(this);
        }

        public virtual void InstantOpenPanel()
        {
            panelObject.transform.localScale = new Vector3(1, 1, 1);
            this.gameObject.SetActive(true);
        }

        public virtual void InstantClosePanel()
        {
            panelObject.transform.localScale = new Vector3(0, 0, 0);
            this.gameObject.SetActive(false);
        }

        public virtual void OpenPanel()
        {
            UIManager.Instance.CloseOtherUIPanels(this);
            this.gameObject.SetActive(true);
            panelObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.Linear);
            
            /*
             * this.gameObject.SetActive(true);
            panelObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.Linear)
                .OnComplete(() => UIManager.Instance.CloseOtherUIPanels(this));
             */
        }

        public virtual void ClosePanel()
        {
            panelObject.transform.DOScale(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.Linear)
                .OnComplete(() => this.gameObject.SetActive(false));
        }
    }
}