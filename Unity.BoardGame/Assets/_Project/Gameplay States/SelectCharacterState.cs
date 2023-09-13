using Finite_State_Machine;
using System;
using UnityEngine;

namespace Assets._Project.Gameplay_States
{
    public class SelectCharacterState : State
    {
        public SelectCharacterState(IStateSwitcher switcher) : base(switcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Select character");
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
