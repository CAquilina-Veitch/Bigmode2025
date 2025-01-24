using System;
using UnityEngine;

namespace Scripts.UI.ButtonPanels
{
    public class MainMenuButtonPanel : ExclusiveButtonPanel<MainMenuButtonPanel.MainMenuButtons>
    {
        public enum MainMenuButtons
        {
            Null,
            Continue,
            NewGame,
            Options,
            Credits,
            Quit
        }
        protected override void OnButtonPressed(MainMenuButtons buttonPressed)
        {
            Debug.Log("Button pressed: " + buttonPressed);
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
    }
}