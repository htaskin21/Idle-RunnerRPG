using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField]
        private List<Button> attackButtons;

        public static Action<bool> OnActiveAttackButtons;

        private void Start()
        {
            OnActiveAttackButtons = delegate(bool state) { };
            OnActiveAttackButtons += ActivateAttackButtons;
        }

        private void ActivateAttackButtons(bool state)
        {
            foreach (var attackButton in attackButtons)
            {
                attackButton.enabled = state;
            }
        }
    }
}