using UnityEngine;
using UnityEngine.Events;

namespace States
{
    public abstract class State : MonoBehaviour
    {
        public StateType stateType;

        protected CharacterController CharacterController;

        public UnityEvent onEnter, onExit;

        public void InitializeState(CharacterController _characterController)
        {
            CharacterController = _characterController;
        }

        public void Enter()
        {
            onEnter?.Invoke();
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
            onExit?.Invoke();
            ExitState();
        }

        protected virtual void ExitState()
        {
        }
    }

    public enum StateType
    {
        Idle,
        WakeUp,
        Run,
        Attack,
        SpecialAttack,
        Hit,
        Die
    }
}