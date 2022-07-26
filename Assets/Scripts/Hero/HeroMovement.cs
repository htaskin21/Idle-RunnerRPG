using System;
using DG.Tweening;
using UnityEngine;

namespace Hero
{
    public class HeroMovement : MonoBehaviour
    {
        public Action OnHeroStartRunning;

        public Action OnHeroStopRunning;

        private Tweener runningTweener = null;

        private void Awake()
        {
            OnHeroStartRunning = delegate { };
            OnHeroStopRunning = delegate { };

            OnHeroStartRunning += StartRunning;
            OnHeroStopRunning += StopRunning;
        }


        private void StartRunning()
        {
            var go = gameObject;
            runningTweener = go.transform.DOMoveX(go.transform.position.x + 1, 1).SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }

        private void StopRunning()
        {
            runningTweener.Kill();
        }
    }
}