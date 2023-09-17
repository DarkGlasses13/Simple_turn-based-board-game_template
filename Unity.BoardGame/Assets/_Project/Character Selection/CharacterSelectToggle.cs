using Assets._Project.Characters;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Character_Selection
{
    public class CharacterSelectToggle : MonoBehaviour
    {
        public event Action<int> OnClick;

        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _image;

        public bool Interactable
        {
            get => _toggle.interactable;

            set
            {
                _toggle.interactable = false;
                _image.color = value ? Color.white : Color.grey;
            }
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        public void Construct(ToggleGroup toggleGroup, CharacterData data)
        {
            _toggle.group = toggleGroup;
            _image.sprite = data.Icon;
        }

        private void OnValueChanged(bool isOn)
        {
            if (isOn)
                OnClick?.Invoke(transform.GetSiblingIndex());
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void Select() => _toggle.isOn = true;
    }
}
