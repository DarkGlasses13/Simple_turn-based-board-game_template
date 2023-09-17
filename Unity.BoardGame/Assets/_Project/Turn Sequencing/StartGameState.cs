using Finite_State_Machine;
using UnityEngine;

namespace Assets._Project.Turn_Sequencing
{
    public class StartGameState : State
    {
        private readonly Turn _turn;

        public StartGameState(IStateSwitcher switcher, Turn turn) : base(switcher)
        {
            _turn = turn;
        }

        public override void Enter()
        {
            Debug.Log("Start game");
            _switcher.Switch<TurnState>();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}
