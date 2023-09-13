using Architecture_Base.Core;
using Assets._Project.Game_Flow.States;
using Finite_State_Machine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project
{
    public class GameRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable
    {
        private readonly FiniteStateMachine _stateMachine;
        private readonly StateUpdateController _stateUpdateController;

        public GameRunner(FiniteStateMachine stateMachine, List<IState> states, StateUpdateController stateUpdateController)
        {
            _stateMachine = stateMachine;
            _stateUpdateController = stateUpdateController;
            _stateMachine.AddStates(states);
        }

        public void Initialize() => RunAsync();

        protected override Task CreateControllers()
        {
            _controllers = new IController[] 
            {
                _stateUpdateController,
            };

            return Task.CompletedTask;
        }

        protected override void OnControllersInitializedAndEnabled()
        {
            _stateMachine.Switch<SelectCharacterState>();
        }
    }
}