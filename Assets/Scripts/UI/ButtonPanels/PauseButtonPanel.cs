using System;
using Audio;
using Scripts.UI.Buttons;
using UnityEngine;

namespace Scripts.UI.ButtonPanels
{
    public class PauseButtonPanel : ExclusiveButtonPanel<PauseButtonPanel.PauseButtons>
    {
        public enum PauseButtons
        {
            Null,
            Continue,
            Options,
            Credits,
            Menu,
            Quit
        }
        [SerializeField] private SoundEffectPlayer sfxPlayer; 

        protected override void OnButtonPressed(ValuedUIButton<PauseButtons> button)
        {
            var buttonPressed = button.Value;
            Debug.Log("Button pressed: " + buttonPressed);

            sfxPlayer.PlaySoundEffect(SoundEffectType.UIClick);
            
            switch (buttonPressed)
            {
                case PauseButtons.Null:
                    break;
                case PauseButtons.Continue:
                    break;
                case PauseButtons.Options:
                    break;
                case PauseButtons.Credits:
                    break;
                case PauseButtons.Menu:
                    break;
                case PauseButtons.Quit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnButtonHovered(ValuedUIButton<PauseButtons> buttonHovered)
        {
            if (sfxPlayer == null) return;
            
            sfxPlayer.PlaySoundEffect(sfx: SoundEffectType.UIClick);
        }
        
    }
}