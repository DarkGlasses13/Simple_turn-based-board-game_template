using Architecture_Base.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Game.Dice_Rolling
{
    public class DicePopup : MonoBehaviour, IUIElement
    {
        public event Action OnInteracted;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _value;
        private Action _animationEndCallback;

        public void Show(Action callback = null)
        {
            _value.text = string.Empty;
            _button.interactable = true;
            gameObject.SetActive(true);
            _button.onClick.AddListener(OnClick);
            callback?.Invoke();
        }

        private void OnClick()
        {
            _button.interactable = false;
            OnInteracted?.Invoke();
        }

        public void Set(int result, Action callback)
        {
            _animationEndCallback = callback;
            _value.text = result.ToString();
            Invoke(nameof(WaitForAnimation), time: 2);
        }

        private void WaitForAnimation()
        {
            _animationEndCallback?.Invoke();
        }

        public void Hide(Action callback = null)
        {
            _button.onClick.RemoveListener(OnClick);
            gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
}
