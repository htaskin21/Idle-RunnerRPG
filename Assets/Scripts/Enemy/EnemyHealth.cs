using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;
        public float MaxHealth => maxHealth;

        [SerializeField]
        private double health;
        public double Health => health;

        [SerializeField]
        private Slider healthBar;

        [SerializeField]
        private CanvasGroup healthBarCanvasGroup;

        private double floatMultiplier = 1;
        
        public Action OnEnemyDie;

        private void Awake()
        {
            OnEnemyDie = delegate { };
        }

        private void Start()
        {
            health = maxHealth;

            floatMultiplier = 100 / maxHealth;
            
            healthBar.minValue = 0;
            healthBar.maxValue = (float) (maxHealth * floatMultiplier);
            healthBar.value = (float) (health * floatMultiplier);

            healthBarCanvasGroup.alpha = 1;
        }

        public void SetHealth(double attackDamage)
        {
            health -= attackDamage;
            healthBar.DOValue((float) (health * floatMultiplier), 0.2f);

            if (health <= 0)
            {
                healthBarCanvasGroup.DOFade(0, .35f).SetEase(Ease.InCirc);
            }
        }
    }
}