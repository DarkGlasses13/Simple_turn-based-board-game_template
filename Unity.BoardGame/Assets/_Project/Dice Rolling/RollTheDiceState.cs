using Finite_State_Machine;
using UnityEngine;

namespace Assets._Project.Dice_Rolling
{
    public class RollTheDiceState : State
    {
        public RollTheDiceState(IStateSwitcher switcher) : base(switcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("Roll the dice");
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
