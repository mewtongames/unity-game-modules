using System;
using UnityEngine;
using UnityEngine.UI;

namespace MewtonGames.UI.Components
{
    public class Toggle : MonoBehaviour
    {
        public event Action<bool> changed;
        public bool isEnabled { get; private set; }
        public bool isInteractable { get; private set; } = true;
        
        [SerializeField] private Button _button;

        [SerializeField] private RectTransform _knob;
        [SerializeField] private Vector2 _enabledKnobPosition;
        [SerializeField] private Vector2 _disabledKnobPosition;

        [SerializeField] private Image _background;
        [SerializeField] private Color _enabledBackgroundColor;
        [SerializeField] private Color _disabledBackgroundColor;


        public void SetInteractable(bool value)
        {
            _button.interactable = value;
        }

        public void SetEnabled(bool value)
        {
            if (isEnabled == value)
            {
                return;
            }
            
            isEnabled = !isEnabled;
            changed?.Invoke(isEnabled);
            UpdateView();
        }
        
        
        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
            UpdateView();
        }

        private void OnButtonClicked()
        {
            isEnabled = !isEnabled;
            changed?.Invoke(isEnabled);
            UpdateView();
        }

        private void UpdateView()
        {
            _knob.anchoredPosition = isEnabled ? _enabledKnobPosition : _disabledKnobPosition;
            _background.color = isEnabled ? _enabledBackgroundColor : _disabledBackgroundColor;
        }
    }
}