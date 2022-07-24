using UnityEngine;

namespace States
{
    public class State : MonoBehaviour
    {
        public StateType name;
        protected StateStep step;
        protected GameObject enemy;
        protected Animator animator;
        protected CharacterMovement characterMovement;
        protected State nextState;

        public State(GameObject _enemy, Animator _animator, CharacterMovement _characterMovement)
        {
            step = StateStep.Enter;
            this.enemy = _enemy;
            animator = _animator;
            this.characterMovement = _characterMovement;
        }

        public virtual void Enter()
        {
            step = StateStep.Update;
        }

        public virtual void Update()
        {
            step = StateStep.Update;
        }

        public virtual void Exit()
        {
            step = StateStep.Exit;
        }

        public State Process()
        {
            if (step == StateStep.Enter)
            {
                Enter();
            }

            if (step == StateStep.Update)
            {
                Update();
            }

            if (step == StateStep.Exit)
            {
                Exit();
                return nextState;
            }

            return this;
        }
    }

    public enum StateType
    {
        Idle,
        Run,
        Attack,
        SpecialAttack,
        Hurt,
        Death
    }

    public enum StateStep
    {
        Enter,
        Update,
        Exit
    }
}