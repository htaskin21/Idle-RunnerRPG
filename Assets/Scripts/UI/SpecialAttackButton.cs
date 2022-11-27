using Hero;
using States;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpecialAttackButton : MonoBehaviour
    {
        [SerializeField]
        private SpecialAttackType specialAttackType;

        public Button buttonComponent;
        
        public Image buttonBackground;

        public void StartSpecialAttack()
        {
            GameManager.Instance.HeroController.heroAttack.specialAttackType = specialAttackType;
            
            var specialAttackState = GameManager.Instance.HeroController.GetState(StateType.SpecialAttack);
            GameManager.Instance.HeroController.TransitionToState(specialAttackState);
        }
    }
}