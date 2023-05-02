using System;
using System.Collections;
using DG.Tweening;
using Enums;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class CurrencyPopUpPanel : MonoBehaviour
    {
        [SerializeField]
        private Color _coinColor;

        [SerializeField]
        private Color _gemColor;

        [SerializeField]
        private TextMeshProUGUI _popUpText;

        [SerializeField]
        private RectTransform _popUpTransform;

        private Coroutine _currentCoroutine;

        public static Action<double, LootType> OnShowCurrencyPopUpPanel;

        private void Awake()
        {
            OnShowCurrencyPopUpPanel = delegate(double d, LootType type) { };
            OnShowCurrencyPopUpPanel += ShowPopUpPanel;
        }

        private void ShowPopUpPanel(double amount, LootType lootType)
        {
            SetText(amount, lootType);
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(ShowPopUpPanelRoutine());
            }
            else
            {
                StopAllCoroutines();
                ClosePopUpPanelInstant();
                _currentCoroutine = StartCoroutine(ShowPopUpPanelRoutine());
            }
        }

        IEnumerator ShowPopUpPanelRoutine()
        {
            _popUpTransform.gameObject.SetActive(true);
            yield return _popUpTransform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InFlash).WaitForCompletion();
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(ClosePopUpPanelRoutine());
        }

        private IEnumerator ClosePopUpPanelRoutine()
        {
            yield return _popUpTransform.DOScale(new Vector3(0, 0, 0), 0.15f).SetEase(Ease.OutFlash)
                .WaitForCompletion();
            _popUpTransform.gameObject.SetActive(false);
            _currentCoroutine = null;
        }

        private void ClosePopUpPanelInstant()
        {
            _popUpTransform.localScale = Vector3.zero;
            _popUpTransform.gameObject.SetActive(false);
        }

        private void SetText(double amount, LootType lootType)
        {
            _popUpText.color = lootType == LootType.Coin ? _coinColor : _gemColor;
            string currencyIcon = lootType == LootType.Coin ? "<sprite=0>" : "<sprite=7>";

            _popUpText.text = $"+{CalcUtils.FormatNumber(amount)} {currencyIcon}";
        }

        //TODO daha sonra sil
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                ShowPopUpPanel(3, LootType.Coin);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log(_currentCoroutine == null ? "Co Boş" : _currentCoroutine.ToString());
            }
        }
    }
}