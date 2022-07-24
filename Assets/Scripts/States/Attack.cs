using UnityEngine;

namespace States
{
    public class Attack : State
    {
        private static readonly int IsAttack = Animator.StringToHash("isAttack");

        public Attack(GameObject _enemy, Animator _animator, CharacterMovement _characterMovement) : base(_enemy,
            _animator, _characterMovement)
        {
            name = StateType.Attack;
        }

        public override void Enter()
        {
            animator.SetTrigger(IsAttack);
            base.Enter();
        }
    }
}