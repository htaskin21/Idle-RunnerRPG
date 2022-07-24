using DG.Tweening;
using UnityEngine;

namespace States
{
    public class Run : State
    {
        private static readonly int IsRun = Animator.StringToHash("isRun");
        private Tweener _runTweener;

        public Run(GameObject enemy, Animator animator, CharacterMovement characterMovement) : base(enemy, animator, characterMovement)
        {
            name = StateType.Run;
        }

        public override void Enter()
        {
            animator.SetTrigger(IsRun);
            _runTweener = characterMovement.StartRunning();
            
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            _runTweener.Kill();
            animator.ResetTrigger(IsRun);
            nextState = new Attack(enemy, animator, characterMovement);
            
            base.Exit();
        }
    }
}