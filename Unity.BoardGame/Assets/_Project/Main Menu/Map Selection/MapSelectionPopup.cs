using Assets._Project.Main_Menu.Maps;
using Assets._Project.UI_Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Assets._Project.Main_Menu.Map_Selection
{
    public class MapSelectionPopup : MonoBehaviour
    {
        public event Action<int> OnSelect;

        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _selectButton;
        private List<SelectToggle> _selectToggles = new();
        private int _selected;

        public async Task ConstructAsync(IEnumerable<MapData> datas)
        {
            foreach (MapData data in datas)
            {
                GameObject instance = await Addressables.InstantiateAsync("Map Select Toggle", _content).Task;
                SelectToggle toggle = instance.GetComponent<SelectToggle>();
                toggle.Construct(_toggleGroup, data.Name, data.Description, data.Icon);
                _selectToggles.Add(toggle);
            }
        }

        public void Show(Action callback = null)
        {
            gameObject.SetActive(true);
            _selectToggles.ForEach(toggle => toggle.OnClick += OnToggleClicked);
            _selectButton.onClick.AddListener(OnSelectClicked);
            _selectToggles[0].Select();
            callback?.Invoke();
        }

        private void OnToggleClicked(int index)
        {
            _selected = index;
        }

        private void OnSelectClicked()
        {
            OnSelect?.Invoke(_selected);
        }

        public void Hide(Action callback = null)
        {

            gameObject?.SetActive(false);
            callback?.Invoke();
        }
    }
}
