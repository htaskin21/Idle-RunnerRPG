using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;

        [SerializeField]
        private double health;

        public double Health => health;

        [SerializeField]
        private Slider healthBar;

        [SerializeField]
        private TextMeshProUGUI healthText;

        [SerializeField]
        private CanvasGroup healthBarCanvasGroup;

        private double floatMultiplier = 1;

        public Action OnEnemyDie;

        private void Awake()
        {
            OnEnemyDie = delegate { };
        }

        public void SetHealth(double attackDamage)
        {
            health -= attackDamage;
            healthText.text = CalcUtils.FormatNumber(health);
            healthBar.DOValue((float) (health * floatMultiplier), 0.2f);

            if (health <= 0)
            {
                healthBarCanvasGroup.DOFade(0, .35f).SetEase(Ease.InCirc);
            }
        }

        public void SetMaxHealth(int level)
        {
            health = maxHealth;
            health += health * level;

            floatMultiplier = 100 / health;

            healthBar.minValue = 0;
            healthBar.maxValue = (float) (health * floatMultiplier);
            healthBar.value = (float) (health * floatMultiplier);

            healthBarCanvasGroup.alpha = 1;

            healthText.text = CalcUtils.FormatNumber(health);
        }
    }
}