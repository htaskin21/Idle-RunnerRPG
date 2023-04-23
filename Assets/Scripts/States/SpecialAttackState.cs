using Enums;
using Hero;
using UI;
using UnityEngine;

namespace States
{
    public class SpecialAttackState : State
    {
        public State runState;
        public State idleState;

        private HeroController _heroController;

        [Header("Special Attack Prefabs")]
        public GameObject explosionAttackPrefab;

        public GameObject lightningAttackPrefab;

        public GameObject iceAttackPrefab;

        public GameObject holyAttackPrefab;

        public GameObject plantAttackPrefab;

        private void Start()
        {
            _heroController = (HeroController) CharacterController;
        }

        protected override void EnterState()
        {
            ButtonController.OnActiveAttackButtons?.Invoke(false);

            CharacterController.AnimationController.PlayAnimation(AnimationType.SpecialAttack);

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                SetSpecialAttackPrefab().SetActive(true));

            CharacterController.AnimationController.onAnimationAction.AddListener(() =>
                HeroAttack.OnInflictDamage?.Invoke(
                    _heroController.heroAttack.GetSpecialAttackDamage(),
                    AttackType.SpecialAttackDamage));

            CharacterController.AnimationController.onAnimationEnd.AddListener(_heroController.DecideNextState);

            base.EnterState();
        }

        protected override void ExitState()
        {
            CharacterController.AnimationController.onAnimationAction.RemoveAllListeners();
            CharacterController.AnimationController.onAnimationEnd.RemoveAllListeners();

            base.ExitState();
        }

        private GameObject SetSpecialAttackPrefab()
        {
            GameObject specialAttack = null;

            switch (_heroController.heroAttack.specialAttackType)
            {
                case SpecialAttackType.Explosion:
                    specialAttack = explosionAttackPrefab;
                    break;
                case SpecialAttackType.Lightning:
                    specialAttack = lightningAttackPrefab;
                    break;
                case SpecialAttackType.IceAttack:
                    specialAttack = iceAttackPrefab;
                    break;
                case SpecialAttackType.Holy:
                    specialAttack = holyAttackPrefab;
                    break;

                case SpecialAttackType.PlantAttack:
                    specialAttack = plantAttackPrefab;
                    break;
            }

            var position = specialAttack.transform.position;
            position = new Vector3(_heroController.heroAttack.CurrentEnemy.specialAttackPosition.position.x,
                position.y, position.z);
            specialAttack.transform.position = position;

            return specialAttack;
        }
    }
}