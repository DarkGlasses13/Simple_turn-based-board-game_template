using Architecture_Base.Core;
using Assets._Project.Character_Selection;
using Assets._Project.Characters;
using Finite_State_Machine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project
{
    public class GameRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable
    {
        private readonly GameConfigLoader _configLoader;
        private readonly FiniteStateMachine _stateMachine;
        private readonly StateUpdateController _stateUpdateController;
        private readonly CharactersBase _charactersBase;
        private readonly CharacterSelectionController _characterSelectionController;

        public GameRunner(GameConfigLoader configLoader, FiniteStateMachine stateMachine, List<IState> states,
            StateUpdateController stateUpdateController, CharactersBase charactersBase,
            CharacterSelectionController characterSelectionController)
        {
            _configLoader = configLoader;
            _stateMachine = stateMachine;
            _stateUpdateController = stateUpdateController;
            _charactersBase = charactersBase;
            _characterSelectionController = characterSelectionController;
            _stateMachine.AddStates(states);
        }

        public void Initialize() => RunAsync();

        protected override async Task CreateControllers()
        {
            await _configLoader.LoadAsync();
            await _charactersBase.InitAsync();

            _controllers = new IController[] 
            {
                _stateUpdateController,
                _characterSelectionController,
            };

            _controllersToEnable = new IController[] 
            {
                _stateUpdateController
            };
        }

        protected override void OnControllersInitialized()
        {
            _stateMachine.Switch<SelectCharacterState>();
        }
    }
}