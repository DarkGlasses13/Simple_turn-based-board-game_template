using Finite_State_Machine;
using UnityEngine;

namespace Assets._Project.Turn_Sequencing
{
    public class TurnState : State
    {
        private readonly TurnSequence _turn;

        public TurnState(IStateSwitcher switcher, TurnSequence turn) : base(switcher)
        {
            _turn = turn;
        }

        public override void Enter()
        {
            Debug.Log(_turn.CurrentPlayer.Name + "'s turn");
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
