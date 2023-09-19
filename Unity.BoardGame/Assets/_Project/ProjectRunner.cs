using Architecture_Base.Core;
using Architecture_Base.Scene_Switching;
using Finite_State_Machine;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets._Project
{
    public class ProjectRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable
    {
        private readonly ISceneSwitcher _sceneSwitcher;
        private readonly GameConfigLoader _configLoader;
        private readonly FiniteStateMachine _stateMachine;
        private readonly StateUpdateController _stateUpdateController;

        public ProjectRunner(ISceneSwitcher sceneSwitcher, GameConfigLoader configLoader, FiniteStateMachine stateMachine, List<IState> states,
            StateUpdateController stateUpdateController)
        {
            _configLoader = configLoader;
            _stateMachine = stateMachine;
            _stateUpdateController = stateUpdateController;
            _sceneSwitcher = sceneSwitcher;
            _stateMachine.AddStates(states);
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            RunAsync();
        }

        protected override async Task CreateControllers()
        {
            await _configLoader.LoadAsync();

            _controllers = new IController[]
            {
                _stateUpdateController,
            };
        }

        protected override void OnControllersInitialized()
        {

        }

        protected override void OnControllersEnabled()
        {
            _sceneSwitcher.ChangeAsync("Main Menu");
        }
    }
}