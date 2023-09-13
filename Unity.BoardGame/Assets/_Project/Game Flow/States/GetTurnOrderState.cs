using Finite_State_Machine;
using System;
using UnityEngine;

namespace Assets._Project.Game_Flow.States
{
    public class GetTurnOrderState : State
    {
        public GetTurnOrderState(IStateSwitcher switcher) : base(switcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Get turn order");
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
