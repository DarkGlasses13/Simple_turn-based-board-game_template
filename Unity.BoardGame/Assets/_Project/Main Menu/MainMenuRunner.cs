using Architecture_Base.Core;
using Assets._Project.Main_Menu.Map_Selection;
using Finite_State_Machine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project.Main_Menu
{
    public class MainMenuRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private readonly FiniteStateMachine _stateMachine;
        private readonly MapSelectionController _mapSelectionController;

        public MainMenuRunner(FiniteStateMachine stateMachine, List<IState> states, MapSelectionController mapSelectionController)
        {
            _stateMachine = stateMachine;
            _stateMachine.AddStates(states);
            _mapSelectionController = mapSelectionController;
            _canEnableControllers = false;
        }

        public void Initialize() => RunAsync();

        protected override async Task CreateControllers()
        {
            _controllers = new IController[]
            {
                _mapSelectionController,
            };

            await Task.CompletedTask;
        }

        protected override void OnControllersInitialized()
        {
            _stateMachine.Switch<SelectMapState>();
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
