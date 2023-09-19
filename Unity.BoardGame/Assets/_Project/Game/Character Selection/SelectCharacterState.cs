using Assets._Project.Game.Turn;
using Finite_State_Machine;

namespace Assets._Project.Game.Character_Selection
{
    public class SelectCharacterState : State
    {
        private readonly TurnSequence _turn;
        private readonly CharacterSelectionController _characterSelectionController;

        public SelectCharacterState(IStateSwitcher switcher, TurnSequence turn,
            CharacterSelectionController characterSelectionController) : base(switcher)
        {
            _turn = turn;
            _characterSelectionController = characterSelectionController;
        }

        public override void Enter()
        {
            _characterSelectionController.Enable();
        }

        public override void Update() { }

        public override void Exit()
        {
            _characterSelectionController?.Disable();
            _turn.CompleteRegistration();
        }
    }
}
