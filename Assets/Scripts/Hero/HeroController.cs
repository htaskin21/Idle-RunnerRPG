using States;
using UnityEngine;

namespace Hero
{
    public class HeroController : CharacterController
    {
        public float attackPoint;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var runState = _states.Find(x => x.stateType == StateType.Run);
                TransitionToState(runState);
            }
        }

      
    }
}