using Architecture_Base.Core;
using Finite_State_Machine;

namespace Assets._Project
{
    public class StateUpdateController : Controller
    {
        private readonly FiniteStateMachine _stateMachine;

        public StateUpdateController(FiniteStateMachine stateMachine) 
        {
            _stateMachine = stateMachine;
        }

        public override void Tick()
        {
            _stateMachine.CurrentState?.Update();
        }
    }
}
