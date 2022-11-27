using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField]
        private List<SpecialAttackButton> attackButtons;

        public static Action<bool> OnActiveAttackButtons;

        private Color _greenColor = new Color(0.2156863f, 0.5803922f, 0.4313726f, 1);
        private Color _greyColor = new Color(0.5754717f, .5754717f, .5754717f, 1);

        private void Start()
        {
            OnActiveAttackButtons = delegate(bool state) { };
            OnActiveAttackButtons += ActivateAttackButtons;
        }

        private void ActivateAttackButtons(bool state)
        {
            foreach (var attackButton in attackButtons)
            {
                attackButton.buttonComponent.interactable = state;
                attackButton.buttonBackground.DOColor(state ? _greenColor : _greyColor, 0.5f);
            }
        }
    }
}