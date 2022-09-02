using States;
using UnityEngine;

namespace Hero
{
    public class HeroController : CharacterController
    {
        public HeroAttack heroAttack;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var runState = GetState(StateType.Run);
                TransitionToState(runState);
            }
        }

        public void OnTempClickButton()
        {
            var runState = GetState(StateType.Run);
            TransitionToState(runState);
        }
    }
}