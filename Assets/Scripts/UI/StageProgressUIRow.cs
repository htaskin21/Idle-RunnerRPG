using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StageProgressUIRow : MonoBehaviour
    {
        [SerializeField]
        protected TextMeshProUGUI _minLevelText;

        [SerializeField]
        protected TextMeshProUGUI _maxLevelText;

        [SerializeField]
        private Slider _slider;

        [Header("Button")]
        [SerializeField]
        private Button _prestigeButton;

        [SerializeField]
        private Image _prestigeButtonImage;

        [SerializeField]
        private Sprite _activeButtonSprite;

        [SerializeField]
        private Sprite _deActiveButtonSprite;

        private StageProgress _stageProgress;
        private (int minLevel, int maxLevel) _minMaxLevels;

        private void Start()
        {
            _stageProgress = new StageProgress();
            InitialSetRow();

            StageManager.OnPassStage += FillUIRow;
        }

        private void InitialSetRow()
        {
            var stageCount = SaveLoadManager.Instance.LoadStageProgress();
            var prestigeCount = SaveLoadManager.Instance.LoadPrestigeCount();
            _minMaxLevels = _stageProgress.CalculatePrestigeLevels(prestigeCount);

            FillUIRow(stageCount);
        }

        private void FillUIRow(int stageCount)
        {
            SetSlider();
            SetButtonState(stageCount);
        }

        private void SetButtonState(int maxLevel)
        {
            if (maxLevel >= _minMaxLevels.maxLevel)
            {
                _prestigeButton.enabled = true;
                _prestigeButtonImage.sprite = _activeButtonSprite;
            }
            else
            {
                _prestigeButton.enabled = false;
                _prestigeButtonImage.sprite = _deActiveButtonSprite;
            }
        }

        private void SetSlider()
        {
            _minLevelText.text = _minMaxLevels.minLevel.ToString();
            _maxLevelText.text = _minMaxLevels.maxLevel.ToString();

            _slider.minValue = _minMaxLevels.minLevel;
            _slider.maxValue = _minMaxLevels.maxLevel;

            var stageCount = SaveLoadManager.Instance.LoadStageProgress();
            if (stageCount <= _minMaxLevels.maxLevel)
            {
                _slider.value = stageCount;
            }
            else
            {
                _slider.value = _minMaxLevels.maxLevel;
            }
        }

        public void OnPrestige()
        {
            SaveLoadManager.Instance.SavePrestigeCount();

            InitialSetRow();
        }
    }
}