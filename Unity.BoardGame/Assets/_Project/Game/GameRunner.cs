using Architecture_Base.Core;
using Assets._Project.Game.Character_Selection;
using Assets._Project.Game.Characters;
using Assets._Project.Game.Dice_Rolling;
using Finite_State_Machine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project.Game
{
    public class GameRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private readonly FiniteStateMachine _stateMachine;
        private readonly CharactersBase _charactersBase;
        private readonly CharacterSelectionController _characterSelectionController;
        private readonly DiceController _diceController;

        public GameRunner(FiniteStateMachine stateMachine, List<IState> states,
            CharactersBase charactersBase, CharacterSelectionController characterSelectionController,
            DiceController diceController)
        {
            _stateMachine = stateMachine;
            _stateMachine.AddStates(states);
            _charactersBase = charactersBase;
            _characterSelectionController = characterSelectionController;
            _diceController = diceController;
            _canEnableControllers = false;
        }

        public void Initialize() => RunAsync();

        protected override async Task CreateControllers()
        {
            await _charactersBase.LoadDataAsync();

            _controllers = new IController[]
            {
                _characterSelectionController,
                _diceController,
            };
        }

        protected override void OnControllersInitialized()
        {
            _stateMachine.Switch<SelectCharacterState>();
        }

        protected override void OnControllersEnabled()
        {

        }

        public void Dispose()
        {
            _stateMachine?.Clear();
        }
    }
}