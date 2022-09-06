using System.Collections.Generic;
using States;
using UnityEngine;

namespace Hero
{
    public class HeroController : CharacterController
    {
        public HeroAttack heroAttack;

        public HeroUI heroUI;

        [SerializeField]
        private List<PetController> pets;

        private void Start()
        {
            pets[0].gameObject.SetActive(true);
        }

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