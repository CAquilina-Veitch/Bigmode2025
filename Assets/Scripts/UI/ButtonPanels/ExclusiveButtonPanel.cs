using System;
using System.Collections.Generic;
using R3;
using Scripts.UI.Buttons;
using UnityEngine;

namespace Scripts.UI.ButtonPanels
{
    public abstract class ExclusiveButtonPanel<T> : MonoBehaviour
    {
        [SerializeField] private List<ValuedUIButton<T>> buttonList;

        private void Awake()
        {
            foreach (var button in buttonList)
            {
                button.CurrentButtonState.Where(x => x is ButtonState.Pressed)
                    .Subscribe(_ => OnButtonPressed(button.Value));
            }
        }

        protected abstract void OnButtonPressed(T buttonPressed);
    }
} 