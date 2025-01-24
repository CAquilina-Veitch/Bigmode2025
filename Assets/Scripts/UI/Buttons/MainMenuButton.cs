using System;
using DG.Tweening;
using R3;
using Scripts.UI.ButtonPanels;
using TMPro;
using UnityEngine;

namespace Scripts.UI.Buttons
{
    public sealed class MainMenuButton : ValuedUIButton<MainMenuButtonPanel.MainMenuButtons>
    {
        [SerializeField] private RectTransform movementTransform;
        [SerializeField] private float rightMovement = 30;
        [SerializeField] private float animationDuration = 0.2f;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private Color colorOnDeactivate = Color.grey;
        private Color initialColour;
        private Vector2 initialPos;
        private void Awake()
        {
            initialPos = movementTransform.anchoredPosition;
            initialColour = textField.color;
            IsHovered.Subscribe(OnHover);
        }

        private void OnHover(bool isHovering)
        {
            var goalPos = isHovering ? initialPos + Vector2.right * rightMovement : initialPos;
            movementTransform.DOAnchorPos(goalPos, animationDuration).SetEase(Ease.OutCubic);
        }

        public override void SetButtonActive(bool newIsActive)
        {
            base.SetButtonActive(newIsActive);
            textField.color = newIsActive ? initialColour : colorOnDeactivate;
        }
    }
}