using UnityEngine;

namespace States
{
    public class WakeUpState : State
    {
        //[SerializeField] private State idleState;

        [SerializeField] private State attackState;
       
        protected override void EnterState()
        {
            characterController.AnimationController.PlayAnimation(AnimationType.WakeUp);

            //var attackState = characterController.GetState(StateType.Attack);
            characterController.AnimationController.OnAnimationEnd.AddListener(() =>
                characterController.TransitionToState(attackState));

            base.EnterState();
        }

        protected override void ExitState()
        {
            characterController.AnimationController.OnAnimationEnd.RemoveAllListeners();
            base.ExitState();
        }
    }
}