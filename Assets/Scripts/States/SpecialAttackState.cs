using System;
using Hero;
using UI;
using UnityEngine;

namespace States
{
    public class SpecialAttackState : State
    {
        public State runState;
        public State idleState;

        [Header("Special Attack Prefabs")]
        public GameObject explosionAttackPrefab;
        public GameObject lightningAttackPrefab;
        
        protected override void EnterState()
        {
            CharacterController.AnimationController.PlayAnimation(AnimationType.SpecialAttack);

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                SetSpecialAttackPrefab().SetActive(true));
            
            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(GameManager.Instance.HeroController.heroAttack.SpecialAttackPoint));

            CharacterController.AnimationController.onAnimationEnd.AddListener(DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            CharacterController.AnimationController.onAnimationAction.RemoveAllListeners();
            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();

            base.ExitState();
        }

        private void DecideNextState()
        {
            if (GameManager.Instance.HeroController.heroAttack.CurrentEnemy.enemyHealth.Health <= 0)
            {
                CharacterController.TransitionToState(runState);
            }
            else
            {
                CharacterController.TransitionToState(idleState);
            }
        }

        private GameObject SetSpecialAttackPrefab()
        {
            GameObject specialAttack = null;            
            
            switch (GameManager.Instance.HeroController.heroAttack.specialAttackType)
            {
                case SpecialAttackType.Explosion:
                    specialAttack = explosionAttackPrefab;
                    break;
                case SpecialAttackType.Lightning:
                    specialAttack = lightningAttackPrefab;
                    break;
            }

            var position = specialAttack.transform.position;
            position = new Vector3(GameManager.Instance.EnemyController.transform.position.x,
                position.y, position.z);
            specialAttack.transform.position = position;

            return specialAttack;
        }
    }
}
