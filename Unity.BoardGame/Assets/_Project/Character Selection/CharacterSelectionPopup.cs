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
        public event Action<int, string> OnNextPlayer;
        public event Action<int, string> OnPlay;

        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _nextPlayerButton, _playButton;
        [SerializeField] private TMP_InputField _nameField;
        private List<CharacterSelectToggle> _selectToggles = new();
        private int _selectedCharacter;

        public bool NextPlayerButtonInteractable
        {
            get => _nextPlayerButton.interactable;

            set => _nextPlayerButton.interactable = value;
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
                CharacterSelectToggle button = instance.GetComponent<CharacterSelectToggle>();
                button.Construct(_toggleGroup, data);
                _selectToggles.Add(button);
                button.OnClick += OnSelect;
            }
        }

        private void OnSelect(int index)
        {
            _selectedCharacter = index;
        }

        public void Show(Action callback = null)
        {
            gameObject.SetActive(true);
            _playButton.onClick.AddListener(OnPlayClicked);
            _nextPlayerButton.onClick.AddListener(OnNextPlayerClicked);
            _selectToggles[0].Select();
            callback?.Invoke();
        }

        private void OnNextPlayerClicked()
        {
            OnNextPlayer?.Invoke(_selectedCharacter, _nameField.text);
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
            _nextPlayerButton.onClick.RemoveListener(OnNextPlayerClicked);
            gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
}
