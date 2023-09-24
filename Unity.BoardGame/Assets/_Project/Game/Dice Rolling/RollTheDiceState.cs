using Assets._Project.Game.Turn;
using Finite_State_Machine;

namespace Assets._Project.Game.Dice_Rolling
{
    public class RollTheDiceState : State
    {
        private readonly DiceController _diceController;

        public RollTheDiceState(IStateSwitcher switcher, DiceController diceController) : base(switcher)
        {
            _diceController = diceController;
        }

        public override void Enter()
        {
            _diceController.Enable();
            _diceController.OnRolled += OnDiceRolled;
        }

        private void OnDiceRolled(int result)
        {
            _diceController.OnRolled -= OnDiceRolled;
            _diceController.Disable();
            _switcher.Switch<TurnState>();
        }

        public override void Exit()
        {
            _diceController?.Disable();
        }

        public override void Update()
        {
        }
    }
}
