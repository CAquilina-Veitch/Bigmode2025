using Extensions;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public enum ButtonState
    {
        Default,
        Hovered,
        Pressed,
        Disabled
    }
    public class UIButton : ObservableMouseHover
    {
        [SerializeField] private Button button;
        [SerializeField] private bool isInitiallyActive = true;
        
        public ReadOnlyReactiveProperty<ButtonState> CurrentButtonState => currentButtonState;
        private readonly ReactiveProperty<ButtonState> currentButtonState = new();
        
        private void Start()
        {
            SetButtonActive(isInitiallyActive);

            IsHovered.Where(_ => CurrentButtonState.CurrentValue != ButtonState.Disabled).Subscribe(x =>
                currentButtonState.Value = x ? ButtonState.Hovered : ButtonState.Default);

            button.OnClickAsObservable().Where(_ => CurrentButtonState.CurrentValue is not ButtonState.Disabled)
                .Subscribe(_ => currentButtonState.Value = ButtonState.Pressed);
        }
        
        public virtual void SetButtonActive(bool newIsActive)
        {
            currentButtonState.Value = newIsActive ? ButtonState.Default : ButtonState.Disabled;
            button.interactable = newIsActive;
            EnableHoverDetection(newIsActive);
        }
    }
}