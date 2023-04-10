using System.Collections.Generic;
using DG.Tweening;
using Enums;
using JetBrains.Annotations;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StageProgressBar : MonoBehaviour
    {
        [SerializeField]
        private List<RectTransform> _fillImages;

        [SerializeField]
        private Image _bossTypeIcon;

        [SerializeField]
        private TextMeshProUGUI _stageProgressText;

        [SerializeField]
        [CanBeNull]
        private DamageIconDataSO _damageIconDataSo;


        public void InitialProgressBar(int level, DamageType enemyDamageType)
        {
            _stageProgressText.text = $"STAGE {level}";
            _bossTypeIcon.sprite = _damageIconDataSo.GetIcon(enemyDamageType);
            _fillImages.ForEach(x => x.gameObject.SetActive(false));
        }

        public void IncreaseProgressBar(int enemyKillCount)
        {
            _fillImages[enemyKillCount - 1].localScale = new Vector3(0f, 0f, 0f);
            _fillImages[enemyKillCount - 1].gameObject.SetActive(true);

            _fillImages[enemyKillCount - 1].DOScale(new Vector3(1.3f, 1.3f, 1.3f), .2f).OnComplete(() =>
                _fillImages[enemyKillCount - 1].DOScale(new Vector3(1f, 1f, 1f), .1f));
        }
    }
}