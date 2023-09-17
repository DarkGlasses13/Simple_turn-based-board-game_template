using Architecture_Base.Core;
using Assets._Project.Characters;
using Assets._Project.Turn_Sequencing;
using Finite_State_Machine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets._Project.Character_Selection
{
    public class CharacterSelectionController : Controller
    {
        private readonly Transform _hudContainer;
        private readonly CharacterSelectionPopupLoader _popupLoader;
        private readonly CharactersBase _charactersBase;
        private readonly Turn _turn;
        private readonly IStateSwitcher _stateSwitcher;
        private CharacterSelectionPopup _popup;

        public CharacterSelectionController([Inject(Id = "Popup")]Transform hudContainer,
            CharacterSelectionPopupLoader popupLoader, CharactersBase charactersBase,
            Turn turn, IStateSwitcher stateSwitcher)
        {
            _hudContainer = hudContainer;
            _popupLoader = popupLoader;
            _charactersBase = charactersBase;
            _turn = turn;
            _stateSwitcher = stateSwitcher;
        }

        public override async Task InitializeAsync()
        {
            _popup = await _popupLoader.LoadAndInstantiateAsync(_hudContainer, false);
            await _popup.ConstructAsync(_charactersBase.Datas);
        }

        protected override void OnEnable()
        {
            _popup.Show();
            UpdateButtonsStates();
            _popup.OnNextPlayer += OnNextPlayer;
            _popup.OnPlay += OnPlay;
        }

        private void OnNextPlayer(int index, string name) => RegistNewPlayer(index, name);

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
            _popup.NextPlayerButtonInteractable = _turn.CanRegistNewPlayers;
            _popup.PlayButtonInteractable = _turn.PlayersCount > 1;
        }

        protected override void OnDisable()
        {
            _popup.OnNextPlayer -= OnNextPlayer;
            _popup.OnPlay -= OnPlay;
            _popup.Hide();
        }
    }
}
