using Architecture_Base.UI;
using Assets._Project.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Assets._Project.Character_Selection
{
    public class CharacterSelectionPopup : MonoBehaviour, IUIElement
    {
        public event Action<int, string> OnSelect;
        public event Action<int, string> OnPlay;

        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _selectButton, _playButton;
        [SerializeField] private TMP_InputField _nameField;
        private List<CharacterSelectToggle> _selectToggles = new();
        private int _selectedCharacter;

        public bool SelectButtonInteractable
        {
            get => _selectButton.interactable;

            set => _selectButton.interactable = value;
        }

        public bool PlayButtonInteractable 
        {
            get => _playButton.interactable;

            set => _playButton.interactable = value;
        }

        public async Task ConstructAsync(IEnumerable<CharacterData> datas)
        {
            foreach (CharacterData data in datas)
            {
                GameObject instance = await Addressables.InstantiateAsync("Character Select Toggle", _content).Task;
                CharacterSelectToggle toggle = instance.GetComponent<CharacterSelectToggle>();
                toggle.Construct(_toggleGroup, data);
                _selectToggles.Add(toggle);
            }
        }

        private void Select(int index)
        {
            _selectedCharacter = index;
        }

        public void Show(Action callback = null)
        {
            gameObject.SetActive(true);
            _playButton.onClick.AddListener(OnPlayClicked);
            _selectButton.onClick.AddListener(OnNextPlayerClicked);
            _selectToggles.ForEach(toggle => toggle.OnClick += Select);
            _selectToggles[0].Select();
            callback?.Invoke();
        }

        private void OnNextPlayerClicked()
        {
            OnSelect?.Invoke(_selectedCharacter, _nameField.text);
            _nameField.text = string.Empty;
            _selectToggles[_selectedCharacter].Interactable = false;
            _selectToggles.First(toggle => toggle.Interactable == true).Select();
        }

        private void OnPlayClicked()
        {
            OnPlay?.Invoke(_selectedCharacter, _nameField.text);
        }

        public void Hide(Action callback = null)
        {
            _playButton.onClick.RemoveListener(OnPlayClicked);
            _selectButton.onClick.RemoveListener(OnNextPlayerClicked);
            _selectToggles.ForEach(toggle => toggle.OnClick -= Select);
            gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
}
