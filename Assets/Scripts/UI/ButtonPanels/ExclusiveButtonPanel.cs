using System;
using System.Collections.Generic;
using Extensions;
using R3;
using Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.UI.ButtonPanels
{
    public abstract class ExclusiveButtonPanel<T> : MonoBehaviour
    {
        [SerializeField] private List<ValuedUIButton<T>> buttonList;
        [SerializeField] private int defaultHoveredValue = -1;

        private void Awake()
        {
            foreach (var button in buttonList)
            {
                button.CurrentButtonState.Where(x => x is ButtonState.Pressed)
                    .Subscribe(_ => OnButtonPressed(button)).AddTo(this);

                button.CurrentButtonState.Where(x => x is ButtonState.Hovered)
                    .Subscribe(_ => OnButtonHovered(button)).AddTo(this);
            }

            
        }

        private void OnEnable()
        {
            if (defaultHoveredValue >= 0 && defaultHoveredValue < buttonList.Count)
                OnButtonHovered(buttonList[defaultHoveredValue]);
        }

        protected abstract void OnButtonPressed(ValuedUIButton<T> buttonPressed);
        protected abstract void OnButtonHovered(ValuedUIButton<T> buttonHovered);
    }
} 