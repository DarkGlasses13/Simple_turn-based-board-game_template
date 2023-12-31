﻿using Architecture_Base.Core;
using Architecture_Base.Hierarchy_Building;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Game.Dice_Rolling
{
    public class DiceController : Controller
    {
        public event Action<int> OnRolled;

        private readonly FastRandom _random;
        private readonly GameConfigLoader _configLoader;
        private readonly DicePopupLoader _dicePopupLoader;
        private readonly Transform _popupContainer;
        private GameConfig _config;
        private DicePopup _popup;

        public int Result { get; private set; }

        public DiceController(FastRandom random, GameConfigLoader configLoader,
            DicePopupLoader dicePopupLoader, HierarchyBuilder hierarchyBuilder)
        {
            _random = random;
            _configLoader = configLoader;
            _dicePopupLoader = dicePopupLoader;
            _popupContainer = hierarchyBuilder.GetParent("Popups");
        }

        public override async Task InitializeAsync()
        {
            _config = await _configLoader.LoadAsync();
            _popup = await _dicePopupLoader.LoadAndInstantiateAsync(_popupContainer, isActive: false);
        }

        protected override void OnEnable()
        {
            _popup.Show();
            _popup.OnInteracted += OnInteracted;
        }

        private void OnInteracted()
        {
            _popup.OnInteracted -= OnInteracted;
            _popup.Set(Roll(), OnAnimationEnded);
        }

        private void OnAnimationEnded()
        {
            OnRolled?.Invoke(Result);
        }

        public int Roll()
        {
            Result = _random.Range(1, _config.DiceMaxValue);
            return Result;
        }

        protected override void OnDisable()
        {
            _popup.Hide();
        }
    }
}
