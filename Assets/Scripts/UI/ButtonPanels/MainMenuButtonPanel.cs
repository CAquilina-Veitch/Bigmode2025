using System;
using Audio;
using DG.Tweening;
using Extensions;
using Scripts.UI.Buttons;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.UI.ButtonPanels
{
    public class MainMenuButtonPanel : ExclusiveButtonPanel<MainMenuButtonPanel.MainMenuButtons>
    {
        [SerializeField] private AnimatedUIImage cursor;
        [SerializeField] private RectTransform cursorImageRectTransform;
        [SerializeField] private float cursorPunchScale = 2;
        [SerializeField] private float cursorPunchDuration;
        [SerializeField] private SoundEffectPlayer sfxPlayer; 
        public enum MainMenuButtons
        {
            Null,
            Continue,
            NewGame,
            Options,
            Credits,
            Quit
        }

        protected override void OnButtonPressed(ValuedUIButton<MainMenuButtons> button)
        {
            var buttonPressed = button.Value;
            Debug.Log("Button pressed: " + buttonPressed);
            cursorImageRectTransform.localScale = Vector3.one;
            cursorImageRectTransform.DOPunchScale(Vector3.one * cursorPunchScale, cursorPunchDuration);

            var clickSound = button.Value is MainMenuButtons.NewGame or MainMenuButtons.Continue
                ? SoundEffectType.StartGame
                : SoundEffectType.UISelect;
            sfxPlayer.PlaySoundEffect(clickSound);
            
            switch (buttonPressed)
            {
                case MainMenuButtons.Continue:
                    break;
                case MainMenuButtons.NewGame:
                    break;
                case MainMenuButtons.Options:
                    break;
                case MainMenuButtons.Credits:
                    break;
                case MainMenuButtons.Quit:
                    Application.Quit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonPressed), buttonPressed, null);
            }
        }

        protected override void OnButtonHovered(ValuedUIButton<MainMenuButtons> button)
        {
            cursor.rectTransform().anchoredPosition = 
                new Vector2(cursor.rectTransform().anchoredPosition.x, button.rectTransform().anchoredPosition.y);
            
            if (sfxPlayer == null) return;
            
            sfxPlayer.PlaySoundEffect(sfx: SoundEffectType.UIClick);
        }
    }
}