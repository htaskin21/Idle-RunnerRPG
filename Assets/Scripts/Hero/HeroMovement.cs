using System;
using DG.Tweening;
using UnityEngine;

namespace Hero
{
    public class HeroMovement : MonoBehaviour
    {
        public static Action OnHeroStartRunning;

        public static Action OnHeroStopRunning;

        private Tweener _runningTweener = null;

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
            _runningTweener = go.transform.DOMoveX(go.transform.position.x + 1, .75f).SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }

        private void StopRunning()
        {
            _runningTweener.Kill();
        }
    }
}