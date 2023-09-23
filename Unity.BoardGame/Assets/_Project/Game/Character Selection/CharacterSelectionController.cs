using Architecture_Base.Core;
using Architecture_Base.Hierarchy_Building;
using Assets._Project.Game.Characters;
using Assets._Project.Game.Turn;
using Finite_State_Machine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Game.Character_Selection
{
    public class CharacterSelectionController : Controller
    {
        private readonly Transform _popupContainer;
        private readonly CharacterSelectionPopupLoader _popupLoader;
        private readonly CharactersBase _charactersBase;
        private readonly TurnSequence _turn;
        private readonly IStateSwitcher _stateSwitcher;
        private CharacterSelectionPopup _popup;

        public CharacterSelectionController(HierarchyBuilder hierarchyBuilder,
            CharacterSelectionPopupLoader popupLoader, CharactersBase charactersBase,
            TurnSequence turn, IStateSwitcher stateSwitcher)
        {
            _popupContainer = hierarchyBuilder.GetParent("Popups");
            _popupLoader = popupLoader;
            _charactersBase = charactersBase;
            _turn = turn;
            _stateSwitcher = stateSwitcher;
        }

        public override async Task InitializeAsync()
        {
            _popup = await _popupLoader.LoadAndInstantiateAsync(_popupContainer, false);
            await _popup.ConstructAsync(_charactersBase.Datas);
        }

        protected override void OnEnable()
        {
            UpdateButtonsStates();
            _popup.Show();
            _popup.OnSelect += OnSelect;
            _popup.OnPlay += OnPlay;
        }

        private void OnSelect(int index, string name) => RegistNewPlayer(index, name);

        private void OnPlay(int index, string name)
        {
            RegistNewPlayer(index, name);
            _stateSwitcher.Switch<StartGameState>();
        }

        private void RegistNewPlayer(int index, string name)
        {
            CharacterData characterData = _charactersBase.Datas.ElementAt(index);
            _turn.TryRegistNewPlayer(name, characterData.ID);
            UpdateButtonsStates();
        }

        private void UpdateButtonsStates()
        {
            _popup.SelectButtonInteractable = _turn.CanRegistNewPlayers;
            _popup.PlayButtonInteractable = _turn.PlayersCount > 1;
        }

        protected override void OnDisable()
        {
            _popup.OnSelect -= OnSelect;
            _popup.OnPlay -= OnPlay;
            _popup.Hide();
        }
    }
}
