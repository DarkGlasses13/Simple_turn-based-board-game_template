using Assets._Project.Game.Turn;
using Finite_State_Machine;
using UnityEngine;

namespace Assets._Project.Game
{
    public class FinishGameState : State
    {
        private readonly TurnSequence _turn;

        public FinishGameState(IStateSwitcher switcher, TurnSequence turn) : base(switcher)
        {
            _turn = turn;
        }

        public override void Enter()
        {
            Debug.Log(_turn.CurrentPlayer.Name + " is win!");
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}
