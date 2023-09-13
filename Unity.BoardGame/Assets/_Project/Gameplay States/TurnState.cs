using Finite_State_Machine;
using UnityEngine;

namespace Assets._Project.Gameplay_States
{
    public class TurnState : State
    {
        public TurnState(IStateSwitcher switcher) : base(switcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Performe steps");
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
