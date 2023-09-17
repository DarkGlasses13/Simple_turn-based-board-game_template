using Assets._Project.Turn_Sequencing;
using Finite_State_Machine;

namespace Assets._Project.Character_Selection
{
    public class SelectCharacterState : State
    {
        private readonly Turn _turn;
        private readonly CharacterSelectionController _characterSelectionController;

        public SelectCharacterState(IStateSwitcher switcher, Turn turn,
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
            _turn.Complete();
        }
    }
}
