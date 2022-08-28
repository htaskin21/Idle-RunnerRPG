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
        private float health;

        public float Health => health;

        [SerializeField]
        private Slider healthBar;

        [SerializeField]
        private CanvasGroup healthBarCanvasGroup;

        public Action OnEnemyDie;

        private void Awake()
        {
            OnEnemyDie = delegate { };
        }

        private void Start()
        {
            health = maxHealth;

            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = health;

            healthBarCanvasGroup.alpha = 1;
        }

        public void SetHealth(float attackDamage)
        {
            health -= attackDamage;
            healthBar.DOValue(health, 0.2f);

            if (health <= 0)
            {
                healthBarCanvasGroup.DOFade(0, .35f).SetEase(Ease.InCirc);
            }
        }
    }
}