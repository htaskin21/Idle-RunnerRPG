using UnityEngine;
using UnityEngine.Events;

namespace States
{
    public abstract class State : MonoBehaviour
    {
        public StateType stateType;

        protected CharacterController characterController;

        public UnityEvent OnEnter, OnExit;

        public void InitializeState(CharacterController _characterController)
        {
            characterController = _characterController;
        }

        public void Enter()
        {
            OnEnter?.Invoke();
            EnterState();
        }

        protected virtual void EnterState()
        {
        }

        public virtual void StateUpdate()
        {
        }

        public virtual void StateFixedUpdate()
        {
        }

        public void Exit()
        {
            OnExit?.Invoke();
            ExitState();
        }

        protected virtual void ExitState()
        {
        }
    }

    public enum StateType
    {
        Idle,
        Run,
        Attack,
        SpecialAttack,
        Hit,
        Die
    }
}